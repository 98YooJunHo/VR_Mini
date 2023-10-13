using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    public static UIManager m_instance;
    private GameObject hudCanvasObj;
    private GameObject shopUICanvasObj;
    private Image playerHpImg;
    private Image bossHpImg;

    private void Awake()
    {
        hudCanvasObj = GameObject.Find("HUD").transform.GetChild(0).gameObject;
        shopUICanvasObj = GameObject.Find("ShopUI").transform.GetChild(0).gameObject;
        playerHpImg = hudCanvasObj.transform.GetChild(0).GetComponent<Image>();
        bossHpImg = hudCanvasObj.transform.GetChild(1).GetComponent <Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hudCanvasObj.SetActive(false);
        shopUICanvasObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open_Shop()
    {
        shopUICanvasObj.SetActive(true);
    }

    public void Close_Shop()
    {
        shopUICanvasObj.SetActive(false);
    }

    public void Open_Hud()
    {
        hudCanvasObj.SetActive(true);
    }

    public void Close_Hud()
    {
        hudCanvasObj.SetActive(false);
    }
}
