using System;
using System.Collections.Generic;
using UnityEngine;


// 순서
public enum Order
{
    PC, ITEM1, ITEM2, MONSTER, MONSTER_PROJECTILE, SPAWN_MONSTER1, SPAWN_MONSTER2, WEAPON1, WEAPON2
}

// KEYS

// ID 1
public enum PC 
{
    ID, DESCRIPTION, HP, WEAPON_NORMAL, WEAPON_POWERED, TIME_GOLD, INIT_GOLD
}

// ID 10~11
public enum Item 
{
    ID, DESCRIPTION, TYPE, DURATION, VAL1, GOLD
}

// ID 100
public enum Monster 
{
    ID, DESCRIPTION, WEAKPOINT_RATE, ACT_TIME, HP, MOVESPEED, NORMAL_GOLD, CRI_GOLD
}

// ID 200
public enum Projectile 
{
    ID, DESCRIPTION, HP, PROJECTILE_LIFETIME, DMG, SPEED
}

// ID 300, 301
public enum SpawnMonster 
{
    ID, DESCRIPTION, HP, DMG, SPEED, ATTACK_RANGE, EXPLOSION_RANGE
}

// ID 1000, 1001
public enum Weapon 
{
    ID, DESCRIPTION, INTERVAL, DMG
}



#region !사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법!사용법
/* !필독 변수가져오는법
 *  !Order와 변수가 뭐가 있는지 F12를 눌러서 뭐가 있는지 확인하세요.
 * -------------------------- 2개 이상의 변수를 가져오고 싶을 때 ----------------------
 *  1. 먼저 List<object> name = GetDataFromID(Order.(내가 원하는 대상))
 *  2. name[(원하는 대상).(원하는 변수)]로 값을 받아 올 수 있습니다.
 *  !2개이상을 불러오실 때는 CSV reader자체에서 형변환을 해주긴합니다.float은 다시 형변환이 필요하질도?
 *  
 *  EX) 
 *  List<object> test = GetDataFromID(Order.PC) // pc의 변수를 가져오고 싶으면
 *  가져오고 싶은 변수가 시작골드라면 INIT_GOLD 입니다.
 *  int gold = test[(int)PC.INIT_GOLD] 이런 식으로 접근하면 됩니다.
 *  
 *  --------------------------- 단일로 변수를 불러오고 싶을 때 -----------------------
 *  1. 원하는 data의 Type을 먼저 아셔야합니다.
 *  !기본이 object 타입입니다~
 *  (Type) name = (Type)GetSingleDataFromID(Order.(원하는 대상), (원하는Key)) //! 형변환 필수
 *  
 *  EX) 원하는 DataType이 int일 때
 *  int initGold = (int)GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
 *  모르는거 있으면 물어보셈 킹아~
 *  
 *  --------------------------- 형변환기 ------------------------------------------
 *  그냥 type 붙이는게 편할듯함, int인데 string으로 불러오고 싶은 방식등에서는 쓸만함
 *  1. 단일변수와 섞어서 사용하시면 됩니다.
 *  (type) name = ChangeType<type>("type", Order.(원하는 대상), (원하는Key))
 *  EX) string str2 = ChangeType<string>("string", GetSingleDataFromID(Order.PC, PC.DESCRIPTION));
 *  
 */
#endregion


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    #region 변수
    // 경로
    private string itemPath = "CSVFiles/ItemTable";
    private string monsterPath = "CSVFiles/MonsterTable";
    private string pcPath = "CSVFiles/PCTable";
    private string weaponPath = "CSVFiles/WeaponTable";
    private string spawnMonsterPath = "CSVFiles/SpwanMonsterTable";
    private string monsterProjectilePath = "CSVFiles/MonsterProjectileTable";

    // Data
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> itemTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> monsterTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> PCTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> weaponTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> spawnMonsterTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> monsterProjectileTable = new List<Dictionary<string, object>>();
    #endregion

    public void Awake()
    {
        Instance = this;
        InitTable();
    }


    public void Start()
    {

        //test
        //List<object> test = GetDataFromID(Order.PC);
        //Debug.Log("*�ð� ��� : " + test[(int)PC.TIME_GOLD].GetType());
        //Debug.Log("���� ��� : " + test[(int)PC.INIT_GOLD]);
        //Debug.Log("�÷��̾� ü�� : " + test[(int)PC.HP]);
        //Debug.Log("*���� : " + test[(int)PC.DESCRIPTION].GetType());

        //List<object> test2 = GetDataFromID(Order.WEAPON1);
        //Debug.Log("Weapon1 ������ : " + test2[(int)Weapon.DMG]);
        //Debug.Log("Weapon1 ���� : " + test2[(int)Weapon.INTERVAL]);

        //List<object> test3 = GetDataFromID(Order.MONSTER);
        //Debug.Log("���� �̵��ӵ�: " + test3[(int)Monster.MOVESPEED]);
        //Debug.Log("���� ũ�����: " + test3[(int)Monster.CRI_GOLD]);

        //int t = (int)GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        //Debug.Log("t InitGold :"+t);


        //string str = GetSingleDataFromID(Order.PC, PC.DESCRIPTION).ToString();
        //Debug.Log("str description :" + str);

        //string str2 = ChangeType<string>("string", GetSingleDataFromID(Order.PC, PC.ID));
        //Debug.Log("str2 : " +str2);
    }

    #region INIT
    private void InitTable()
    {
           
        InitItemTable();
        InitAllData();
    }


    private void InitItemTable()
    {
        itemTable = CSVReader.Read(itemPath);
        monsterTable = CSVReader.Read(monsterPath);
        weaponTable = CSVReader.Read(weaponPath);
        PCTable = CSVReader.Read(pcPath);
        spawnMonsterTable = CSVReader.Read(spawnMonsterPath);
        monsterProjectileTable = CSVReader.Read(monsterProjectilePath);
    }

    private void InitAllData()
    {
        data.AddRange(PCTable);
        data.AddRange(itemTable);
        data.AddRange(monsterTable);
        data.AddRange(monsterProjectileTable);
        data.AddRange(spawnMonsterTable);
        data.AddRange(weaponTable);
    }
    #endregion

    #region FUNCTION
    public List<object> GetDataFromID(Order order)
    {
        List<object> list = new List<object>();
        Dictionary<string, object> dic = data[(int)order];
        foreach (var value in dic)
        {
            list.Add(value.Value);
        }
        if (list.Count == 0){ Debug.Log("Data List Empty Error"); return null;}
        return list;
    }

    public object GetSingleDataFromID<T>(Order order, T target) where T : Enum
    {
        List<object> list = new();
        Dictionary<string, object> dic = data[(int)order];
        foreach (var value in dic)
        {
            list.Add(value.Value);
        }

        object myObject = list[Convert.ToInt32(target)];

        if (myObject != null) { return myObject; }
        else { Debug.Log("Fail To Access Data"); return default; }
    }

    public T ChangeType<T>(string type, object target)
    {

        switch (type)
        { 
            case "string":
                return (T)(object)target.ToString();
            case "int":
                return (T)(object)Convert.ToInt32(target);
            case "float":
                return (T)(object)Convert.ToSingle(target);
            default:
                Debug.Log("TypeChange Error");
                return default;
        }
    }

    #endregion
}
