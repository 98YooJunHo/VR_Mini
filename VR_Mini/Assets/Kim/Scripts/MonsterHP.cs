using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MonsterHP : MonoBehaviour, IDamagable
{
    public static MonsterHP Instance;
    private int iD;
    private string description;
    public float weakPointRate;
    private int actTime;
    public float hp;
    public float hp1;
    public float hp2;
    public float hp3;
    public float maxHP;
    private float moveSpeed;

    // { 10/25 유준호 추가
    private const string DAMAGE_CANVAS_NAME = "DamageCanvas";
    private GameObject damageCanvas;
    private GameObject damageObj;
    private TMP_Text damageTMP;
    private Vector3 damageOriginScale;
    // } 10/25 유준호 추가
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        List<object> monster = ResourceManager.Instance.GetDataFromID(Order.MONSTER);
        hp1 = (int)monster[(int)MONSTER.P1_HP];
        hp2 = (int)monster[(int)MONSTER.P2_HP];
        hp3 = (int)monster[(int)MONSTER.P3_HP];
        hp = hp1 + hp2 + hp3;
        maxHP = hp;
        // { 10/25 유준호 추가
        damageCanvas = GameObject.Find(DAMAGE_CANVAS_NAME);

        Invoke("Check", 3f);
        // } 10/25 유준호 추가
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameManager.Instance.gameOver)
        //{
        //    return;
        //}

        //if(GameManager.Instance.bossHp == 0)
        //{
        //    GameManager.Instance.End_Game();
        //}
    }

    private void Check()
    {
        OnDamage(10, 10, new Vector3(50, 79, 631));
    }

    public void OnHit(int value)
    {
       // GameManager.Instance.bossHp -= value;
    }

    public void OnDamage(float damage, int hitGold, Vector3 worldPos)
    {
        GameManager.Instance.bossHp -= damage;
        hp -= damage;
        
        ColorChange color = FindObjectOfType<ColorChange>();
        color.ChangeColor();

        // { 10/25 유준호 추가
        GameManager.Instance.Add_Gold(hitGold);

        damageObj = DamageTextPool.Instance.Get();
        damageTMP = damageObj.GetComponentInChildren<TMP_Text>();

        damageObj.SetActive(true);
        damageOriginScale = damageObj.transform.localScale;
        damageObj.transform.position = worldPos;
        damageObj.transform.parent = damageCanvas.transform;
        damageObj.transform.localPosition = new Vector3(damageObj.transform.localPosition.x, damageObj.transform.localPosition.y, damageObj.transform.localPosition.z);
        damageObj.transform.localScale = damageOriginScale * damageObj.transform.position.z / 600;
        damageTMP.text = "" + damage;
        // } 10/25 유준호 추가
    }    
}
