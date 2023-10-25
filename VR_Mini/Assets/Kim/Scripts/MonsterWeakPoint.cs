using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Monster_Kim;

public class MonsterWeakPoint : EctGetDamage
{
    public List<GameObject> landWeakPoint; 
    public List<GameObject> flyWeakPoint;

    public List<GameObject> weakPoints;

    public GameObject weakpoint;
    //private DamagedPoint point_;
    private Monster_Kim monster;
    private Player player;
    private int addWeakPoint;

    private MonsterHP monsterHP;
    private DamagedPoint point;

    //public bool isWork = false;

    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster_Kim>();
        addWeakPoint = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_COUNT);
        player = GameObject.Find("Player").GetComponent<Player>();
        monsterHP = GetComponent<MonsterHP>();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public void MakeWeakPoint()
    {
        if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
        {
            for (int i = 0; i < addWeakPoint; i++)
            {
                int rnd = Random.Range(0, flyWeakPoint.Count);
                do
                {
                    rnd = Random.Range(0, flyWeakPoint.Count);
                } while (weakPoints.Contains(flyWeakPoint[rnd]));
                point = flyWeakPoint[i].GetComponent<DamagedPoint>();
                point.isStart = false;

                weakPoints.Add(flyWeakPoint[rnd]);
                flyWeakPoint[rnd].SetActive(true);                
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
                landWeakPoint[rnd].SetActive(true);
            }
            //landWeakPoint.RemoveAt(rnd);
        }
    }
    
    public void PointBreak()
    {
        if (weakpoint != null)
        {
            weakPoints.Remove(weakpoint); //이거때문에 오프 안됨
            //if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
            //{
            //    flyWeakPoint.Add(weakpoint);
            //}
            //else
            //{
            //    weakPoints.Add(weakpoint);
            //}
            weakpoint = null;
        }
    }

    public bool BreakUp()
    {
        if (weakPoints.Count <= 0)
        {
            // 여기에 약점공략시 대미지 주는 코드
            if (monster.monsterHP.hp <= monster.monsterHP.maxHP - monster.monsterHP.hp1 && monster.monsterHP.hp > monster.monsterHP.maxHP - (monster.monsterHP.hp1 + monster.monsterHP.hp2))
            {
                //float pase = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.P2_HP);
                float pase = GameManager.Instance.bossMaxHp;
                float damage = pase / (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_DMG);
                //monsterHP.hp =- damage;
            }
            else if(monster.monsterHP.hp < monster.monsterHP.maxHP - (monster.monsterHP.hp1 + monster.monsterHP.hp2))
            {
                //float pase = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.P3_HP);
                float pase = GameManager.Instance.bossMaxHp;
                float damage = pase / (float)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.WEAKPOINT_DMG);
                //monsterHP.hp =- damage;
            }
            weakPoints = new List<GameObject>();

            return true;
        }

        //weakPoints = new List<GameObject>();

        return false;

    }

    public void Fail()
    {

        for (int i = 0; i < weakPoints.Count; i++)
        {
            Debug.Log("1");
            weakPoints[i].SetActive(false);
            //if (monster.type == Monster_Kim.MonsterDoingType.ultimate)
            //{
            //   //flyWeakPoint.Add(weakPoints[i]);
            //    flyWeakPoint[i].SetActive(false);
            //}
            //else
            //{
            //   // landWeakPoint.Add(weakPoints[i]);
            //    landWeakPoint[i].SetActive(false);
            //}
        }
        Debug.Log("2");

        weakPoints = null;
        weakPoints = new List<GameObject>();
    }
}
