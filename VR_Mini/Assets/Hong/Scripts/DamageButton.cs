using UnityEngine;

public class DamageButton : ShopButtonOrigin_HHB
{

    public override void Init()
    {
     
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
