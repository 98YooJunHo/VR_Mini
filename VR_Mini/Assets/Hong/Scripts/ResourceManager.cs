using System;
using System.Collections.Generic;
using UnityEngine;


// ����
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
    ID, DESCRIPTION, TYPE, DURATION, VAL1
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



#region !����!����!����!����!����!����!����!����!����!����!����!����!����!����!����!����
/* !�ʵ� �����������¹�
 *  !Order�� ������ ���� �ִ��� F12�� ������ ���� �ִ��� Ȯ���ϼ���.
 * -------------------------- 2�� �̻��� ������ �������� ���� �� ----------------------
 *  1. ���� List<object> name = GetDataFromID(Order.(���� ���ϴ� ���))
 *  2. name[(���ϴ� ���).(���ϴ� ����)]�� ���� �޾� �� �� �ֽ��ϴ�.
 *  !2���̻��� �ҷ����� ���� CSV reader��ü���� ����ȯ�� ���ֱ��մϴ�.float�� �ٽ� ����ȯ�� �ʿ�������?
 *  
 *  EX) 
 *  List<object> test = GetDataFromID(Order.PC) // pc�� ������ �������� ������
 *  �������� ���� ������ ���۰���� INIT_GOLD �Դϴ�.
 *  int gold = test[(int)PC.INIT_GOLD] �̷� ������ �����ϸ� �˴ϴ�.
 *  
 *  --------------------------- ���Ϸ� ������ �ҷ����� ���� �� -----------------------
 *  1. ���ϴ� data�� Type�� ���� �Ƽž��մϴ�.
 *  !�⺻�� object Ÿ���Դϴ�~
 *  (Type) name = (Type)GetSingleDataFromID(Order.(���ϴ� ���), (���ϴ�Key)) //! ����ȯ �ʼ�
 *  
 *  EX) ���ϴ� DataType�� int�� ��
 *  int initGold = (int)GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
 *  �𸣴°� ������ ����� ŷ��~
 *  
 *  --------------------------- ����ȯ�� ------------------------------------------
 *  �׳� type ���̴°� ���ҵ���, int�ε� string���� �ҷ����� ���� ��ĵ���� ������
 *  1. ���Ϻ����� ��� ����Ͻø� �˴ϴ�.
 *  (type) name = ChangeType<type>("type", Order.(���ϴ� ���), (���ϴ�Key))
 *  EX) string str2 = ChangeType<string>("string", GetSingleDataFromID(Order.PC, PC.DESCRIPTION));
 *  
 */
#endregion


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    #region ����
    // ���
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
        InitTable();
        
        //test
        List<object> test = GetDataFromID(Order.PC);
        Debug.Log("*�ð� ��� : " + test[(int)PC.TIME_GOLD].GetType());
        Debug.Log("���� ��� : " + test[(int)PC.INIT_GOLD]);
        Debug.Log("�÷��̾� ü�� : " + test[(int)PC.HP]);
        Debug.Log("*���� : " + test[(int)PC.DESCRIPTION].GetType());

        List<object> test2 = GetDataFromID(Order.WEAPON1);
        Debug.Log("Weapon1 ������ : " + test2[(int)Weapon.DMG]);
        Debug.Log("Weapon1 ���� : " + test2[(int)Weapon.INTERVAL]);

        List<object> test3 = GetDataFromID(Order.MONSTER);
        Debug.Log("���� �̵��ӵ�: " + test3[(int)Monster.MOVESPEED]);
        Debug.Log("���� ũ�����: " + test3[(int)Monster.CRI_GOLD]);

        int t = (int)GetSingleDataFromID(Order.PC, PC.INIT_GOLD);
        Debug.Log("t InitGold :"+t);


        string str = GetSingleDataFromID(Order.PC, PC.DESCRIPTION).ToString();
        Debug.Log("str description :" + str);

        string str2 = ChangeType<string>("string", GetSingleDataFromID(Order.PC, PC.ID));
        Debug.Log("str2 : " +str2);
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
        List<object> list = new();
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
