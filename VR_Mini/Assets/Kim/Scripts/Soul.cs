using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public Transform wayPoint;
    public bool reset = false;
    private float speed = 20.0f;

    public float wayTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!reset)
        {
            //Debug.Log("!");
            transform.position = new Vector3(0, 0, 0);
            WayTime();
            Debug.Log(wayTime);

            reset = true;
        }
        transform.position = Vector3.MoveTowards(transform.position, wayPoint.position, speed * Time.deltaTime);

        
    }

    public void WayTime()
    {

        Vector3 far = transform.position - wayPoint.position;
        float distance = far.magnitude;

        wayTime = distance / speed;

    }
}
