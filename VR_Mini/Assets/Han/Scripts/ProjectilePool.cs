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
        for (int i = 0; i < 50; i++)
        {
            GameObject electricProjectile = Instantiate(electricProjectilePrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            electricProjectile.transform.SetParent(gameObject.transform);
            electricProjectileQueue.Enqueue(electricProjectile);
            electricProjectile.SetActive(false);
        }
        for (int i = 0; i < 50; i++)
        {
            GameObject iceProjectile = Instantiate(iceProjectilePrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            iceProjectile.transform.SetParent(gameObject.transform);
            iceProjectileQueue.Enqueue(iceProjectile);
            iceProjectile.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        GameObject gameObject = default;
        if (userWeaponState == (int)WeaponState.LIGHTING)
        {
            gameObject = electricProjectileQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.ICE)
        {
            gameObject = iceProjectileQueue.Dequeue();
            gameObject.SetActive(true);
        }
        return gameObject;
    }
}