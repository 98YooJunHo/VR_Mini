using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterWeakPoint : MonsterHP
{
    public List<GameObject> landWeakPoint; 
    public List<GameObject> flyWeakPoint;

    public List<GameObject> weakPoints;

    public GameObject weakpoint;
    private DamagedPoint point;
    //private DamagedPoint point_;
    private Monster_Kim monster;

    //public bool isWork = false;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster_Kim>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void MakeWeakPoint()
    {
        if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
        {
            for (int i = 0; i < 5; i++)
            {
                int rnd = Random.Range(0, flyWeakPoint.Count);
                weakPoints.Add(flyWeakPoint[rnd]);
                weakPoints[i].SetActive(true);
                flyWeakPoint.RemoveAt(rnd);
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                int rnd = Random.Range(0, landWeakPoint.Count);
                weakPoints.Add(landWeakPoint[rnd]);
                weakPoints[i].SetActive(true);

                landWeakPoint.RemoveAt(rnd);
            }
        }
    }
    
    public bool BreakUp()
    {      
        if(weakpoint != null)
        {
            weakPoints.Remove(weakpoint);
        }
        if(weakPoints.Count == 0)
        {
            return false;
        }


        return true;

    }

}
