using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_Canvas_Yoo : MonoBehaviour
{
    private enum TARGET
    {
        TIME, GOLD, PLAYER_HP, BOSS_HP
    }

    enum BOSS_PHASE
    {
        ONE = 1, TWO = 2, THREE = 3
    }

    enum COLOR
    {
        PHASE_ONE, PHASE_TWO, PHASE_THREE
    }

    #region Variable
    private const int TOTAL_BOSS_PHASE = 3;

    private TMP_Text timeTMP;
    private TMP_Text goldTMP;
    private TMP_Text playerHpTMP;
    private TMP_Text bossHpTMP;
    private Image playerHpImg;
    private Image bossHpImg;

    private Color[] bossHpColors = new Color[TOTAL_BOSS_PHASE];
    private int bossPhase;
    private int bossHp;
    private int playerHp;
    #endregion

    private void Awake()
    {
        timeTMP = transform.GetChild((int)TARGET.TIME).GetComponentInChildren<TMP_Text>();
        goldTMP = transform.GetChild((int)TARGET.GOLD).GetComponentInChildren<TMP_Text>();
        playerHpTMP = transform.GetChild((int)TARGET.PLAYER_HP).GetComponentInChildren<TMP_Text>();
        bossHpTMP = transform.GetChild((int)TARGET.BOSS_HP).GetComponentInChildren<TMP_Text>();
        playerHpImg = transform.GetChild((int)TARGET.PLAYER_HP).GetComponent<Image>();
        bossHpImg = transform.GetChild((int)TARGET.BOSS_HP).GetComponent<Image>();

        bossPhase = GameManager.Instance.bossPhase;
        bossHp = GameManager.Instance.bossHp;
        playerHp = GameManager.Instance.playerHp;

        //bossHpColors[(int)COLOR.PHASE_ONE] = bossHpImg.GetComponent<Color>();
        //bossHpColors[(int)COLOR.PHASE_TWO] = new Color();
        //bossHpColors[(int)COLOR.PHASE_THREE] = new Color();
    }

    // Start is called before the first frame update
    void Start()
    {
        Set_PlayerHpGauge();
        Set_BossHpGauge();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameOver)
        {
            return;
        }
        timeTMP.text = ((int)(GameManager.Instance.time / 60)) + ":" 
            + ((int)(GameManager.Instance.time % 60)).ToString("D2");
        goldTMP.text = "" + GameManager.Instance.gold;

        Set_PlayerHpGauge();
        Set_BossHpGauge();
    }

    #region Function
    private void Set_PlayerHpGauge()
    {
        if(playerHp == GameManager.Instance.playerHp)
        {
            return;
        }

        playerHp = GameManager.Instance.playerHp;
        playerHpImg.fillAmount = GameManager.Instance.playerHp / (float)GameManager.Instance.playerMaxHp * 0.5f;
        playerHpTMP.text = GameManager.Instance.playerHp + "/" + GameManager.Instance.playerMaxHp;
    }

    private void Set_BossHpGauge()
    {
        if (bossHp == GameManager.Instance.bossHp)
        {
            return;
        }

        bossHp = GameManager.Instance.bossHp;
        bossHpImg.fillAmount = GameManager.Instance.bossHp / (float)GameManager.Instance.bossMaxHp * 0.5f;
        bossHpTMP.text = GameManager.Instance.bossHp + "/" + GameManager.Instance.bossMaxHp;
    }

    private void Set_BossHpColor()
    {

    }
    #endregion
}
