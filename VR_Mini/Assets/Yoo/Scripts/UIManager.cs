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
                if(m_instance == null)
                {
                    GameObject uiManager = new GameObject("UIManager");
                    uiManager.AddComponent<UIManager>();
                }
            }
            return m_instance;
        }
    }

    public static UIManager m_instance;

    #region !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse !HowToUse
    /* !게임의 HUD, ShopUI, GameStartUI, GameOverUI를 열고 닫는 것을 관리하는 UIManager입니다.
     * 각 UI들은 Open_xxxUI, Close_xxxUI 함수를 이용해 열고 닫을 수 있습니다. (HUD의 경우 Open_HUD, Close_HUD)
     */
    #endregion

    #region Variable
    private GameObject gameStartUI;
    private GameObject gameOverUI;
    private GameObject hudObj;
    private GameObject shopUIObj;

    //{ 10/17 홍한범
    public TextMeshProUGUI weakGoldTxt;
    public TextMeshProUGUI weakTimeTxt;
    public TextMeshProUGUI attGoldTxt;
    public TextMeshProUGUI attTimeTxt;
    //} 10/17 홍한범
    
    #endregion

    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(this);
            }
        }

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

        //{ 10/17 홍한범
        PrintItemText();
        //} 10/17 홍한범
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Function
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
        gameOverUI.transform.localPosition = Vector3.zero;
        gameOverUI.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gameOverUI.transform.localScale = Vector3.zero;
    }

    public void Open_ShopUI()
    {
        shopUIObj.transform.localScale = Vector3.one * 0.8f;
    }

    public void Close_ShopUI()
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

    //{ 10/17 홍한범
    public void PrintItemText()
    {
         weakGoldTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.GOLD).ToString();
         weakTimeTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.DURATION).ToString();
         attGoldTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.GOLD).ToString();
         attTimeTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.DURATION).ToString();
    }


    #endregion
}