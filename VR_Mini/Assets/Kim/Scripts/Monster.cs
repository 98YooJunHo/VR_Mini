using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int paseTrigger;
    private float attackTrigger;
    private float checkTime = 0;
    public MonsterHP monsterHP;
    private int maxHP;

    private bool doSomething = false;

    private Animator animator;
    private float speed = 3.2f;
    public enum MonsterDoingType
    {
        idle,
        attack,
        skill_1,
        skill_2,
        ultimate,
        die
    }
    public MonsterDoingType type;
    // Start is called before the first frame update
    void Start()
    {
        monsterHP = transform.GetComponent<MonsterHP>();
        maxHP = monsterHP.hp;
        type = MonsterDoingType.idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        checkTime += Time.deltaTime;
        MonsterPase();        
        MonsterMove();
    }

    private void MonsterPase()
    {
        if (monsterHP.hp > maxHP - paseTrigger)
        {
            Pase1();
        }
        else if (monsterHP.hp <= maxHP - paseTrigger && monsterHP.hp > maxHP - (paseTrigger * 2))
        {
            Pase2();
        }
        else if (monsterHP.hp < maxHP - (paseTrigger * 2))
        {
            Pase3();
        }
        else if (monsterHP.hp <= 0)
        {
            type = MonsterDoingType.die;
        }
    }

    private void MonsterMove()
    {
        if (type == MonsterDoingType.idle)
        {
            animator.Play("Walk");
        }
        else if (type == MonsterDoingType.attack)
        {
            animator.Play("Attack");
            StartCoroutine(Attack());

        }
        else if (type == MonsterDoingType.skill_1)
        {
            animator.Play("Idle");
            StartCoroutine(Skill_1());

        }
        else if (type == MonsterDoingType.skill_2)
        {
            animator.Play("");
            StartCoroutine(Skill_2());


        }
        else if (type == MonsterDoingType.ultimate)
        {
            animator.Play("Fly Up");
            StartCoroutine(Ultimate());


        }
        else if (type == MonsterDoingType.die)
        {
            animator.Play("Die");

        }
    }

    private void Pase1()
    {
        if (checkTime > attackTrigger)
        {
            type = MonsterDoingType.attack;
        }
    }
    private void Pase2()
    {
        int pattern = Random.Range(0, 10);

        if (checkTime > attackTrigger)
        {
            if(pattern < 8)
            {
                type = MonsterDoingType.attack;
            }
            else
            {
                type = MonsterDoingType.skill_1;
            }
        }
    }
    private void Pase3()
    {
        int pattern = Random.Range(0, 10);

        if (checkTime > attackTrigger)
        {
            if(pattern < 6)
            {
                type = MonsterDoingType.attack;
            }
            else if(pattern <= 6 && pattern < 9)
            {
                type = MonsterDoingType.skill_1;
            }
            else if (pattern >= 9 && pattern <= 10)
            {
                type = MonsterDoingType.skill_2;
            }
        }
    }

    private IEnumerator Attack()
    {
        // 미사일 날라감
        yield return new WaitForSeconds(1.1f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
    }

    private IEnumerator Skill_1()
    {
        // 메테오
        yield return new WaitForSeconds(1.1f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
    }

    private IEnumerator Skill_2()
    {
        yield return new WaitForSeconds(1.1f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
    }
    private IEnumerator Ultimate()
    {
        // 화염방사
        yield return new WaitForSeconds(1.1f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
    }
    //IEnumerator BackIdle(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //    type = MonsterDoingType.idle;
    //}
}
