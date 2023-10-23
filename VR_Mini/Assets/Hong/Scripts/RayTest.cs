using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    private LineRenderer lineRenderer = default;
    private GameObject shopUI;
    private bool isClicked = false;

    private LaserButton laserButton;
    private LightingButton lightingButton;
    private IceButton iceButton;

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
                laserButton = hitInfo.transform.gameObject.GetComponent<LaserButton>();
                lightingButton = hitInfo.transform.gameObject.GetComponent<LightingButton>();
                iceButton = hitInfo.transform.gameObject.GetComponent<IceButton>();

                if (hitInfo.collider.gameObject.name == "LaserWeapon")
                {
                    laserButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                    {
                        laserButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "LightingWeapon")
                {
                    lightingButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                    {
                        lightingButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "IceWeapon")
                {
                    iceButton.OnRayIn();
                    if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
                    {
                        iceButton.OnRayClick();
                    }
                }
                else if (hitInfo.collider.gameObject.name == "ESCButton")
                {
                    if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
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
                        LaserButton noLaser = FindObjectOfType<LaserButton>();
                        LightingButton noLight = FindObjectOfType<LightingButton>();
                        IceButton noIce = FindObjectOfType<IceButton>();
                        noLaser.OnRayOut();
                        noLight.OnRayOut();
                        noIce.OnRayOut();
                    }
                }
            }
        }
    }
}
