using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

[RequireComponent(typeof(Button))]
public class ShopButtonOrigin_HHB : MonoBehaviour
{
    protected Button button;
    protected Image iconImg;
    protected Image buttonImg;
    protected Color originalIconColor;
    protected Color originalButtonlColor;
    protected Color selectedColor;
    protected Color disabledColor;
    protected int shopLayer;
    protected bool isClicked;
    protected float coolTime;
    protected int buyGold;


    public void Start()
    {
        shopLayer = 1 << LayerMask.NameToLayer("ShopLayer");
        button = GetComponent<Button>();
        iconImg = GetComponentInChildren<Image>();
        buttonImg = GetComponent<Image>();
        originalIconColor = iconImg.color;
        originalButtonlColor = buttonImg.color;
        selectedColor = Color.white;
        disabledColor = new Color(55f, 47f, 47f);
        Init();
    }

    public virtual void Init() { }

    public virtual void PackShopRay(RaycastHit hitInfo)
    {
        OnRayIn(hitInfo);
        OnRayOut(hitInfo);
    }



    public virtual bool CheckMoneyAndCoolTime()
    {
        int userGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        if ((userGold - buyGold >= 0) && isClicked)
        {
            userGold -= buyGold;
            // 돌려주기 

            return true;
        }
        else { return false; }
    }

    public virtual void RunCoolTime()
    {
        StartCoroutine(StartCoolTime());
    }

    IEnumerator StartCoolTime()
    {
        float startTime = 0f;
        while (startTime < coolTime)
        {
            startTime += Time.deltaTime;
            yield return null;
        }
        button.enabled = true;
        isClicked = false;
        buttonImg.color = originalButtonlColor;
        iconImg.color = originalIconColor;
    }

    // LTouch button 구매
    public virtual void OnRayClick(RaycastHit hitInfo)
    {
        if (isClicked && CheckRayName(hitInfo) && CheckMoneyAndCoolTime())
        {
            button.enabled = false;
            isClicked = true;
            buttonImg.color = disabledColor;
            iconImg.color = disabledColor;
            RunCoolTime();
        }
        else { /*Do Nothing*/ }
    }

    public virtual void OnRayIn(RaycastHit hitInfo)
    {
        if (isClicked && CheckRayName(hitInfo))
        {
            iconImg.color = selectedColor;
        }
        else { /*Do Nothing*/ }
    }

    public virtual void OnRayOut(RaycastHit hitInfo)
    {
        if (isClicked && !CheckRayName(hitInfo))
        {
            iconImg.color = originalIconColor;
        }
        else { /*Do Nothing*/ }
    }

    public virtual bool CheckRayName(RaycastHit hitInfo)
    {
        if (hitInfo.transform.gameObject.layer == shopLayer)
        {
            return true;
        }
        else { return false; }
    }
}
