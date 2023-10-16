using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterWeakPoint : MonsterHP
{
    public List<GameObject> ultWeakPoint; 
    public List<GameObject> skill_1WeakPoint;
    public List<GameObject> skill_2WeakPoint;

    private List<GameObject> activateWeakPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MakeWeakPoint()
    {
        for (int i = 0; i < 3; i++)
        {
            int a = Random.Range(1, 8);
            ultWeakPoint[a].SetActive(true);
        }
    }

    public bool BreakUp()
    {
        bool anyActive = activateWeakPoint.Any(obj => obj.activeSelf);
        if(!anyActive)
        {
            return true;
        }
        
        return false;

    }
}
