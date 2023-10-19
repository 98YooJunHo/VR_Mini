using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    private int iD;
    private string description;
    public float weakPointRate;
    private int actTime;
    public int hp;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.gameOver)
        {
            return;
        }

        if(GameManager.Instance.bossHp == 0)
        {
            GameManager.Instance.End_Game();
        }
    }

    public void OnHit(int value)
    {
        GameManager.Instance.bossHp -= value;
    }
}
