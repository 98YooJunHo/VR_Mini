using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove_Kim : MonoBehaviour
{
    private Monster_Kim monster;

    public Transform targetLocation;
    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        monster = GetComponent<Monster_Kim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            return;
        }

        if (monster.type == Monster_Kim.MonsterDoingType.idle)
        {
            // 몬스터 이동
            float move = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, move);

        }

    }
}
