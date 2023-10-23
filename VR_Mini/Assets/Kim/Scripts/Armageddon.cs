using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armageddon : MonoBehaviour
{
    private float speed = 30.0f;
    private bool doOnce = false;
    private Player player;
    private int damage;

    private void Start()
    {
        speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_METEOR_SKILL, MONSTER_METEOR_SKILL.PROJECTILE_SPEED);
        damage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_METEOR_SKILL, MONSTER_METEOR_SKILL.SKILL_DMG);
        player = GameObject.Find("Player").GetComponent<Player>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!doOnce)
        {
            transform.localPosition = new Vector3 (0, 0, 0);
            doOnce = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 0), speed * Time.deltaTime);
        if (transform.localPosition.y < 1)   // y 값 수정해야됨
        {
            player.DamageTake(damage);
            doOnce = false;
            transform.gameObject.SetActive(false);
        }
    }
}
