using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUITransform_Yoo : MonoBehaviour
{
    private Vector3 playerPos = default;
    private Vector3 dir = default;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            transform.position = ARAVRInput.LHandPosition;
            dir = transform.position - playerPos;
            transform.forward = dir;
        }
    }
}
