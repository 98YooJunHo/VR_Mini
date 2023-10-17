using UnityEngine;

public class WeakButton : ShopButtonOrigin_HHB
{

    public override void Init()
    {
        buyGold = ResourceManager.Instance.ChangeType<int>("int", ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.GOLD));
        coolTime = ResourceManager.Instance.ChangeType<float>("float", ResourceManager.Instance.GetSingleDataFromID(Order.ITEM1, Item.GOLD));
    }

    public override void Effect()
    {
        //Debug.Log("Weak buyGold :" + buyGold);
        //Debug.Log("Weak coolTime :" + coolTime);
        //Debug.Log("약점버튼");
        // 약점확대
    }
}
