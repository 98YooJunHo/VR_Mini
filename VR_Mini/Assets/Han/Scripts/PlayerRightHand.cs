using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightHand : MonoBehaviour
{
    //선을 그릴 라인 렌더러
    private LineRenderer lineRenderer = default;
    
    private int currentHp = default;
    private int currentGold = default;
    private int currentWeapon = default;
    private int interval = default;
    private int damage = default;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger,ARAVRInput.Controller.RTouch))
        {
            lineRenderer.enabled = true;
        }
        else if (ARAVRInput.GetUp(ARAVRInput.Button.IndexTrigger,ARAVRInput.Controller.RTouch))
        {
            lineRenderer.enabled = false;
        }
        if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            // 1. 왼쪽 컨트로러를 기준으로 Ray를 만든다.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo = default;
            int layer = 1 << LayerMask.NameToLayer("Boss");

            // 2. Boss만 Ray 충돌 검출한다.
            if (Physics.Raycast(ray, out hitInfo, 200f, layer))
            {
                // 3. Ray가 부딫힌 지점에 라인그리기
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);

                // TODO : 괴수의 피격당하는 함수실행시키기.

            }
            else
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            }
        }
    }
}
