using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Kim : MonoBehaviour
{
    private float speed = 0.3f; // 이동 속도
    private float arcHeight = 50f; // 아치의 높이 조절
    private int damage;
    public GameObject target; // 목표 위치
    private Player player;

    private Vector3 initialPosition;
    public float startTime;
    public float delayTime;
    private bool isFlying = false;

    private int hp;

    void Start()
    {
        initialPosition = transform.position;
        target = GameObject.Find("Player");
        player = target.transform.GetComponent<Player>();
        speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.SKILL_SPEED);
        hp = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.PROJECTILE_HP);
        damage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_NORMAL_SKILL, MONSTER_NORMAL_SKILL.SKILL_DMG);
    }

    void Update()
    {
        delayTime += Time.deltaTime;

        if(hp <=0)
        {
            Destroy(gameObject);
        }

        if (delayTime > 2.5f && !isFlying)
        {
            isFlying = true;
            startTime = Time.time;
        }

        if (isFlying)
        {
            float t = (Time.time - startTime) * speed;
            if (t <= 1.0f)
            {
                Vector3 nextPosition = Vector3.Lerp(initialPosition, new Vector3(0, 0, 0), t);
                nextPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;
                transform.position = nextPosition;
            }
            else
            {
                player.DamageTake(damage);
                Destroy(gameObject);
            }
        }
    }
}
