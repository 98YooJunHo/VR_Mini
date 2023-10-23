using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : MonoBehaviour
{
    //public bool isWeakPoint = false;
    public int weakpoint;
    private MonsterWeakPoint monsterWeakPoint;
    private void Start()
    {
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
        weakpoint = 10;
    }

    private void Update()
    {
        if (weakpoint == 0)
        {
            monsterWeakPoint.weakpoint = transform.gameObject;
            monsterWeakPoint.PointBreak();
            transform.gameObject.SetActive(false);
        }
    }
}
