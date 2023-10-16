using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance
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
    private GameObject gameStartUI;
    private GameObject gameOverUI;
    private GameObject hudObj;
    private GameObject shopUIObj;

    private void Awake()
    {
        gameStartUI = GameObject.Find("GameStartUI");
        gameOverUI = GameObject.Find("GameOverUI");
        hudObj = GameObject.Find("HUD");
        shopUIObj = GameObject.Find("ShopUI");
    }

    // Start is called before the first frame update
    void Start()
    {
        hudObj.transform.localScale = Vector3.zero;
        shopUIObj.transform.localScale = Vector3.zero;
        gameOverUI.transform.localScale = Vector3.zero;

        //Invoke("Open_Shop", 3f);
        //Invoke("Close_Shop", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open_GameStartUI()
    {
        gameStartUI.transform.localScale = Vector3.one;
    }

    public void Close_GameStartUI()
    {
        gameStartUI.transform.localScale = Vector3.zero;
    }

    public void Open_GameOverUI()
    {
        gameOverUI.transform.parent = null;
        gameOverUI.transform.localScale = Vector3.one;
    }

    public void Close_GameOverUI()
    {
        gameOverUI.transform.parent = GameObject.Find("CenterEyeAnchor").transform;
        gameOverUI.transform.localScale = Vector3.zero;
    }

    public void Open_Shop()
    {
        shopUIObj.transform.localScale = Vector3.one;
    }

    public void Close_Shop()
    {
        shopUIObj.transform.localScale = Vector3.zero;
    }

    public void Open_Hud()
    {
        hudObj.transform.localScale = Vector3.one;
    }

    public void Close_Hud()
    {
        hudObj.transform.localScale = Vector3.zero;
    }
}