using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InforceEffect : MonoBehaviour
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
        BulletPoolManager.instance.InsertQueue(gameObject, 2);
    }
}
