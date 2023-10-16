using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Kim : MonoBehaviour
{
    private MonsterAttack monsterAttack;
    private Ultimate_Kim ult;

    private int paseTrigger = 30;
    private float attackTrigger = 5;
    private float checkTime = 0;
    private MonsterHP monsterHP;
    private int maxHP;

    private Animator animator;
    private float speed = 3.2f;

    public bool pattern = false;
    private bool doOnce = false;

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
        monsterAttack = GetComponent<MonsterAttack>();
        ult = GetComponent<Ultimate_Kim>();

        monsterHP = transform.GetComponent<MonsterHP>();
        maxHP = monsterHP.hp;
        type = MonsterDoingType.idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pattern)
        {
            checkTime += Time.deltaTime;
        }
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
            Debug.Log("2");
            Pase2();
        }
        else if (monsterHP.hp < maxHP - (paseTrigger * 2))
        {
            Debug.Log("3");
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
            pattern = true;
            animator.Play("Scream");
            StartCoroutine(Attack());
        }
        else if (type == MonsterDoingType.skill_1)
        {
            pattern = true;
            animator.Play("Idle");
            StartCoroutine(Skill_1());

        }
        else if (type == MonsterDoingType.skill_2)
        {
            pattern = true;
            animator.Play("");
            StartCoroutine(Skill_2());


        }
        else if (type == MonsterDoingType.ultimate)
        {
            pattern = true;
            if (!doOnce)
            {
                StartCoroutine(Ultimate());
            }
            Debug.Log("111");

        }
        else if (type == MonsterDoingType.die)
        {
            pattern = true;
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
        // �̻��� ����
        if(!doOnce)
        {
            StartCoroutine(monsterAttack.Missile());
            doOnce = true;
        }
        yield return new WaitForSeconds(8f);      // ��� �ð� ���鼭 �ð� �����ؾߵ�

        checkTime = 0;
        type = MonsterDoingType.idle;
        doOnce = false;
        pattern = false;
    }

    private IEnumerator Skill_1()
    {
        // ���׿�
        yield return new WaitForSeconds(1.1f);      // ��� �ð� ���鼭 �ð� �����ؾߵ�
        checkTime = 0;
        pattern = false;
    }

    private IEnumerator Skill_2()
    {
        yield return new WaitForSeconds(1.1f);      // ��� �ð� ���鼭 �ð� �����ؾߵ�
        checkTime = 0;
        pattern = false;
    }
    private IEnumerator Ultimate()
    {
        // ȭ�����
        
            doOnce = true;
            StartCoroutine(ult.Ultimate_());
        
        yield return new WaitForSeconds(13f);      // ��� �ð� ���鼭 �ð� �����ؾߵ�
        animator.Play("Land");  
        yield return new WaitForSeconds(4f);
        Debug.Log(type);
        checkTime = 0;
        type = MonsterDoingType.idle;
        doOnce = false;
        pattern = false;
        yield break;
    }
    //IEnumerator BackIdle(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //    type = MonsterDoingType.idle;
    //}
}
