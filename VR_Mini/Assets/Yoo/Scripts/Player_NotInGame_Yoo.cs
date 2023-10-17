using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_NotInGame_Yoo : MonoBehaviour
{
    #region Variable
    Ray ray;
    RaycastHit hitInfo;
    Vector3 rayStartPos;
    LineRenderer lineRenderer;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameOver)
        {
            return;
        }

        Physics.Raycast(ray, out hitInfo, 200);
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1, hitInfo.point);

        if(ARAVRInput.GetDown(ARAVRInput.Button.One,ARAVRInput.Controller.RTouch))
        {
            if(hitInfo.collider.gameObject.name == "GameExit")
            {
                GameManager.Instance.Exit_Game();
            }

            if(hitInfo.collider.gameObject.name == "GameStart")
            {
                GameManager.Instance.Start_Game();
            }
        }
    }
}
