using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LaserEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("ReturnQueue",0.1f);
    }

    void Start()
    {
        
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
