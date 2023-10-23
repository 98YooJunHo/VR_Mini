using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamRotation_Yoo : MonoBehaviour
{
    private const string OCULUS_CAM = "CenterEyeAnchor";
    private GameObject centerEyeObj;
    // Start is called before the first frame update
    void Start()
    {
        centerEyeObj = GameObject.Find(OCULUS_CAM);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.gameOver)
        {
            transform.rotation = centerEyeObj.transform.rotation;
        }
    }
}
