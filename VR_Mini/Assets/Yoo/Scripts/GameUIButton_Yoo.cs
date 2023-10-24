using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIButton_Yoo : MonoBehaviour
{
    private Color greenColor = new Color(122 / 255f, 255 / 255f, 122 / 255f);

    private Image buttonImg;
    private Color originButtonColor;
    private Color targetButtonColor;

    private void Awake()
    {
        buttonImg = GetComponent<Image>();
        originButtonColor = buttonImg.color;
        targetButtonColor = greenColor;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnRayIn()
    {
        buttonImg.color = targetButtonColor;
    }

    public void OnRayOut()
    {
        buttonImg.color = originButtonColor;
    }
}
