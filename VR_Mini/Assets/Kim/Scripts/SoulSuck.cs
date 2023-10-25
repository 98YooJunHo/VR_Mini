using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSuck : MonoBehaviour
{
    public GameObject soul;
    private MonsterWeakPoint monsterWeakPoint;
    private Soul soul_;
    private Player player;
    public float wayTime;
    private int damage;

    private Animator animator;
    public bool isBreak = false;
    // Start is called before the first frame update
    private void Start()
    {
        damage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_SOUL_SKILL, MONSTER_SOUL_SKILL.SKILL_DMG); 
        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
        soul_ = soul.GetComponent<Soul>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    public IEnumerator Skill_2()
    {
        soul.SetActive(true);
        yield return null;
        float second = soul_.wayTime;
        wayTime = second;

        monsterWeakPoint.MakeWeakPoint();
        // 약점 노출
        animator.Play("Idle");
        yield return new WaitForSeconds(second);
        isBreak = monsterWeakPoint.BreakUp();
        if (!isBreak)
        {
            monsterWeakPoint.Fail();
            animator.Play("Bite");

            yield return new WaitForSeconds(2f);
            player.DamageTake(damage);
            soul.SetActive(false);
            yield return new WaitForSeconds(2.668f);

            soul_.reset = false;
        }
        else
        {
            soul.SetActive(false);
            animator.Play("Get Hit");
            yield return new WaitForSeconds(1.333f);
            animator.Play("Idle");
            yield break;
        }

    }

}
