using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUITransform_Yoo : MonoBehaviour
{
    private const string PLAYER_NAME = "Player";                // 플레이어 포지션 가져올 때 사용할 플레이어 오브젝트의 이름(상수)

    private Vector3 playerPos = default;                        // 플레이어 포지션 저장할 변수
    private Vector3 lHandPos = default;                         // 왼 손 포지션 저장할 변수
    private Vector3 dir = default;                              // 왼 손 포지션을 시작으로 플레이어 포지션이 끝인 방향을 저장할 변수
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            MovePos();
        }
    }

    #region Init
    private void Init()                     // 초기 값 저장하는 함수
    {
        playerPos = GameObject.Find(PLAYER_NAME).transform.position;
        lHandPos = ARAVRInput.LHandPosition;
        transform.position = lHandPos;
        dir = transform.position - playerPos;
        transform.forward = dir;
    }
    #endregion

    #region Function
    private bool CheckPos()                 // 왼 손의 포지션 변경을 감지해서 변경되면 true를 반환하는 함수
    {
        if (lHandPos != ARAVRInput.LHandPosition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void MovePos()                  // 왼 손의 포지션 변경이 감지될 경우 이 스크립트를 보유한 오브젝트의 위치 및 방향을 변경하는 함수
    {
        if(CheckPos())
        {
            lHandPos = ARAVRInput.LHandPosition;
            transform.position = lHandPos;
            dir = transform.position - playerPos;
            transform.forward = dir;
        }
    }
    #endregion
}
