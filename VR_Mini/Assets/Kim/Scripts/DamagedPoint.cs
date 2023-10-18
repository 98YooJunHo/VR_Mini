using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedPoint : MonoBehaviour
{
    public bool isWeakPoint = false;
    public int weakPoint;

    private bool reset = false;
    public GameObject weakManager;
    private MonsterWeakPoint monsterWeakPoint;
    private ResourceManager rm;


    private void Start()
    {
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
    }

    private void Update()
    {
        if(!reset && isWeakPoint)
        {
            weakPoint = (int)rm.GetSingleDataFromID(Order.MONSTER, Monster.WEAKPOINT_RATE);
            reset = true;
        }
        if(monsterWeakPoint.isWork)
        {
            weakManager.SetActive(false);
        }
        if(isWeakPoint)
        {
            weakManager.SetActive(true);
            if (weakPoint <= 0) 
            { 
                isWeakPoint = false;
                weakManager.SetActive(false);
                reset = false;
            }
        }
        else
        {
            // 일반 데미지
        }
    }

}
