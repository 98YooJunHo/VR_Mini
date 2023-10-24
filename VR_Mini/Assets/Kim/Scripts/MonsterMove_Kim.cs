using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove_Kim : MonoBehaviour
{
    private Monster_Kim monster;

    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        //speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_MOVE_SKILL, MONSTER_MOVE_SKILL.MOVE_SPEED_P1);
        speed = 5;
        monster = GetComponent<Monster_Kim>();
        //hp1 = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.P1_HP);
    }


    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.gameOver)
        //{
        //    return;
        //}

        if (monster.type == Monster_Kim.MonsterDoingType.idle && !monster.useUlt)
        {
            // 몬스터 이동
            float move = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), move);

        }

    }
}

