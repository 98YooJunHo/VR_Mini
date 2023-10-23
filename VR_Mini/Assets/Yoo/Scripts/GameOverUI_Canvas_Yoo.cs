using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI_Canvas_Yoo : MonoBehaviour
{
    private const string WIN_TEXT = "Win!";
    private const string LOSE_TEXT = "Lose";
    private const string SCORE_TEXT = "Score: ";

    private TMP_Text timeTMP;
    private TMP_Text resultTMP;
    private TMP_Text scoreTMP;

    private enum Target
    {
        TIME, RESULT, SCORE
    }

    private void Awake()
    {
        timeTMP = transform.GetChild((int)Target.TIME).GetComponentInChildren<TMP_Text>();
        resultTMP = transform.GetChild((int)Target.RESULT).GetComponentInChildren<TMP_Text>();
        scoreTMP = transform.GetChild((int)Target.SCORE).GetComponentInChildren<TMP_Text>();
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
            timeTMP.text = ((int)(GameManager.Instance.time / 60)) + ":"
                + ((int)(GameManager.Instance.time % 60)).ToString("D2");
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
