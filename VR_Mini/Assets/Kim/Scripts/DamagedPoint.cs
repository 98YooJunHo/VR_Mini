using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : MonoBehaviour
{
    //public bool isWeakPoint = false;
    private GameObject dragon;
    public int weakpoint;
    private MonsterWeakPoint monsterWeakPoint;
    private void Start()
    {
        weakpoint = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_HP);
        dragon = GameObject.Find("Dragon");
    }

    private void Update()
    {
        monsterWeakPoint = dragon.GetComponent<MonsterWeakPoint>();
        if (weakpoint <= 0)
        {

            monsterWeakPoint.weakpoint = transform.gameObject;
            monsterWeakPoint.PointBreak();
            transform.gameObject.SetActive(false);
        }
    }
}
