using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSkill_1 : MonoBehaviour
{
    public GameObject armageddon;
    private MonsterWeakPoint monsterWeakPoint;

    private Animator animator;
    public bool isBreak = false;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
       
    }
    public IEnumerator Skill_1()
    {
        monsterWeakPoint.MakeWeakPoint();
        // 약점 노출
        yield return new WaitForSeconds(4);
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
            yield break;
        }
        
    }
}
