using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hit : MonoBehaviour
{
    private Image hitImage;

    // Start is called before the first frame update
    void Start()
    {
        hitImage = GameObject.Find("hitImage").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
