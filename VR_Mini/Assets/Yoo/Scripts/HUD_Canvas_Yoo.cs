using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD_Canvas_Yoo : MonoBehaviour
{
    private enum TARGET                                 // TMP_Text 가져올 때 사용할 TARGET
    {
        TIME, GOLD, PLAYER_HP, BOSS_HP
    }

    // { 10/23 유준호 추가
    enum BOSS_PHASE                                     // 현재 보스페이즈 비교할 때 사용할 BOSS_PHASE
    {
        ONE = 1, TWO = 2, THREE = 3
    }

    enum BOSSHP_COLOR                                   // 보스 페이즈별 색상을 가져올 때 사용할 BOSSHP_COLOR
    {
        PHASE_ONE, PHASE_TWO, PHASE_THREE
    }
    // } 10/23 유준호 추가

    #region Variable
    private const int TOTAL_BOSS_PHASE = 3;             // 총 보스 페이즈 수 (상수)
    private const int MINUTE = 60;                      // 시간 표시를 위한 분 단위 (상수)
    private const float MAX_FILL_AMOUNT = 0.5f;         // fillAmount 최대 수치 (상수)

    private TMP_Text timeTMP;                           // 시간 TMP_Text
    private TMP_Text goldTMP;                           // 골드 TMP_Text
    private TMP_Text playerHpTMP;                       // 플레이어 체력 TMP_Text
    private TMP_Text bossHpTMP;                         // 보스 체력 TMP_Text
    private Image playerHpImg;                          // 플레이어 체력 Image
    private Image bossHpImg;                            // 보스 체력 Image

    // { 10/23 유준호 추가
    private Color[] bossHpColors = new Color[TOTAL_BOSS_PHASE];         // 보스 페이즈별 보스 체력색상을 저장할 보스색상 배열
    private Color yellowColor = new (255 / 255f, 235 / 255f, 4 / 255f, 100 / 255f); // 1페이즈에 쓸 노란색
    private Color orangeColor = new (255 / 255f, 128 / 255f, 0, 100 / 255f);        // 2페이즈에 쓸 주황색
    private Color redColor = new (255 / 255f, 0, 0, 100 / 255f);                    // 3페이즈에 쓸 빨간색
    private int bossPhase;                              // 보스 페이즈를 받아와서 저장하는 변수
    private int bossHp;                                 // 보스 체력을 받아와서 저장하는 변수
    private int playerHp;                               // 플레이어 체력을 받아와서 저장하는 변수
    // } 10/23 유준호 추가
    #endregion

    private void Awake()
    {
        Init_TextNImg();
    }

    // Start is called before the first frame update
    void Start()
    {
        // { 10/23 유준호 추가
        Init_Stats();
        Init_TextInfoNColor();
        // } 10/23 유준호 추가
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }
        timeTMP.text = ((int)(GameManager.Instance.time / MINUTE)) + ":"
            + ((int)(GameManager.Instance.time % MINUTE)).ToString("D2");
        goldTMP.text = "" + GameManager.Instance.gold;

        Set_BossHpColor();
        Set_PlayerHpGauge();
        Set_BossHpGauge();
    }

    #region Init
    // { 10/23 유준호 추가
    private void Init_TextNImg()            // 텍스트랑 이미지 불러오는 함수
    {
        timeTMP = transform.GetChild((int)TARGET.TIME).GetComponentInChildren<TMP_Text>();
        goldTMP = transform.GetChild((int)TARGET.GOLD).GetComponentInChildren<TMP_Text>();
        playerHpTMP = transform.GetChild((int)TARGET.PLAYER_HP).GetComponentInChildren<TMP_Text>();
        bossHpTMP = transform.GetChild((int)TARGET.BOSS_HP).GetComponentInChildren<TMP_Text>();
        playerHpImg = transform.GetChild((int)TARGET.PLAYER_HP).GetComponent<Image>();
        bossHpImg = transform.GetChild((int)TARGET.BOSS_HP).GetComponent<Image>();
    }

    private void Init_TextInfoNColor()      // 색상, 텍스트 내용, 이미지 충전량 초기설정 하는 함수
    {
        bossHpColors[(int)BOSSHP_COLOR.PHASE_ONE] = yellowColor;
        bossHpColors[(int)BOSSHP_COLOR.PHASE_TWO] = orangeColor;
        bossHpColors[(int)BOSSHP_COLOR.PHASE_THREE] = redColor;

        bossHpImg.color = bossHpColors[(int)BOSSHP_COLOR.PHASE_ONE];

        playerHpImg.fillAmount = playerHp / (float)GameManager.Instance.playerMaxHp * MAX_FILL_AMOUNT;
        playerHpTMP.text = playerHp + "/" + GameManager.Instance.playerMaxHp;

        bossHpImg.fillAmount = bossHp / (float)GameManager.Instance.bossMaxHp * MAX_FILL_AMOUNT;
        bossHpTMP.text = bossHp + "/" + GameManager.Instance.bossMaxHp;
    }

    private void Init_Stats()               // 보스 페이즈, 체력, 플레이어 체력 초기설정하는 함수
    {
        bossPhase = GameManager.Instance.bossPhase;
        bossHp = GameManager.Instance.bossHp;
        playerHp = GameManager.Instance.playerHp;
    }
    // } 10/23 유준호 추가
    #endregion

    #region Function
    private void Set_PlayerHpGauge()                    // 플레이어 체력 변경 감지 시 체력게이지 변경하는 함수 
    {
        if (playerHp == GameManager.Instance.playerHp)
        {
            return;
        }

        playerHp = GameManager.Instance.playerHp;
        playerHpImg.fillAmount = playerHp / (float)GameManager.Instance.playerMaxHp * MAX_FILL_AMOUNT;
        playerHpTMP.text = playerHp + "/" + GameManager.Instance.playerMaxHp;
    }

    private void Set_BossHpGauge()                      // 보스 체력 변경 감지 시 체력게이지 변경하는 함수 
    {
        if (bossHp == GameManager.Instance.bossHp)
        {
            return;
        }

        bossHp = GameManager.Instance.bossHp;
        bossHpImg.fillAmount = bossHp / (float)GameManager.Instance.bossMaxHp * MAX_FILL_AMOUNT;
        bossHpTMP.text = bossHp + "/" + GameManager.Instance.bossMaxHp;
    }

    // { 10/23 유준호 추가
    private void Set_BossHpColor()                      // 보스 페이즈 변경 감지 시 페이즈 별 보스 체력게이지 색상 변경하는 함수
    {
        if (bossPhase == GameManager.Instance.bossPhase)
        {
            return;
        }

        bossPhase = GameManager.Instance.bossPhase;
        
        switch (bossPhase)
        {
            case (int)BOSS_PHASE.ONE:
                bossHpImg.color = bossHpColors[(int)BOSSHP_COLOR.PHASE_ONE];
                break;
            case (int)BOSS_PHASE.TWO:
                bossHpImg.color = bossHpColors[(int)BOSSHP_COLOR.PHASE_TWO];
                break;
            case (int)BOSS_PHASE.THREE:
                bossHpImg.color = bossHpColors[(int)BOSSHP_COLOR.PHASE_THREE];
                break;
            default:
                Debug.LogError("예기치 못한 오류, 찾아내야함");
                break;
        }
    }
    // } 10/23 유준호 추가
    #endregion
}
