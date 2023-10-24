public class LightingButton : ItemButtonOrigin
{
    public override void Init()
    {
        buyGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.BUY_GOLD);
        coinTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.BUY_GOLD).ToString();
        nameTxt.text = ResourceManager.Instance.ChangeType<string>("string", ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.NAME));
        explainTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.DESCRIPTION).ToString();

        isBought = false;

        weaponExplainImg.color = disabledColor;
        weaponNameImg.color = disabledColor;

        explainTxt.text = lockedTxt;

    }

    public override void UnLockWeaponText()
    {
        explainTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.DESCRIPTION).ToString();
    }

    public override void Effect() 
    {
        //Player.instance.userWeaponState = (int)WeaponState.LIGHTING;
    }

}
