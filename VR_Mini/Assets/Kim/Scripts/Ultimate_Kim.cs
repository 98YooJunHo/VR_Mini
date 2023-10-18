using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Kim : MonoBehaviour
{
    public GameObject breath;

    private Animator animator;
    private MonsterWeakPoint monsterWeakPoint;
    public bool isBreak;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        isBreak = monsterWeakPoint.BreakUp();
    }
    public IEnumerator Ultimate_()
    {
        animator.Play("Fly Up");
        yield return new WaitForSeconds(4.0f);
        monsterWeakPoint.MakeWeakPoint();
        animator.Play("Fly Idle");
        yield return new WaitForSeconds(6.0f);

        // 약점 공격하면 끊기는 코드
        if (!isBreak)
        {
            animator.Play("Ultimate");
            yield return new WaitForSeconds(3.0f);
            breath.SetActive(true);
            yield return new WaitForSeconds(1);
            breath.SetActive(false);
            monsterWeakPoint.isWork = false;
        }
        else
        {
            yield break;
        }
    }
}
