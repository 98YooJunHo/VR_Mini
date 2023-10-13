using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public Monster monster;
    private Animator animator;
    private float speed = 3.2f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        monster = transform.GetComponent<Monster>();
    }
       
    // Update is called once per frame
    void Update()
    {
        
    }
}
