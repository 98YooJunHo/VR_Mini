using UnityEngine;

public class DamageButton : ShopButtonOrigin_HHB
{

    public override void Init()
    {
        buyGold = ResourceManager.Instance.ChangeType<int>("int", ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.GOLD));
        coolTime = ResourceManager.Instance.ChangeType<float>("float", ResourceManager.Instance.GetSingleDataFromID(Order.ITEM2, Item.DURATION));
    }

    public override void Effect()
    {
        Player.instance.InforceWeapon();
        //Debug.Log("Dmg buyGold :" + buyGold);
        //Debug.Log("Dmg coolTime :" + coolTime);
        //Debug.Log("공격력증가");
        // 공격력 증대
    }
}
