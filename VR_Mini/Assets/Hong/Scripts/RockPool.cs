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

    private Vector3 spwanPosition;

    private Queue<GameObject> rockPool;

    private void Start()
    {
        Instance = this;
        Init();
    }


    private void Init()
    {
        for (int i = 0; i < 16; i++)
        {
            rockPool.Enqueue(CreateRocks(i));
        }

    }

    private GameObject CreateRocks(int i)
    {
        i = i % 8;
        var rock = Instantiate(rocks[i]);
        rock.name = "Rock";
        Mesh rndMesh = rock.GetComponent<Mesh>();
        //int rndMesh = Random.Range();
        //rndMesh = rockMeshes[]
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
            SetPosition(rocks);
            rocks.SetActive(true);
        }
    }
}
