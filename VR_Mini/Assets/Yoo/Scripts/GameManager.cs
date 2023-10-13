using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }
    public static GameManager m_instance;

    public bool gameStart { get; private set; }
    public bool gameOver { get; private set; }
    private int score = default;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = false;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start_Game()
    {
        gameStart = true;
    }

    public void Game_Over()
    {
        gameOver = true;
    }

    public int Get_Score()
    {
        return score;
    }

    public void Add_Score(int bonusScore)
    {
        score += bonusScore;
    }
}
