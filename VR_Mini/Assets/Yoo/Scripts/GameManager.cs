using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
                if (m_instance == null)
                {
                    GameObject gameManager = new GameObject("GameManager");
                    gameManager.AddComponent<GameManager>();
                }
            }
            return m_instance;
        }
    }
    private static GameManager m_instance;

    #region !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse
    /* !게임의 승패, 점수와 관련된 GameManager입니다.
     * gold, playerHp, bossHp, 게임의 상태를 알려주는 gameOver, 시간을 표시하는 time을 포함한 스크립트입니다.
     * Start_Game, End_Game, Restart_Game, Exit_Game 함수를 통해 게임 시작, 게임 끝, 게임 재시작, 게임 종료를 실행 할 수 있습니다.
     * gold, score의 경우 Add_xxx(value)를 통해 증가 시킬 수 있습니다.
     * gold의 경우 Use_Gold(value)로 사용할 수 있습니다.
     */
    #endregion

    #region Variable
    public float goldPerTimeDelay = 1f;
    public int goldPerTime = 5;
    public float scorePerTimeDelay = 1f;
    public int scorePerTime = 5;
    public float goldToScorePer = 0.4f;
    public bool shopOpen = false;

    private WaitForSeconds goldDelay = default;
    private WaitForSeconds scoreDelay = default;
    public int score { get; private set; }
    public bool gameOver { get; private set; }
    public float time { get; private set; }
    public int playerMaxHp { get; private set; }
    public int bossMaxHp { get; private set; }
    public int gold { get; private set; }
    private int mBossHp;
    private int mPlayerHp;
    public int bossHp
    {
        get { return mBossHp; }
        set
        {
            if (value < 0)
                value = 0;
            if (value > bossMaxHp)
                value = bossMaxHp;
            mBossHp = value;
        }
    }
    public int playerHp
    {
        get { return mPlayerHp; }
        set
        {
            if (value < 0)
                value = 0;
            if (value > playerMaxHp)
                value = playerMaxHp;
            mPlayerHp = value;
        }
    }
    #endregion

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(this);
            }
        }

        Init_Stats();
    }
    // Start is called before the first frame update
    void Start()
    {
        Init_Delay();
        Init_UI();
        //Invoke("Start_Game", 3f);
        //Invoke("End_Game", 5.5f);
        //Invoke("Exit_Game", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            time += Time.deltaTime;
            if (playerHp <= 0 || bossHp <= 0)
            {
                End_Game();
            }
        }
    }

    #region Init
    public void Init_All()
    {
        Init_Delay();
        Init_Stats();
        Init_UI();
    }

    public void Init_Stats()
    {
        time = 0;
        score = 0;
        gameOver = true;
        shopOpen = false;
        playerMaxHp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.HP);
        bossMaxHp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, Monster.HP);
        gold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        playerHp = playerMaxHp;
        bossHp = bossMaxHp;
    }

    public void Init_Delay()
    {
        goldDelay = new WaitForSeconds(goldPerTimeDelay);
        scoreDelay = new WaitForSeconds(scorePerTimeDelay);
    }

    public void Init_UI()
    {
        UIManager.Instance.Close_Hud();
        UIManager.Instance.Close_ShopUI();
        UIManager.Instance.Close_GameOverUI();
        UIManager.Instance.Open_GameStartUI();
    }
    #endregion

    #region Function
    public void Start_Game()
    {
        gameOver = false;
        UIManager.Instance.Close_GameStartUI();
        UIManager.Instance.Open_Hud();
        StartCoroutine(Add_GoldPerTime());
        StartCoroutine(Add_ScorePerTime());
    }

    public void Restart_Game()
    {
        Init_All();
        Start_Game();
    }

    public void End_Game()
    {
        gameOver = true;
        shopOpen = false;
        score += (int)((float)gold * goldToScorePer);
        // ToDo: 몬스터 페이즈에 따른 점수 추가 필요
        UIManager.Instance.Close_Hud();
        UIManager.Instance.Open_GameOverUI();
        UIManager.Instance.Close_ShopUI();
    }

    public void Exit_Game()
    {
        Application.Quit();
    }

    public void Add_Score(int bonusScore)
    {
        score += bonusScore;
    }

    public void Add_Gold(int bonusGold)
    {
        gold += bonusGold;
    }

    public void Use_Gold(int price)
    {
        if(price > gold)
        {
            return;
        }
        else
        {
            gold -= price;
        }
    }

    private IEnumerator Add_GoldPerTime()
    {
        while(!gameOver)
        {
            yield return goldDelay;
            gold += goldPerTime;
        }

        if (gameOver)
            yield break;
    }

    private IEnumerator Add_ScorePerTime()
    {
        while(!gameOver)
        {
            yield return scoreDelay;
            score += scorePerTime;
        }

        if (gameOver) 
            yield break;
    }
    #endregion
}