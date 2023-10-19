using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armageddon : MonoBehaviour
{
    private float speed = 30.0f;
    public Transform targetLocation;
    private bool doOnce = false;
   
    // Update is called once per frame
    void Update()
    {
        if (!doOnce)
        {
            transform.localPosition = new Vector3 (0, 0, 0);
            doOnce = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetLocation.position, speed * Time.deltaTime);
        if (transform.position.y < 1)   // y 값 수정해야됨
        {
            doOnce= false;
            Debug.Log("1");
            transform.gameObject.SetActive(false);
        }
    }
}
