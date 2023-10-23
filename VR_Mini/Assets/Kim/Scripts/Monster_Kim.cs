using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Kim : MonoBehaviour
{
    public static Monster_Kim Instance;
    private MonsterAttack monsterAttack;
    private Ultimate_Kim ult;
    private MonsterSkill_1 skill_1;
    private SoulSuck soulSuck;

    private int paseTrigger = 30;
    private float attackTrigger = 5;
    private float checkTime = 0;
    private MonsterHP monsterHP;
    private int maxHP;

    private Animator animator;
    //private float speed = 3.2f;

    public bool pattern = false;
    private bool doOnce = false;

    public bool useUlt = false;
    public bool pase_1UseUlt = true;
    public bool pase_2UseUlt = true;
    public bool pase_3UseUlt = true;

    private int after = 0;
    private int before = 0;


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
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        monsterAttack = GetComponent<MonsterAttack>();
        ult = GetComponent<Ultimate_Kim>();
        skill_1 = GetComponent<MonsterSkill_1>();
        soulSuck = transform.GetComponent<SoulSuck>();

        monsterHP = FindObjectOfType<MonsterHP>();
        maxHP = monsterHP.hp;
        type = MonsterDoingType.idle;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GameManager.Instance.gameOver)
        //{
        //    return;
        //}

        if (!pattern)
        {
            checkTime += Time.deltaTime;
        }
        MonsterPase();
        MonsterMove();
        if(pase_1UseUlt && monsterHP.hp <= maxHP - paseTrigger && monsterHP.hp > maxHP - (paseTrigger * 2))
        {
            after++;
        }
        else if(pase_2UseUlt && monsterHP.hp < maxHP - (paseTrigger * 2))
        {
            after++;
        }
        else if(pase_3UseUlt && monsterHP.hp <= 0)
        {
            after++;
        }
    }

    private void MonsterPase()
    {
        if (monsterHP.hp > maxHP - paseTrigger)
        {
            Pase1();
        }
        else if (monsterHP.hp <= maxHP - monsterHP.hp1 && monsterHP.hp > maxHP - (monsterHP.hp1 + monsterHP.hp2))
        {
            if(after != before)
            {
                before ++;
                type = MonsterDoingType.ultimate;
                
            }
            Debug.Log("2");
            if (!useUlt)
            {
                Pase2();
            }
        }
        else if (monsterHP.hp < maxHP - (monsterHP.hp1 + monsterHP.hp2))
        {
            if (after != before)
            {
                before++;
                type = MonsterDoingType.ultimate;

            }
            Debug.Log("3");
            if (!useUlt)
            {
                Pase3();
            } 
        }
        else if (monsterHP.hp <= 0)
        {
            if (after != before)
            {
                before++;
                type = MonsterDoingType.ultimate;

            }
            if (!useUlt)
            {
                type = MonsterDoingType.die;
            }
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
            StartCoroutine(Skill_1());

        }
        else if (type == MonsterDoingType.skill_2)
        {
            pattern = true;
            //animator.Play("");
            StartCoroutine(Skill_2());


        }
        else if (type == MonsterDoingType.ultimate)
        {
            pattern = true;
            if (!doOnce)
            {
                StartCoroutine(Ultimate());
            }
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
            type = MonsterDoingType.skill_2;
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
        if(!doOnce)
        {
            StartCoroutine(monsterAttack.Missile());
            doOnce = true;
        }
        yield return new WaitForSeconds(8f);      // 모션 시간 보면서 시간 조정해야됨

        checkTime = 0;
        type = MonsterDoingType.idle;
        doOnce = false;
        pattern = false;
    }

    private IEnumerator Skill_1()
    {
        // 메테오
        if (!doOnce)
        {
            animator.Play("Idle");

            StartCoroutine(skill_1.Skill_1());
            doOnce = true;
        }
        yield return new WaitForSeconds(6.667f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
        type = MonsterDoingType.idle;
        doOnce = false;
        pattern = false;
    }

    private IEnumerator Skill_2()
    {
        if (!doOnce)
        {
            animator.Play("Idle");
            StartCoroutine(soulSuck.Skill_2());

            doOnce = true;
        }
        yield return null; yield return null; yield return null;

        yield return new WaitForSeconds(soulSuck.wayTime + 4.668f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
        type = MonsterDoingType.idle;

        doOnce = false;
        pattern = false;
    }
    private IEnumerator Ultimate()
    {
        // 화염방사

        doOnce = true;
        StartCoroutine(ult.Ultimate_());

        yield return new WaitForSeconds(13f);      // 모션 시간 보면서 시간 조정해야됨
        animator.Play("Land");
        yield return new WaitForSeconds(4f);
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
