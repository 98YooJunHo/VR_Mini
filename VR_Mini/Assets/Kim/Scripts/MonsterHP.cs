using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    public static MonsterHP Instance;
    private int iD;
    private string description;
    public float weakPointRate;
    private int actTime;
    public int hp;
    public int hp1;
    public int hp2;
    public int hp3;
    private float moveSpeed;


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //List<object> monster = ResourceManager.Instance.GetDataFromID(Order.MONSTER);
        //hp1 = (int)monster[(int)MONSTER.P1_HP];
        //hp2 = (int)monster[(int)MONSTER.P2_HP];
        //hp3 = (int)monster[(int)MONSTER.P3_HP];
        //hp = hp1 + hp2 + hp3; 
        hp1 = 1;
        hp2 = 1;
        hp3 = 1;
        hp = hp1 + hp2 + hp3;
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.Instance.gameOver)
        //{
        //    return;
        //}

        //if(GameManager.Instance.bossHp == 0)
        //{
        //    GameManager.Instance.End_Game();
        //}
    }

    public void OnHit(int value)
    {
       // GameManager.Instance.bossHp -= value;
    }
}
