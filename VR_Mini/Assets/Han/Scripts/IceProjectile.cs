using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.PackageManager;
using UnityEditor.Rendering;
using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public string projectileName = "iceProjectile";
    public int originDMG = default;
    public int chargeDMG = default;
    public int currentDMG = default;
    public int speed = default;
    public Transform currentScale = default;

    private int iceGold;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.DMG);
        chargeDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_PER_DMG);

        iceGold = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.HIT_GOLD);

        //speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.PROJECTILE_SPEED);
        speed = 100;
        projectileName = "iceProjectile";
    }

    private void OnEnable()
    {
        currentScale = gameObject.transform;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void shot()
    {
        rb.velocity = ARAVRInput.RHandDirection * speed;
    }

    public void OnTriggerEnter(Collider other)
    {
        
        GameObject gameObject=EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
        gameObject.transform.position = transform.position;
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            //Todo: 피감소하는 함수
            MonsterHP.Instance.OnDamage(currentDMG,iceGold, transform.position);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WeakPoint"))
        {
            if (other.gameObject.name == "Cylinder(Clone)")
            {
                Missile_Kim missile = other.transform.GetComponent<Missile_Kim>();
                missile.OnDamage(currentDMG);
            }

            if (other.gameObject.name == "WeakPoint")
            {
                DamagedPoint dmgPoint = other.transform.GetComponent<DamagedPoint>();
                dmgPoint.OnDamage(currentDMG);
            }
                //TOdo: 피감소하는 함수
        }
            GoBackToQueue();
    }

    private void GoBackToQueue()
    {
        gameObject.transform.localScale = Vector3.one;
        ProjectilePool.instance.InsertProjectileQueue(this.gameObject, projectileName);

    }
}
