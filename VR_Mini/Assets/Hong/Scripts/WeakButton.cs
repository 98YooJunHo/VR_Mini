using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakButton : ShopButtonOrigin_HHB
{
    public override void Init()
    {
        buyGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.GOLD);
        Debug.Log("Weak buyGold :" + buyGold);
        coolTime = (float)(int)ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.DURATION);
        Debug.Log("Weak coolTime :" +coolTime);
    }
}
