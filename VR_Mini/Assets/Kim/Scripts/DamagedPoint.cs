using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : EctGetDamage
{
    //public bool isWeakPoint = false;
    public GameObject dragon;
    //public int weakpoint;
    private MonsterWeakPoint monsterWeakPoint;
    private void Start()
    {
        hp = (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
        //dragon = GameObject.Find("Dragon");  //80
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
