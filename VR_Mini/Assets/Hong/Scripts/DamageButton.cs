using UnityEngine;

public class DamageButton : ShopButtonOrigin_HHB
{

    public override void Init()
    {
        buyGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.GOLD);
        coolTime = ResourceManager.Instance.ChangeType<float>("float", ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.DURATION));
    }

    public override void Effect()
    { 
        //Debug.Log("Dmg buyGold :" + buyGold);
        //Debug.Log("Dmg coolTime :" + coolTime);
        //Debug.Log("공격력증가");
        // 공격력 증대
    }
}
