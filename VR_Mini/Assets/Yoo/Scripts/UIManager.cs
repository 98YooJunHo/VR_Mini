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
                if (m_instance == null)
                {
                    Debug.LogError("UIManager 찾지 못함");
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
    private const string GAME_START_UI = "GameStartUI";
    private const string GAME_OVER_UI = "GameOverUI";
    private const string HUD = "HUD";
    private const string SHOP_UI = "ShopUI";
    private const string SCORE_UI = "ScoreUI";
    private const string OCULUS_CAM = "CenterEyeAnchor";

    private GameObject gameStartUI;
    private GameObject gameOverUI;
    private GameObject hudObj;
    private GameObject shopUIObj;
    private GameObject scoreUIObj;

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

        gameStartUI = GameObject.Find(GAME_START_UI);
        gameOverUI = GameObject.Find(GAME_OVER_UI);
        hudObj = GameObject.Find(HUD);
        shopUIObj = GameObject.Find(SHOP_UI);
        scoreUIObj = GameObject.Find(SCORE_UI);

    }

    // Start is called before the first frame update
    void Start()
    {
        hudObj.transform.localScale = Vector3.zero;
        shopUIObj.transform.localScale = Vector3.zero;
        gameOverUI.transform.localScale = Vector3.zero;

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
        gameOverUI.transform.parent = GameObject.Find(OCULUS_CAM).transform;
        gameOverUI.transform.localPosition = Vector3.zero;
        gameOverUI.transform.localRotation = Quaternion.Euler(Vector3.zero);
        gameOverUI.transform.localScale = Vector3.zero;
    }

    public void Open_ShopUI()
    {
        shopUIObj.transform.localScale = Vector3.one * 0.8f;
        Close_ScoreUI();
        GameManager.Instance.shopOpen = true;
    }

    public void Close_ShopUI()
    {
        shopUIObj.transform.localScale = Vector3.zero;
        Open_ScoreUI();
        GameManager.Instance.shopOpen = false;
    }

    public void Open_ScoreUI()
    {
        scoreUIObj.transform.localScale = Vector3.one;
    }

    public void Close_ScoreUI()
    {
        scoreUIObj.transform.localScale = Vector3.zero;
    }

    public void Open_Hud()
    {
        hudObj.transform.localScale = Vector3.one;
    }

    public void Close_Hud()
    {
        hudObj.transform.localScale = Vector3.zero;
    }


    #endregion
}