using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NotInGame_Yoo : MonoBehaviour
{
    #region Variable
    private const string UI_LAYER = "UIButtonLayer";            // 타겟 레이어 이름 (상수)
    private const string GAME_START = "GameStart";              // 게임 스타트 버튼 이름 (상수)
    private const string GAME_RESTART = "GameRestart";          // 게임 재시작 버튼 이름 (상수)
    private const string GAME_EXIT = "GameExit";                // 게임 나가기 버튼 이름 (상수)
    private const string STAFF_CRYSTAL = "Aurous_Crystal";      // 레이 시작 포지션을 정하기 위한 오브젝트를 가져오기 위한 이름 (상수)
    private const int RAY_DISTANCE = 500;                       // 레이 사거리 (상수)

    Ray ray;                                                    // 레이
    GameObject staffCrystal = default;                          // 레이 시작 포지션을 가져오기 위한 게임오브젝트
    Vector3 rayDir = default;                                   // 레이 방향을 저장할 변수
    RaycastHit hitInfo;                                         // 레이 맞은 대상 저장할 변수
    LineRenderer lineRenderer;                                  // 라인렌더러 저장 변수
    int targetLayer;                                            // 타겟 레이어 저장할 변수
    GameUIButton_Yoo button;                                    // 버튼스크립트 저장할 변수
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        targetLayer = LayerMask.NameToLayer(UI_LAYER);
        staffCrystal = GameObject.Find(STAFF_CRYSTAL);
        //Debug.Log(targetLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            return;
        }

        Act();

        //ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        //if (!lineRenderer.enabled)
        //{
        //    lineRenderer.enabled = true;
        //}

        //Physics.Raycast(ray, out hitInfo, 500);
        //lineRenderer.SetPosition(0, ray.origin);
        //lineRenderer.SetPosition(1, hitInfo.point);

        //if (hitInfo.collider != null)
        //{
        //    //Debug.Log(hitInfo.collider.gameObject.layer);
        //    if (hitInfo.collider.gameObject.layer == targetLayer)
        //    {
        //        if (hitInfo.collider.gameObject.name == GAME_EXIT || hitInfo.collider.gameObject.name == GAME_START
        //            || hitInfo.collider.gameObject.name == GAME_RESTART)
        //        {
        //            //Debug.Log("버튼 찾음");
        //            button = hitInfo.collider.transform.GetComponent<GameUIButton_Yoo>();
        //            button.OnRayIn();
        //        }
        //        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        //        {
        //            if (hitInfo.collider.gameObject.name == GAME_EXIT)
        //            {
        //                GameManager.Instance.Exit_Game();
        //            }

        //            if (hitInfo.collider.gameObject.name == GAME_START)
        //            {
        //                GameManager.Instance.Start_Game();
        //            }

        //            if (hitInfo.collider.gameObject.name == GAME_RESTART)
        //            {
        //                GameManager.Instance.Restart_Game();
        //            }
        //            button.OnRayOut();
        //        }
        //    }
        //    else
        //    {
        //        if (button != null)
        //        {
        //            button.OnRayOut();
        //        }
        //    }
        //}
    }

    #region Function
    private void Shoot_Ray()                // 레이 쏘는 함수
    {
        rayDir = ARAVRInput.RHandDirection;
        ray = new Ray(staffCrystal.transform.position, rayDir);
        Physics.Raycast(ray, out hitInfo, RAY_DISTANCE);
    }

    private void Show_Line()                // 레이에 맞춰서 라인 그리는 함수
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, hitInfo.point);
    }

    private bool Check_Target()             // 레이의 피격 대상이 맞는지 체크하고 맞다면 버튼 활성화 시키는 함수
    {
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.layer == targetLayer)
            {
                button = hitInfo.collider.transform.GetComponent<GameUIButton_Yoo>();
                button.OnRayIn();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void Off_Button()               // 버튼 비활성화 시키는 함수
    {
        if (button != null)
        {
            button.OnRayOut();
        }
    }

    private void Use_Button()               // 버튼 사용하는 함수
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            if (hitInfo.collider.gameObject.name == GAME_EXIT)
            {
                GameManager.Instance.Exit_Game();
            }

            if (hitInfo.collider.gameObject.name == GAME_START)
            {
                GameManager.Instance.Start_Game();
            }

            if (hitInfo.collider.gameObject.name == GAME_RESTART)
            {
                GameManager.Instance.Restart_Game();
            }
            button.OnRayOut();
        }
    }

    private void Act()                      // 행동 함수
    {
        Shoot_Ray();
        Show_Line();

        if(Check_Target())
        {
            Use_Button();
        }
        else
        {
            Off_Button();
        }
    }
    #endregion
}
