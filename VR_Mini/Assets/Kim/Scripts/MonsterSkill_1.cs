using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill_1 : MonoBehaviour
{
    public GameObject armageddon;
    private MonsterWeakPoint monsterWeakPoint;

    private Animator animator;
    public bool isBreak = false;
    public float wait;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
       wait = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_METEOR_SKILL, MONSTER_METEOR_SKILL.CASTING_TIME);
    }
    public IEnumerator Skill_1()
    {
        monsterWeakPoint.MakeWeakPoint();
        // 약점 노출
        yield return new WaitForSeconds(wait);
        isBreak = monsterWeakPoint.BreakUp();       
        if(!isBreak)
        {
            monsterWeakPoint.Fail();
            animator.Play("Flame Attack");
            yield return new WaitForSeconds(1.667f);
            armageddon.SetActive(true);
        }
        else
        {
            animator.Play("Get Hit");
            yield return new WaitForSeconds(1.333f);
            yield break;
        }
        
    }
}
