using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager instance;

    public GameObject bulletPrefab;

    public Queue<GameObject> bulletQueue = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        for (int i=0; i< 100; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, new Vector3(0f, -3f, 0f), Quaternion.identity);
            bulletQueue.Enqueue(bullet);
            bullet.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InsertQueue(GameObject gameobject)
    {
        Rigidbody rb = gameobject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        bulletQueue.Enqueue (gameObject);
        gameobject.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject gameObject = bulletQueue.Dequeue();
        gameObject.SetActive(true);
        return gameObject;
    }
}
