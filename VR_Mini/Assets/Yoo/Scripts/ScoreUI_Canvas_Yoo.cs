using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI_Canvas_Yoo : MonoBehaviour
{
    private TMP_Text scoreTMP;

    private enum Target
    {
        SCORE = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreTMP = transform.GetChild((int)Target.SCORE).GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameOver)
        {
            return;
        }
        scoreTMP.text = "" + GameManager.Instance.score;
    }
}
