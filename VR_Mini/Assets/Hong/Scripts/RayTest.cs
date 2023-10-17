using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private LineRenderer lineRenderer = default;
    private GameObject shopUI;
    private bool isClicked = false;

    private DamageButton attButton;
    private WeakButton weakButton;

    public Transform att;
    public Transform weak;

    // Start is called before the first frame update
    void Start()
    {
        // shopUI
        shopUI = GameObject.Find("ShopUI");
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            if (shopUI.transform.localScale == Vector3.zero)
            {
                UIManager.Instance.Open_ShopUI();
            }
            else { UIManager.Instance.Close_ShopUI(); lineRenderer.enabled = false; }

            isClicked = !isClicked;

        }
        if (isClicked == true)
        {
            lineRenderer.enabled = true;
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 300);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("ShopLayer");
            int tLayer = 1 << LayerMask.NameToLayer("Terrain");

            if (Physics.Raycast(ray, out hitInfo, 300f, layer | tLayer))
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
                attButton = hitInfo.transform.gameObject.GetComponent<DamageButton>();
                weakButton = hitInfo.transform.gameObject.GetComponent<WeakButton>();

                if (hitInfo.collider.gameObject.name == "AttackSpeedButton")
                {
                    attButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
                    {
                        attButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "WeakPointButton")
                {
                    weakButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
                    {
                        weakButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "ESCButton")
                {
                    if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
                    { 
                        isClicked = !isClicked;
                        UIManager.Instance.Close_ShopUI(); 
                        lineRenderer.enabled = false;                    
                    }
                }
                else 
                {
                    if (hitInfo.collider.name == "Terrain" || hitInfo.collider.name == "Sphere_128_flip")
                    {
                        DamageButton a = att.GetComponent<DamageButton>();
                        WeakButton w = weak.GetComponent<WeakButton>();
                        w.OnRayOut();
                        a.OnRayOut();
                    }
                }
            }
        }
    }
}
