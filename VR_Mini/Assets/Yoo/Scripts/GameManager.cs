using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float goldPerTimeDelay = 1f;
    public int goldPerTime = 5;
    public float scorePerTimeDelay = 1f;
    public int scorePerTime = 5;
    public float goldToScorePer = 0.4f;

    private WaitForSeconds goldDelay;
    private WaitForSeconds scoreDelay;
    public int score { get; private set; } = 0;
    public bool gameOver { get; private set; } = true;
    public float time { get; private set; } = 0;
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

    private void Awake()
    {
        Instance = this;

        //Debug.Log((int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.HP));
        playerMaxHp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.HP);
        bossMaxHp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, Monster.HP);
        gold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        playerHp = playerMaxHp;
        bossHp = bossMaxHp;

        Debug.LogFormat("최대체력" + playerMaxHp + "보스최대체력" + bossMaxHp);
    }
    // Start is called before the first frame update
    void Start()
    {
        goldDelay = new WaitForSeconds(goldPerTimeDelay);
        scoreDelay = new WaitForSeconds(scorePerTimeDelay);
        Invoke("Start_Game", 3f);
        //Invoke("Game_Over", 5.5f);
        //Invoke("Game_Exit", 7f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            time += Time.deltaTime;
            if (playerHp <= 0 || bossHp <= 0)
            {
                Game_Over();
            }
        }
    }

    public void Start_Game()
    {
        time = 0;
        score = 0;
        gold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        gameOver = false;
        UIManager.Instance.Close_GameStartUI();
        UIManager.Instance.Close_GameOverUI();
        UIManager.Instance.Open_Hud();
        StartCoroutine(Add_GoldPerTime());
        StartCoroutine(Add_ScorePerTime());
    }

    public void Game_Over()
    {
        gameOver = true;
        score += (int)((float)gold * goldToScorePer);
        // ToDo: 몬스터 페이즈에 따른 점수 추가 필요
        UIManager.Instance.Close_Hud();
        UIManager.Instance.Open_GameOverUI();
    }

    public void Game_Exit()
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
}