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

    GameObject shopCanvas;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        // Todo : 게임오버 상태일때는 Return 해버려서 못하게해야한다.
        
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            if(shopCanvas.activeSelf == true)
            {
                shopCanvas.SetActive(false);
            }
            if (shopCanvas.activeSelf == false)
            {
                shopCanvas.SetActive(true);
            }
        }
        // 게임 상점이 열렸을때
        if (shopCanvas.activeSelf == true)
        {
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo = default;
            int layer = 1 << LayerMask.NameToLayer("ShopUI");

            if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && (Physics.Raycast(ray, out hitInfo, 200f, layer)))
            {
                // 버튼 실행.
            }    
        }


        // 게임 상점이 열린상태일떄는 공격입력을 받지 못하게하는 코드
        if ( shopCanvas.activeSelf == true)
        {
            return;
        }

        // 공격코드
        if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger,ARAVRInput.Controller.RTouch))
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
                GameObject effect = BulletPoolManager.instance.GetQueue();
                effect.transform.position = hitInfo.normal;
                Invoke("Makefalse",5f);
            }
            else
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            }
        }
    }

    private void Makefalse(GameObject effect)
    {
        BulletPoolManager.instance.InsertQueue(effect);
    }
}
