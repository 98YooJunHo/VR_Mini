using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NotInGame_Yoo : MonoBehaviour
{
    #region Variable
    Ray ray;
    RaycastHit hitInfo;
    LineRenderer lineRenderer;
    int targetLayer;
    GameUIButton_Yoo button;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        targetLayer = LayerMask.NameToLayer("UIButtonLayer");
        //Debug.Log(targetLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            return;
        }

        ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        Physics.Raycast(ray, out hitInfo, 500);
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, hitInfo.point);

        if (hitInfo.collider != null)
        {
            //Debug.Log(hitInfo.collider.gameObject.layer);
            if (hitInfo.collider.gameObject.layer == targetLayer)
            {
                if (hitInfo.collider.gameObject.name == "GameExit" || hitInfo.collider.gameObject.name == "GameStart"
                    || hitInfo.collider.gameObject.name == "GameRestart")
                {
                    //Debug.Log("버튼 찾음");
                    button = hitInfo.collider.transform.GetComponent<GameUIButton_Yoo>();
                    button.OnRayIn();
                }
                if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
                {
                    if (hitInfo.collider.gameObject.name == "GameExit")
                    {
                        GameManager.Instance.Exit_Game();
                    }

                    if (hitInfo.collider.gameObject.name == "GameStart")
                    {
                        GameManager.Instance.Start_Game();
                    }

                    if (hitInfo.collider.gameObject.name == "GameRestart")
                    {
                        GameManager.Instance.Restart_Game();
                    }
                    button.OnRayOut();
                }
            }
            else
            {
                if (button != null)
                {
                    button.OnRayOut();
                }
            }
        }
    }
}
