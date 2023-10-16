using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private LineRenderer lineRenderer = default;
    private Transform shopUI;
    private bool isClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        shopUI = gameObject.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).gameObject.transform;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
        {
            Debug.Log("¹öÆ°´­·¯Áü?");
            shopUI.gameObject.SetActive(true);
            isClicked = true;
            lineRenderer.enabled = true;
        }
        if (isClicked)
        {
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("ShopLayer");

            if (Physics.Raycast(ray, out hitInfo, 100f, layer))
            {
                Debug.Log("rayCast µé¾î¿È");
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
                Debug.Log("hitInfo : " + hitInfo.collider.gameObject.name);
                if (hitInfo.collider.gameObject.name == "AttackSpeedButton")
                {

                }
                else if (hitInfo.collider.gameObject.name == "WeakPointButton")
                {

                }
                else if (hitInfo.collider.gameObject.name == "ESCButton")
                {

                }

            }
            else 
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            }
        }
    }
}
