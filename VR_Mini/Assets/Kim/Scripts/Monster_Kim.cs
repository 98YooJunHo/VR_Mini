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
    private GameManager gameManager;

    private int paseTrigger = 30;
    private float attackTrigger = 5;
    private float checkTime = 0;
    public MonsterHP monsterHP;
    private float maxHP;

    private Animator animator;
    //private float speed = 3.2f;

    public bool pattern = false;
    private bool doOnce = false;
    private bool doUlt = false;

    public bool useUlt = false;
    public bool pase_1UseUlt = true;
    public bool pase_2UseUlt = true;
    public bool pase_3UseUlt = true;

    public int after = 0;
    public int before = 0;

    public enum MonsterDoingType
    {
        idle,
        attack,
        skill_1,
        skill_2,
        ultimate,
        die,
    }
    public MonsterDoingType type;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        monsterAttack = GetComponent<MonsterAttack>();
        ult = GetComponent<Ultimate_Kim>();
        skill_1 = GetComponent<MonsterSkill_1>();
        soulSuck = transform.GetComponent<SoulSuck>();

        monsterHP = GetComponent<MonsterHP>();
        type = MonsterDoingType.idle;
        animator = GetComponent<Animator>();
        maxHP = monsterHP.hp;
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        if (!pattern)
        {
            checkTime += Time.deltaTime;
        }
        if (type != MonsterDoingType.die)
        {
            MonsterPase();
        }
        MonsterMove();
        if (pase_1UseUlt && monsterHP.hp <= monsterHP.maxHP - monsterHP.hp1)
        {
            after++;
            pase_1UseUlt = false;
        }
        else if (pase_2UseUlt && monsterHP.hp <= monsterHP.maxHP - monsterHP.hp1 - monsterHP.hp2)
        {
            after++;
            pase_2UseUlt = false;
        }
        else if (pase_3UseUlt && monsterHP.hp <= 0)
        {
            after++;
            pase_3UseUlt = false;
        }
    }

    private void MonsterPase()
    {
        if (monsterHP.hp > monsterHP.maxHP - monsterHP.hp1 && !pattern)
        {            
            Pase1();
        }
        else if (monsterHP.hp <= monsterHP.maxHP - monsterHP.hp1 
            && monsterHP.hp > monsterHP.maxHP - monsterHP.hp1 - monsterHP.hp2 && !pattern)
        {
            if(after != before && !pattern)
            {
                before ++;
                type = MonsterDoingType.ultimate;
                pase_1UseUlt = false;
            }

            if (!useUlt)
            {
                Pase2();
            }
        }
        else if (monsterHP.hp < monsterHP.maxHP - (monsterHP.hp1 + monsterHP.hp2) && !pattern)
        {
            if (after != before && !pattern)
            {
              
                before++;
                type = MonsterDoingType.ultimate;
                pase_2UseUlt = false;
            }

            if (!useUlt)
            {
                Pase3();
            } 
        }
        else if (monsterHP.hp <= 0 && !pattern)
        {
            if (after != before)
            {

                before++;
                type = MonsterDoingType.ultimate;
                pase_3UseUlt = false;
            }
            if (!pase_1UseUlt)
            {
                type = MonsterDoingType.die;
            }
        }
    }

    private void MonsterMove()
    {
        if (type == MonsterDoingType.idle && !useUlt)
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
            if (!doUlt)
            {
                doUlt = true;
                StartCoroutine(Ultimate());
            }
        }
        else if (type == MonsterDoingType.die)
        {
            pattern = true;
            animator.Play("Die");
            gameManager.End_Game();
        }
    }

    private void Pase1()
    {
        if (before == after)
        {
            if (checkTime > attackTrigger)
            {
                type = MonsterDoingType.attack;
            }
        }
    }
    private void Pase2()
    {
        if (before == after)
        {


            int pattern = Random.Range(0, 10);

            if (checkTime > attackTrigger)
            {
                if (pattern < (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.PROB_P2))
                {
                    type = MonsterDoingType.attack;
                }
                else
                {
                    type = MonsterDoingType.skill_1;
                }
            }
        }
    }
    private void Pase3()
    {
        int pattern = Random.Range(0, 10);
        if (before == after)
        {

            if (checkTime > attackTrigger)
            {
                if (pattern < (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.PROB_P3))
                {
                    type = MonsterDoingType.attack;
                }
                else if (pattern <= (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.PROB_P3)
                    && pattern < (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_SOUL_SKILL, MONSTER_SOUL_SKILL.PROB_P3))
                {
                    type = MonsterDoingType.skill_1;
                }
                else if (pattern >= (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_SOUL_SKILL, MONSTER_SOUL_SKILL.PROB_P3)
                    && pattern <= (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_METEOR_SKILL, MONSTER_METEOR_SKILL.PROB_P3))
                {

                    type = MonsterDoingType.skill_2;
                }
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
        if (after == before && !useUlt)
        {
            type = MonsterDoingType.idle;
        }
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
        yield return new WaitForSeconds(skill_1.wait + 1.667f);      // 모션 시간 보면서 시간 조정해야됨
        checkTime = 0;
        if (after == before && !useUlt)
        {
            type = MonsterDoingType.idle;
        }
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
        if (after == before && !useUlt)
        {

            type = MonsterDoingType.idle;
        }
        doOnce = false;
        pattern = false;
    }
    private IEnumerator Ultimate()
    {
        // 화염방사

        doUlt = true;

        StartCoroutine(ult.Ultimate_());

        yield return new WaitForSeconds(ult.wait + 7.5f);      // 모션 시간 보면서 시간 조정해야됨
        animator.Play("Land");
        yield return new WaitForSeconds(4f);
        if (after == before)
        {
            type = MonsterDoingType.idle;
        }
        checkTime = 0;
        doUlt = false;
        pattern = false;
        yield break;
    }
    //IEnumerator BackIdle(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //    type = MonsterDoingType.idle;
    //}
}
