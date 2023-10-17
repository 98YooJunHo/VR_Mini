using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEffect : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Makefalse", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Makefalse()
    {
        EffectPoolManager.instance.InsertQueue(this.gameObject,1);
    }
}
