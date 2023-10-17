using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Kim : MonoBehaviour
{
    private float speed = 0.3f; // 이동 속도
    private float arcHeight = 20f; // 아치의 높이 조절
    public GameObject target; // 목표 위치

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public float startTime;
    public float delayTime;
    private bool isFlying = false;

    void Start()
    {
        initialPosition = transform.position;
        target = GameObject.Find("Player");
        //Transform playerpos = target.transform;
        targetPosition = target.transform.position;
    }

    void Update()
    {
        delayTime += Time.deltaTime;
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
                Vector3 nextPosition = Vector3.Lerp(initialPosition, targetPosition, t);
                nextPosition.y += Mathf.Sin(t * Mathf.PI) * arcHeight;
                transform.position = nextPosition;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
