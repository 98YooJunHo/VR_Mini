using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Kim : MonoBehaviour
{
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
        //isBreak = monsterWeakPoint.BreakUp();
    }
    public IEnumerator Ultimate_()
    {
        animator.Play("Fly Up");
        yield return new WaitForSeconds(4.0f);
        //monsterWeakPoint.MakeWeakPoint();
        animator.Play("DoFlying");
            yield return new WaitForSeconds(6.0f);

        // ���� �����ϸ� ����� �ڵ�
        if (!isBreak)
        {
            Debug.Log("1");
            animator.Play("Ultimate");
            yield return new WaitForSeconds(3.0f);
            Debug.Log("2");

        }
        else
        {
            yield break;
        }
    }
}
