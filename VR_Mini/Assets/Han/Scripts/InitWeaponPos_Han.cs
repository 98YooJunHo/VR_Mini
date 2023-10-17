using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitWeaponPos_Han : MonoBehaviour
{

    private GameObject gun1;
    private GameObject gun2;
    // Start is called before the first frame update
    void Start()
    {
        gun1 = GameObject.Find("Gun1");
        gun2 = GameObject.Find("Gun2");
        gun1.transform.SetParent(GameObject.Find("RightControllerAnchor").transform);
        gun2.transform.SetParent(GameObject.Find("RightControllerAnchor").transform);
        gun1.gameObject.transform.position = transform.position;
        gun2.gameObject.transform.position = transform.position;
        gun2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
