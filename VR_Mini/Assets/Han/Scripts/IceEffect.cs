using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("ReturnQueue", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnQueue()
    {
        EffectPoolManager.instance.InsertEffectQueue(this.gameObject);

    }
}
