using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI_Canvas_Yoo : MonoBehaviour
{
    private const string WIN_TEXT = "Win!";             // 승리 텍스트 문자열
    private const string LOSE_TEXT = "Lose";            // 패배 텍스트 문자열
    private const string SCORE_TEXT = "Score: ";        // 점수 텍스트 문자열
    private const int MINUTE = 60;                      // 시간 표시를 위한 분 단위 (상수)

    private TMP_Text timeTMP;                           // 시간 TMP_Text
    private TMP_Text resultTMP;                         // 종료 결과 TMP_Text
    private TMP_Text scoreTMP;                          // 점수 TMP_Text

    private enum TARGET                                 // TMP_Text 불러올 때 사용할 TARGET
    {
        TIME, RESULT, SCORE
    }

    private void Awake()
    {
        timeTMP = transform.GetChild((int)TARGET.TIME).GetComponentInChildren<TMP_Text>();
        resultTMP = transform.GetChild((int)TARGET.RESULT).GetComponentInChildren<TMP_Text>();
        scoreTMP = transform.GetChild((int)TARGET.SCORE).GetComponentInChildren<TMP_Text>();
        timeTMP.text = null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver && timeTMP.text == null)
        {
            timeTMP.text = ((int)(GameManager.Instance.time / MINUTE)) + ":"
                + ((int)(GameManager.Instance.time % MINUTE)).ToString("D2");
            scoreTMP.text = SCORE_TEXT + GameManager.Instance.score;
            if(GameManager.Instance.bossHp == 0)
            {
                resultTMP.text = WIN_TEXT;
            }
            else
            {
                resultTMP.text = LOSE_TEXT;
            }
        }

        if (!GameManager.Instance.gameOver && timeTMP.text != null)
        {
            timeTMP.text = null;
        }
    }
}
