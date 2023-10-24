using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class EffectPoolManager : MonoBehaviour
{
    const string laserName = "LaserEffect(Clone)";

    const string electricName = "ElectricEffect(Clone)";

    const string iceName = "IceEffect(Clone)";

    public static EffectPoolManager instance;

    public GameObject laserEffectPrefab;

    public GameObject electricEffectPrefab;

    public GameObject iceEffectPrefab;

    public Queue<GameObject> laserEffectQueue = new Queue<GameObject>();

    public Queue<GameObject> electricEffectQueue = new Queue<GameObject>();

    public Queue<GameObject> iceEffectQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i=0; i< 50; i++)
        {
            GameObject laserEffect = Instantiate(laserEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            laserEffect.transform.SetParent(gameObject.transform);
            laserEffectQueue.Enqueue(laserEffect);
            laserEffect.SetActive(false);
        }
        for (int i = 0; i < 50; i++)
         {
             GameObject electricEffect = Instantiate(electricEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
             electricEffect.transform.SetParent(gameObject.transform);
             electricEffectQueue.Enqueue(electricEffect);
             electricEffect.SetActive(false);
         }
         for (int i = 0; i < 50; i++)
         {
             GameObject iceEffect = Instantiate(iceEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
             iceEffect.transform.SetParent(gameObject.transform);
             iceEffectQueue.Enqueue(iceEffect);
             iceEffect.SetActive(false);
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertEffectQueue(GameObject gameobject )
    {
        if (gameobject.name == laserName)
        {
            laserEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if (gameobject.name == electricName)
        {
            electricEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if (gameobject.name == iceName)
        {
            iceEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
    }

    public GameObject GetQueue(int userWeaponState)
    {
        GameObject gameObject = default;
        if (userWeaponState == (int)WeaponState.LASER)
        {
            if(laserEffectQueue.Count<50)
            {
                while(laserEffectQueue.Count<=50)
                {
                    GameObject laserEffect = Instantiate(laserEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
                    laserEffect.transform.SetParent(gameObject.transform);
                    laserEffectQueue.Enqueue(laserEffect);
                }
            }
            gameObject = laserEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.LIGHTING)
        {
            if (electricEffectQueue.Count < 50)
            {
                while (electricEffectQueue.Count <= 50)
                {
                    GameObject electricEffect = Instantiate(electricEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
                    electricEffect.transform.SetParent(gameObject.transform);
                    electricEffectQueue.Enqueue(electricEffect);
                }
            }
            gameObject = electricEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.ICE)
        {
            if (iceEffectQueue.Count < 50)
            {
                while (iceEffectQueue.Count <= 50)
                {
                    GameObject iceEffect = Instantiate(iceEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
                    iceEffect.transform.SetParent(gameObject.transform);
                    iceEffectQueue.Enqueue(iceEffect);
                }
            }
            gameObject = iceEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        return gameObject;
    }
}
