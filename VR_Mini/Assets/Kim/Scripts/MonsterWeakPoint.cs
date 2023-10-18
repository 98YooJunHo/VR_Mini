using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterWeakPoint : MonsterHP
{
    public List<GameObject> landWeakPoint; 
    public List<GameObject> flyWeakPoint;

    private List<GameObject> weakPoints;

    private DamagedPoint point;
    private DamagedPoint point_;
    private Monster_Kim monster;

    public bool isWork = false;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster_Kim>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeWeakPoint()
    {
        if(!isWork)
        {
            weakPoints = null;
            isWork = true;
        }
        if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
        {
            for (int i = 0; i < 3; i++)
            {
                int a = Random.Range(1, 8);
                point = landWeakPoint[i].GetComponent<DamagedPoint>();
                point.isWeakPoint = true;
                weakPoints.Add(landWeakPoint[i]);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                int a = Random.Range(1, 8);
                point = landWeakPoint[i].GetComponent<DamagedPoint>();
                point.isWeakPoint = true;
                weakPoints.Add(landWeakPoint[i]);
            }
        }
    }

    public bool BreakUp()
    {
        for (int i = 0; i < weakPoints.Count; i++)
        {
            point_ = weakPoints[i].GetComponent<DamagedPoint>();

            if (point_.weakPoint > 0)
            {
                return false;
            }
        }
        weakPoints = null;
        return true;
    }

}
