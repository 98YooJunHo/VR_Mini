using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHP : MonoBehaviour
{
    private int iD;
    private string description;
    public float weakPointRate;
    private int actTime;
    public int hp;
    private float moveSpeed;
    private ResourceManager rm;

    // Start is called before the first frame update
    void Start()
    {
        //hp = (int)rm.GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        hp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
