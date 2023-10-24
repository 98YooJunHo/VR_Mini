using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonOrigin : MonoBehaviour
{
    #region 변수

    #region 무기버튼
    // 설명, 이름, 배경, 아이콘, 골드 이미지
    protected Image weaponExplainImg;
    protected Image weaponNameImg;
    protected Image weaponBackGroundImg;
    protected Image weaponIcon;
    protected GameObject coinObj;

    // 설명, 이름 Txt
    protected TextMeshProUGUI explainTxt;
    protected TextMeshProUGUI nameTxt;
    // 비활성화 Txt
    protected string lockedTxt;
    protected TextMeshProUGUI coinTxt;

    // 설명, 이름, 배경의 원래 색
    protected Color originalExplainColor;
    protected Color originalNameColor;
    protected Color originalBackGroundColor;

    // 선택, 비활성화 색
    protected Color selectedColor;
    protected Color disabledColor;

    // 구매했는지, 구매비용 변수
    protected bool isBought;
    protected int buyGold;
    #endregion

    #region 장착무기
    // 현재 장착중인 이미지
    protected GameObject currentWeapon;
    protected Image currentWeaponImg;
    protected TextMeshProUGUI currentExplainTxt;
    protected TextMeshProUGUI currentNameTxt;
    #endregion


    #endregion

    public virtual void Awake()
    {
        weaponExplainImg = this.gameObject.transform.Find("WeaponExplain").gameObject.GetComponent<Image>();
        weaponNameImg = this.gameObject.transform.Find("WeaponName").gameObject.GetComponent<Image>();
        weaponBackGroundImg = this.gameObject.transform.Find("BackGround").gameObject.GetComponent<Image>();

        explainTxt = this.gameObject.transform.Find("WeaponExplain").Find("WeaponExplainTxt").gameObject.GetComponent<TextMeshProUGUI>();
        nameTxt = this.gameObject.transform.Find("WeaponName").Find("WeaponNameTxt").gameObject.GetComponent<TextMeshProUGUI>();

        coinObj = this.gameObject.transform.Find("Coin").gameObject;
        coinTxt = coinObj.transform.Find("CoinTxt").gameObject.GetComponent<TextMeshProUGUI>();

        originalExplainColor = weaponExplainImg.color;
        originalNameColor = weaponNameImg.color;
        originalBackGroundColor = weaponBackGroundImg.color;

        selectedColor = new Color(0f / 255f, 139f / 255f, 253f / 255f);
        disabledColor = new Color(191f / 255f, 191f / 255f, 191f / 255f);

        weaponIcon = this.gameObject.transform.Find("WeaponIcon").gameObject.GetComponent<Image>();
        currentWeapon = GameObject.Find("CurrentWeapon");
        currentWeaponImg = currentWeapon.transform.Find("WeaponIcon").gameObject.GetComponent<Image>();
        currentExplainTxt = currentWeapon.transform.Find("WeaponExplain").Find("WeaponExplainTxt").gameObject.GetComponent<TextMeshProUGUI>();
        currentNameTxt = currentWeapon.transform.Find("WeaponName").Find("WeaponNameTxt").gameObject.GetComponent<TextMeshProUGUI>();
        lockedTxt = "LOCKED";
        Init();
    }

    // 기본무기제외) 처음에 비활성화색으로 Init, 골드 Init
    public virtual void Init() { }

    protected bool CheckMoney()
    {
        int userGold = GameManager.Instance.gold;
        if (userGold - buyGold >= 0)
        {
            return true;
        }
        else { return false; }
    }

    // 효과 userWeaponState 교체
    public virtual void Effect() { }

    // LTouch button 구매
    // Ray IPointerClickHandler
    public void OnRayClick()
    {
        // 구매하지 않은 경우
        if (!isBought && CheckMoney())
        {
            // 골드차감후, 산 상태로 바꿈
            GameManager.Instance.Use_Gold(buyGold);
            HUD_Canvas_Yoo hUD_Canvas_Yoo = FindObjectOfType<HUD_Canvas_Yoo>();
            hUD_Canvas_Yoo.UpdateShopGold(GameManager.Instance.gold.ToString());
            isBought = true;
            // unlocked -> 설명문으로 교체
            UnLockWeaponText();
            // 골드 숨기기
            coinObj.SetActive(false);
        }
        // 구매한 경우
        else if (isBought) 
        {
            // 설명과 이름을 선택색으로 바꿈
            weaponExplainImg.color = selectedColor;
            weaponNameImg.color = selectedColor;
            // 현재 보유무기 이미지를 현재껄로 교체
            currentWeaponImg.sprite = weaponIcon.sprite;
            // 이름, 설명 바꾸기
            currentExplainTxt.text = explainTxt.text;
            currentNameTxt.text = nameTxt.text;
            Effect();
        }
    }

    public virtual void UnLockWeaponText() {}

    // Ray IPointerEnterHandler
    public void OnRayIn()
    {
        // 배경 켜기
        weaponBackGroundImg.gameObject.SetActive(true);
        // 구매하지않은 경우
        if (!isBought)
        {
            // 구매할 돈이 있는경우, 배경을 선택색으로
            if (CheckMoney()) { weaponBackGroundImg.color = selectedColor; }
            // 구매할 돈이 없는 경우, 배경을 비활성화색으로
            else { weaponBackGroundImg.color = disabledColor; }
        }
        // 구매한 경우, 배경을 선택색으로
        else if (isBought) { weaponBackGroundImg.color = selectedColor; }
    }

    // Ray IPointerExitHandler
    public void OnRayOut()
    {
        // 배경 끄기
        weaponBackGroundImg.gameObject.SetActive(false);
        // 구매한 경우 원래색으로
        if (isBought) { weaponNameImg.color = originalNameColor; weaponExplainImg.color = originalExplainColor; }
        // 구매안한 경우 비활성화 색으로
        else { weaponExplainImg.color = disabledColor; weaponNameImg.color = disabledColor; }
    }
}
