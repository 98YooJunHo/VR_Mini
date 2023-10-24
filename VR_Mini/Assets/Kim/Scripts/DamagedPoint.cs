using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : EctGetDamage
{
    //public bool isWeakPoint = false;
    private GameObject dragon;
    //public int weakpoint;
    private MonsterWeakPoint monsterWeakPoint;
    private void Start()
    {
        hp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
        dragon = GameObject.Find("Dragon");
    }

    private void Update()
    {
        monsterWeakPoint = dragon.GetComponent<MonsterWeakPoint>();
        if (hp <= 0)
        {
            monsterWeakPoint.weakpoint = transform.gameObject;
            monsterWeakPoint.PointBreak();
            transform.gameObject.SetActive(false);
            Debug.Log("실행됨");
        }
    }
}
