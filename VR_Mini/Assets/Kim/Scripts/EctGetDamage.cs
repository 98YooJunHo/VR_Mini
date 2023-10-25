using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EctGetDamage : MonoBehaviour, IDamagable
{
    public float hp;
    // Start is called before the first frame update
  
    public void OnDamage(float damage)
    {
        hp -= damage;
    }

}
