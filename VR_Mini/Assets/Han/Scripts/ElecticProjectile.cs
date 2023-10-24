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
        GameObject electricEffect = EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
        electricEffect.transform.position = transform.position;
        if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            // 보스 HP 깎는 코드
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            // 보스의 투사체일 경우 HP깎는 코드
        }
        ProjectilePool.instance.InsertProjectileQueue(gameObject, iceName);
    }
}
