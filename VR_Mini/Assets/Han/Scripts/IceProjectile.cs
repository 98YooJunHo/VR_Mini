using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class IceProjectile : MonoBehaviour
{
    public string projectileName = "iceProjectile";
    public int originDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.DMG);
    public int chargeDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_PER_DMG);
    public int currentDMG = default;
    public int speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.PROJECTILE_SPEED);
    public Transform currentScale = default;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentScale = gameObject.transform;
        projectileName = "iceProjectile";
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
