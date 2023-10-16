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

            }
            else
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            }
        }
    }
}
