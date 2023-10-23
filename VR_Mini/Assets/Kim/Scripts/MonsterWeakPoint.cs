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

    private int addWeakPoint;

    //public bool isWork = false;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster_Kim>();
            addWeakPoint = 5;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void MakeWeakPoint()
    {
        weakPoints = new List<GameObject>();
        if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
        {
            for (int i = 0; i < addWeakPoint; i++)
            {
                int rnd = Random.Range(0, flyWeakPoint.Count);
                do
                {
                    rnd = Random.Range(0, flyWeakPoint.Count);
                } while (weakPoints.Contains(flyWeakPoint[rnd]));
                weakPoints.Add(flyWeakPoint[rnd]);
                weakPoints[i].SetActive(true);
            }
                //flyWeakPoint.RemoveAt(rnd);
        }
        else
        {
            for (int i = 0; i < addWeakPoint; i++)
            {
                int rnd = Random.Range(0, landWeakPoint.Count);

                do
                {
                    rnd = Random.Range(0, landWeakPoint.Count);
                } while (weakPoints.Contains(landWeakPoint[rnd]));


                weakPoints.Add(landWeakPoint[rnd]);
                weakPoints[i].SetActive(true);

            }
                //landWeakPoint.RemoveAt(rnd);
        }
    }
    
    public void PointBreak()
    {
        if (weakpoint != null)
        {
            weakPoints.Remove(weakpoint);
            if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
            {
                flyWeakPoint.Add(weakpoint);
            }
            else
            {
                weakPoints.Add(weakpoint);
            }
            weakpoint = null;
        }
    }

    public bool BreakUp()
    {      
        
        if(weakPoints.Count <= 0)
        {
            // 여기에 약점공략시 대미지 주는 코드
            return true;
        }


        return false;

    }

    public void Fail()
    {
        for (int i = 0; i < weakPoints.Count; i++)
        {
            weakPoints[i].SetActive(false);
            //if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
            //{
            //    flyWeakPoint.Add(weakPoints[i]);
            //}
            //else
            //{
            //    landWeakPoint.Add(weakPoints[i]);
            //}
        }
        weakPoints = null;
    }
}
