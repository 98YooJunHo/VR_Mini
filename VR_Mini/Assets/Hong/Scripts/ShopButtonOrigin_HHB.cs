using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonOrigin_HHB : MonoBehaviour
{
    protected Image iconImg;
    protected Image buttonImg;
    protected Color originalIconColor;
    protected Color originalButtonlColor;
    protected Color selectedColor;
    protected Color disabledColor;
    protected bool isClicked;
    protected float coolTime;
    protected int buyGold;

    public virtual void Awake()
    {
        isClicked = false;
        iconImg = GetComponentInChildren<Image>();
        buttonImg = GetComponent<Image>();
        originalIconColor = iconImg.color;
        originalButtonlColor = buttonImg.color;
        selectedColor = new Color(122f / 255f, 255f / 255f, 122f / 255f);
        disabledColor = new Color(255f / 255f, 0f / 255f, 25f / 255f);
        Init();
    }

    public virtual void Init() { }

    public virtual bool CheckMoneyAndCoolTime()
    {
        int userGold = GameManager.Instance.gold;
        if ((userGold - buyGold >= 0) && isClicked)
        {
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
        Effect();
        float startTime = 0f;
        while (startTime < coolTime)
        {
            buttonImg.fillAmount = startTime / coolTime;
            startTime += Time.deltaTime;
            yield return null;
        }
        buttonImg.fillAmount = 1f;
        isClicked = false;
        buttonImg.color = originalButtonlColor;
        iconImg.color = originalIconColor;
    }

    public virtual void Effect() { }

    // LTouch button 구매
    public virtual void OnRayClick()
    {
        if (!isClicked/* && CheckMoneyAndCoolTime()*/)
        {
            GameManager.Instance.Use_Gold(buyGold);
            isClicked = true;
            buttonImg.color = disabledColor;
            iconImg.color = disabledColor;
            RunCoolTime();
        }
    }

    public virtual void OnRayIn()
    {
        if (isClicked == false)
        {
            buttonImg.color = selectedColor;
        }
    }

    public virtual void OnRayOut()
    {
        if (isClicked  == false)
        {
            buttonImg.color = originalButtonlColor;
        }
    }
}
