using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimate_Kim : MonoBehaviour
{
    public GameObject breath;

    private Animator animator;
    private MonsterWeakPoint monsterWeakPoint;
    public bool isBreak;
    private Player player;
    private Monster_Kim monster;
    private int damage;
    public float wait;
   // private Vector3 startPos = new Vector3(0, 0, (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER, MONSTER.FAR));

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = startPos;
        animator = GetComponent<Animator>();
        monsterWeakPoint = GetComponent<MonsterWeakPoint>();
        monster = GetComponent<Monster_Kim>();
        player = GameObject.Find("Player").GetComponent<Player>();
        damage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_BREATHE_SKILL, MONSTER_BREATHE_SKILL.SKILL_DMG);
        wait = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_BREATHE_SKILL, MONSTER_BREATHE_SKILL.CASTING_TIME);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator Ultimate_()
    {
        Debug.Log("3");

        monster.useUlt = true;
        animator.Play("Fly Up");
        yield return new WaitForSeconds(4.0f);
        monsterWeakPoint.MakeWeakPoint();
        animator.Play("Fly Idle");
        yield return new WaitForSeconds(wait);
        isBreak = monsterWeakPoint.BreakUp();
        // 약점 공격하면 끊기는 코드
        if (!isBreak)
        {
            monsterWeakPoint.Fail();
            animator.Play("Ultimate");
            yield return new WaitForSeconds(0.5f);
            breath.SetActive(true);
            yield return new WaitForSeconds(2.0f);

            player.DamageTake(damage);
            breath.SetActive(false);
            yield return new WaitForSeconds(1);
            monster.useUlt = false;
            // monsterWeakPoint.isWork = false;
        }
        else
        {
            monster.useUlt = false;
            yield break;
        }
    }
}
