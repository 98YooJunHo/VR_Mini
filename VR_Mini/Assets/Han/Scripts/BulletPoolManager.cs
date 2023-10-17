using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager instance;

    public GameObject normalEffectPrefab;

    public GameObject inforceEffectPrefab;

    public Queue<GameObject> normalQueue = new Queue<GameObject>();

    public Queue<GameObject> inforceQueue = new Queue<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i=0; i< 50; i++)
        {
            GameObject normalEffect = Instantiate(normalEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            normalEffect.transform.SetParent(gameObject.transform);
            normalQueue.Enqueue(normalEffect);
            normalEffect.SetActive(false);
        }
        for (int i = 0; i < 50; i++)
        {
            GameObject inforceEffect = Instantiate(inforceEffectPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            inforceEffect.transform.SetParent(gameObject.transform);
            inforceQueue.Enqueue(inforceEffect);
            inforceEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertQueue(GameObject gameobject, int weaponNum)
    {
        if (weaponNum == 1)
        {
            normalQueue.Enqueue(gameObject);
            gameobject.SetActive(false);
        }
        else if (weaponNum ==2)
        {
            inforceQueue.Enqueue(gameObject);
            gameobject.SetActive(false);
        }
    }

    public GameObject GetQueue(int weaponNum)
    {
        GameObject gameObject =default;
        if (weaponNum == 1)
        {
            gameObject= normalQueue.Dequeue();
            gameObject.SetActive(true);
        }
        else if (weaponNum ==2)
        {
            gameObject = inforceQueue.Dequeue();
            gameObject.SetActive(true);
        }
        Debug.Log("겟큐는 잘작동하고있어");
        return gameObject;
    }
}
