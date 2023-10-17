using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private LineRenderer lineRenderer = default;
    private Transform shopUI;
    private bool isClicked = false;

    private DamageButton attButton;
    private WeakButton weakButton;

    public Transform att;
    public Transform weak;

    // Start is called before the first frame update
    void Start()
    {
        // shopUI
        shopUI = gameObject.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetChild(1).gameObject.transform;
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two, ARAVRInput.Controller.RTouch))
        {
            if (!shopUI.gameObject.activeSelf)
            {
                shopUI.gameObject.SetActive(true);
            }
            else { shopUI.gameObject.SetActive(false); lineRenderer.enabled = false; }

            isClicked = !isClicked;

        }
        if (isClicked == true)
        {
            lineRenderer.enabled = true;
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            RaycastHit hitInfo;
            int layer = 1 << LayerMask.NameToLayer("ShopLayer");
            int tLayer = 1 << LayerMask.NameToLayer("Terrain");




            if (Physics.Raycast(ray, out hitInfo, 100f, layer | tLayer))
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
                        //Debug.Log("ATTButton");
                        attButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "WeakPointButton")
                {
                    weakButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch))
                    {
                        //Debug.Log("WeakButton");
                        weakButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "ESCButton")
                {
                    //끄기
                    isClicked = !isClicked;
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
