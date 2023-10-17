using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_Canvas_Yoo : MonoBehaviour
{
    private enum Target
    {
        time, gold, playerHp, bossHp
    }

    #region Variable
    private TMP_Text timeTMP;
    private TMP_Text goldTMP;
    private TMP_Text playerHpTMP;
    private TMP_Text bossHpTMP;
    private Image playerHpImg;
    private Image bossHpImg;
    #endregion

    private void Awake()
    {
        timeTMP = transform.GetChild((int)Target.time).GetComponentInChildren<TMP_Text>();
        goldTMP = transform.GetChild((int)Target.gold).GetComponentInChildren<TMP_Text>();
        playerHpTMP = transform.GetChild((int)Target.playerHp).GetComponentInChildren<TMP_Text>();
        bossHpTMP = transform.GetChild((int)Target.bossHp).GetComponentInChildren<TMP_Text>();
        playerHpImg = transform.GetChild((int)Target.playerHp).GetComponent<Image>();
        bossHpImg = transform.GetChild((int)Target.bossHp).GetComponent<Image>();
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
    public void Set_PlayerHpGauge()
    {
        playerHpImg.fillAmount = (float)GameManager.Instance.playerHp / (float)GameManager.Instance.playerMaxHp * 0.5f;
        playerHpTMP.text = GameManager.Instance.playerHp + "/" + GameManager.Instance.playerMaxHp;
    }

    public void Set_BossHpGauge()
    {
        bossHpImg.fillAmount = (float)GameManager.Instance.bossHp / (float)GameManager.Instance.bossMaxHp * 0.5f;
        bossHpTMP.text = GameManager.Instance.bossHp + "/" + GameManager.Instance.bossMaxHp;
    }
    #endregion
}
