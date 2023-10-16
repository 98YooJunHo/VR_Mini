using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageButton : ShopButtonOrigin_HHB
{
    public override void Init()
    {
        buyGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.GOLD);
        Debug.Log("Dmg buyGold :" + buyGold);
        coolTime = (float)(int)ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.DURATION);
        Debug.Log("Dmg coolTime :" + coolTime);

    }
}
