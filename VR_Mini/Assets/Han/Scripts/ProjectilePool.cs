using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;

    public GameObject electricProjectilePrefab;

    public GameObject iceProjectilePrefab;

    public Queue<GameObject> electricProjectileQueue = new Queue<GameObject>();
    
    public Queue<GameObject> iceProjectileQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < 50; i++)
        {
            GameObject electricProjectile = Instantiate(electricProjectilePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            electricProjectile.transform.SetParent(gameObject.transform);
            electricProjectileQueue.Enqueue(electricProjectile);
            electricProjectile.SetActive(false);
        }

        for (int i = 0; i < 50; i++)
        {
            GameObject iceProjectile = Instantiate(iceProjectilePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
            iceProjectile.transform.SetParent(gameObject.transform);
            iceProjectileQueue.Enqueue(iceProjectile);
            iceProjectile.SetActive(false);
        }
    }

    // Update is called once per frame

    public void InsertProjectileQueue(GameObject gameobject, string projectileName)
    {
        if (projectileName == "electricProjectile")
        {
            electricProjectileQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if (projectileName == "iceProjectile")
        {
            iceProjectileQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
    }

    public GameObject GetQueue(int userWeaponState)
    {
        GameObject  projectile = default;
        if (userWeaponState == (int)WeaponState.LIGHTING)
        {
            if(electricProjectileQueue.Count <50)
            {
                while(electricProjectileQueue.Count<=50)
                {
                    GameObject electricProjectile = Instantiate(electricProjectilePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    electricProjectile.transform.SetParent(gameObject.transform);
                    electricProjectileQueue.Enqueue(electricProjectile);
                    electricProjectile.SetActive(false);
                }
            }
            projectile = electricProjectileQueue.Dequeue();
            projectile.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.ICE)
        {
            if (iceProjectileQueue.Count < 50)
            {
                while (iceProjectileQueue.Count <= 50)
                {
                    GameObject iceProjectile = Instantiate(iceProjectilePrefab, new Vector3(0f, 0f, 0f), Quaternion.identity);
                    iceProjectile.transform.SetParent(gameObject.transform);
                    iceProjectileQueue.Enqueue(iceProjectile);
                    iceProjectile.SetActive(false);
                }
            }
            projectile = iceProjectileQueue.Dequeue();
            projectile.SetActive(true);
        }
        return projectile;
    }
}
