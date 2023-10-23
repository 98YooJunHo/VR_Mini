using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Player;

public class EffectPoolManager : MonoBehaviour
{
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

    public void InsertEffectQueue(GameObject gameobject, int userWeaponState )
    {
        if (userWeaponState == (int)WeaponState.LASER)
        {
            laserEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if (userWeaponState == (int)WeaponState.LIGHTING)
        {
            electricEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
        else if (userWeaponState == (int)WeaponState.ICE)
        {
            iceEffectQueue.Enqueue(gameobject);
            gameobject.SetActive(false);
        }
    }

    public GameObject GetQueue(int userWeaponState)
    {
        GameObject gameObject =default;
        if (userWeaponState == (int)WeaponState.LASER)
        {
            gameObject = laserEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.LIGHTING)
        {
            gameObject = electricEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (userWeaponState == (int)WeaponState.ICE)
        {
            gameObject = iceEffectQueue.Dequeue();
            gameObject.SetActive(true);
        }
        return gameObject;
    }
}
