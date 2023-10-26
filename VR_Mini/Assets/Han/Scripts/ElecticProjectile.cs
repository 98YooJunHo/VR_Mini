using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecticProjectile : MonoBehaviour
{
    public string iceName  = "ElectricProjectile";
    public int damage = default;
    public float speed = 0f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        damage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.DMG);
        //speed = (float)ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.ATTACK_SPEED);
        speed = 100;
    }

    private void OnEnable()
    {
        rb.velocity = ARAVRInput.RHandDirection * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            GameObject electricEffect = EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
            electricEffect.transform.position = transform.position;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("WeakPoint"))
        {
            GameObject electricEffect = EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
            electricEffect.transform.position = transform.position;
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            GameObject electricEffect = EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
            electricEffect.transform.position = transform.position;
        }

        ProjectilePool.instance.InsertProjectileQueue(gameObject, iceName);
    }
}
