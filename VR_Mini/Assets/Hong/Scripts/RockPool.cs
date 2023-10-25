using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{
    public static RockPool Instance;

    public GameObject[] rocks;
    public Mesh[] rockMeshes;

    private float randomScale;
    private float maxScale;
    private float minScale;

    private float y;
    private float rndX;
    private float rndZ;


    public Queue<GameObject> rockPool = new Queue<GameObject>();

    private void Start()
    {
        Instance = this;
        Init();
    }


    private void Init()
    {
        minScale = 6f;
        maxScale = 10f;
        for (int i = 0; i < 16; i++)
        {
            rockPool.Enqueue(CreateRocks(i));
        }

        for (int i = 0; i < 16; i++)
        { 
            GetRockObjectFromPooling();
        }


    }

    private GameObject CreateRocks(int i)
    {
        int j = i % 8;
        var rock = Instantiate(rocks[j]);        
        rock.name = "Rock";

        // 매쉬 랜덤 변경
        int rndMesh = Random.Range(0, 3);
        Mesh choosedMesh = rockMeshes[rndMesh];
        rock.GetComponent<MeshFilter>().mesh = choosedMesh;

        // 크기 랜덤변경
        randomScale = Random.Range(minScale, maxScale);
        rock.transform.localScale = rock.transform.localScale * randomScale;

        SetRockEnqueueTransform(rock);

        rock.SetActive(false);
        return rock;
    }

    private void SetRockEnqueueTransform(GameObject rock)
    {
        GameObject mother = GameObject.Find("RockPool");
        rock.transform.SetParent(mother.transform);
    }

    public void SetPosition(GameObject rocks)
    {
        if (randomScale > 8f)
        {
            if (Random.Range(0, 2) == 0)
            {
                rndX = Random.Range(-100f, -50f);
            }
            else
            { 
                rndX = Random.Range(50f, 100f);
            }
            rndZ = Random.Range(50f, 400f);
        }

        float rockScale = rocks.GetComponent<Transform>().localScale.x;
        y = rockScale * 0.001f;
        rocks.transform.position = new Vector3(rndX, y/0.6f + 1f, rndZ);
    }

    public void GetRockObjectFromPooling()
    {
        if (rockPool.Count > 0)
        {
            var rocks = rockPool.Dequeue();
            rocks.SetActive(true);
            SetPosition(rocks);
        }
        else 
        {
            int rnd = Random.Range(0,9);
            var rocks = CreateRocks(rnd);
            rocks.SetActive(true);
            SetPosition(rocks);
        }
    }
}
