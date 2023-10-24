using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
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

    private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.DMG);
        chargeDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_PER_DMG);
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
        originDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.DMG);
    }

    public void OnTriggerEnter(Collider other)
    {
        GoBackToQueue();
        GameObject gameObject=EffectPoolManager.instance.GetQueue(Player.instance.userWeaponState);
        gameObject.transform.position = transform.position;
        if(other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            //Todo: 피감소하는 함수
            
        }
        else if(other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            //TOdo: 피감소하는 함수
        }
    }

    private void GoBackToQueue()
    {
        gameObject.transform.localScale = Vector3.one;
        ProjectilePool.instance.InsertProjectileQueue(this.gameObject, projectileName);

    }
}
