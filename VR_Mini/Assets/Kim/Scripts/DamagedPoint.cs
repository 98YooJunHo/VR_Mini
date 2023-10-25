using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : EctGetDamage
{
    //public bool isWeakPoint = false;
    public GameObject dragon;
    //public int weakpoint;
    private MonsterWeakPoint monsterWeakPoint;
    public bool isStart = true;
    private void Start()
    {
        hp = (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
        //dragon = GameObject.Find("Dragon");  //80
        monsterWeakPoint = dragon.GetComponent<MonsterWeakPoint>();
    }

    private void Update()
    {
        //if (!isStart)
        //{
        //    hp = (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
        //    isStart = true;
        //}
        if (hp <= 0)
        {
            monsterWeakPoint.weakpoint = gameObject;
            monsterWeakPoint.PointBreak();
            hp = (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
            gameObject.SetActive(false);
        }
    }
}
