public class LaserButton : ItemButtonOrigin
{
    public override void Init() 
    {
        //Player.instance.userWeaponState = (int)WeaponState.LASER;
        explainTxt.text = ResourceManager.Instance.GetSingleDataFromID(Order.LASER_WEAPON, LASER_WEAPON.DESCRIPTION).ToString();
        nameTxt.text = ResourceManager.Instance.ChangeType<string>("string", ResourceManager.Instance.GetSingleDataFromID(Order.LASER_WEAPON, LASER_WEAPON.NAME));


        currentNameTxt.text = nameTxt.text;
        currentExplainTxt.text = explainTxt.text;


        coinObj.SetActive(false);
        isBought = true;

        weaponExplainImg.color = originalExplainColor;
        weaponNameImg.color = originalNameColor;
    }

    public override void Effect() 
    {
        //Player.instance.userWeaponState = (int)WeaponState.LASER;
    }

}
