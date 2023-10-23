using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricProjectile : MonoBehaviour
{
    public string projectileName = "electricProjectile";
    public int originDMG = (int)ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.DMG);
    public int speed = (int)ResourceManager.Instance.GetSingleDataFromID(Order.LIGHTING_WEAPON, LIGHTING_WEAPON.ATTACK_SPEED);
    private Rigidbody rb;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        rb.velocity = ARAVRInput.RHandDirection * speed;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            //Todo: 피감소하는 함수
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Projectile"))
        {
            //TOdo: 피감소하는 함수
        }
    }

    private void GoBackToQueue()
    {
        ProjectilePool.instance.InsertProjectileQueue(this.gameObject, projectileName);
    }
}