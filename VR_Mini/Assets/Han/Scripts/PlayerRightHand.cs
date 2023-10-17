using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRightHand : MonoBehaviour
{
    //���� �׸� ���� ������
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
        // Todo : ���ӿ��� �����϶��� Return �ع����� ���ϰ��ؾ��Ѵ�.
        
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
        // ���� ������ ��������
        if (shopCanvas.activeSelf == true)
        {
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo = default;
            int layer = 1 << LayerMask.NameToLayer("ShopUI");

            if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && (Physics.Raycast(ray, out hitInfo, 200f, layer)))
            {
                // ��ư ����.
            }    
        }


        // ���� ������ ���������ϋ��� �����Է��� ���� ���ϰ��ϴ� �ڵ�
        if ( shopCanvas.activeSelf == true)
        {
            return;
        }

        // �����ڵ�
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
            // 1. ���� ��Ʈ�η��� �������� Ray�� �����.
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo = default;
            int layer = 1 << LayerMask.NameToLayer("Boss");

            // 2. Boss�� Ray �浹 �����Ѵ�.
            if (Physics.Raycast(ray, out hitInfo, 200f, layer))
            {
                // 3. Ray�� �΋H�� ������ ���α׸���
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
                // TODO : ������ �ǰݴ��ϴ� �Լ������Ű��.
                //GameObject effect = BulletPoolManager.instance.GetQueue();
                //effect.transform.position = hitInfo.normal;
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
