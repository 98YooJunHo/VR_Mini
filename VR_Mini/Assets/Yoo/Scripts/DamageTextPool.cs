using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextPool : MonoBehaviour
{
    public static DamageTextPool Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DamageTextPool>();
                if(m_instance == null)
                {
                    Debug.LogError("데미지 텍스트 풀 찾지 못함");
                }
            }
            return m_instance;
        }
    }
    
    private static DamageTextPool m_instance;

    public GameObject damageTextPrefab;

    private GameObject tempObj;
    private Queue<GameObject> pool = new Queue<GameObject>();
    private Vector3 damageOriginScale;
    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            if(m_instance != this)
            {
                Destroy(this);
            }
        }

        damageOriginScale = damageTextPrefab.transform.localScale;
    }
    // Start is called before the first frame update
    void Start()
    {
        ChargePool();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChargePool()
    {
        for (int i = 0; i < 25; i++)
        {
            tempObj = Instantiate(damageTextPrefab);
            tempObj.transform.parent = transform;
            pool.Enqueue(tempObj);
        }
    }

    public GameObject Get()
    {
        if(pool.Count == 0)
        {
            ChargePool();
        }
        return pool.Dequeue();
    }

    public void Set(GameObject obj)
    {
        obj.transform.parent = null;
        obj.transform.localScale = damageOriginScale;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.parent = transform;
        pool.Enqueue(obj);
        obj.SetActive(false);
    }
}
