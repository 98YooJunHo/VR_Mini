using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulSuck : MonoBehaviour
{
    public GameObject soul;
    private MonsterWeakPoint monsterWeakPoint;
    private Soul soul_;

    public float wayTime;

    private Animator animator;
    public bool isBreak = false;
    // Start is called before the first frame update
    private void Start()
    {
        isBreak = monsterWeakPoint.BreakUp();

        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
        soul_ = soul.GetComponent<Soul>();
    }
    public IEnumerator Skill_2()
    {
        soul.SetActive(true);
        yield return null;
        float second = soul_.wayTime;
        wayTime = second;
        Debug.Log(second);

        monsterWeakPoint.MakeWeakPoint();
        // 약점 노출
        animator.Play("Idle");
        yield return new WaitForSeconds(second);
        Debug.Log(second);
        if (!isBreak)
        {
            animator.Play("Bite");
            Debug.Log("!");
            Debug.Log("@");
            yield return new WaitForSeconds(2f);

            soul.SetActive(false);
            yield return new WaitForSeconds(2.668f);

            soul_.reset = false;
        }
        else
        {
            yield break;
        }

    }

}
