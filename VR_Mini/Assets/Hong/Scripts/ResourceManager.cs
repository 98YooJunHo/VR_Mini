using System.Collections.Generic;
using UnityEngine;

// ����
public enum Order 
{ 
    PC, ITEM1, ITEM2, MONSTER, MONSTER_PROJECTILE, SPAWN_MONSTER1, SPWAN_MONSTER2, WEAPON1, WEAPON2 
}
// KEYS
public enum Item 
{
    ID, DESCRIPTION, TYPE, DURATION, VAL1
}
public enum PC 
{
    ID, DESCRIPTION, HP, WEAPON_NORAML, WEAPON_POWERED, TIME_GOLD, INIT_GOLD
}
public enum Monster 
{
    ID, DESCRIPTION, WEAKPOINT_RATE, ACT_TIME, HP, MOVESPEED, NORMAL_GOLD, CRI_GOLD
}
public enum Weapon 
{
    ID, DESCRIPTION, INTERVAL, DMG
}
public enum SpawnMonster 
{
    ID, DESCRIPTION, HP, DMG, SPEED, ATTACK_RANGE, EXPLOSION_RANGE
}
public enum Projectile 
{
    ID, DESCRIPTION, HP, PROJECTILE_LIFETIME, DAMAGE, SPEED
}

/* !�ʵ� �����������¹�
 *  1. ���� List<object> name = GetDataFromID(Order.(���� ���ϴ� ���))
 *  !Order�� ������ ���� �ִ��� F12�� ������ ���� �ִ��� Ȯ���ϼ���.
 *  2. name[(���ϴ� ���).(���ϴ� ����)]�� ���� �޾� �� �� �ֽ��ϴ�.
 *  
 *  EX) 
 *  List<object> test = GetDataFromID(Order.PC) // pc�� ������ �������� ������
 *  �������� ���� ������ ���۰���� INIT_GOLD �Դϴ�.
 *  int gold = test[(int)PC.TIME_GOLD]] �̷� ������ �����ϸ� �˴ϴ�.
 *  �𸣴°� ������ �����
 */
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // ���
    private string itemPath = "CSVFiles/ItemTable";
    private string monsterPath = "CSVFiles/MonsterTable";
    private string pcPath = "CSVFiles/PCTable";
    private string weaponPath = "CSVFiles/WeaponTable";
    private string spawnMonsterPath = "CSVFiles/SpwanMonsterTable";
    private string monsterProjectilePath = "CSVFiles/MonsterProjectileTable";

    // Data
    public List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();

    public List<Dictionary<string, object>> itemTable = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> monsterTable = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> PCTable = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> weaponTable = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> spawnMonsterTable = new List<Dictionary<string, object>>();
    public List<Dictionary<string, object>> monsterProjectileTable = new List<Dictionary<string, object>>();

    public void Awake()
    {
        InitTable();
        
        List<object> test = GetDataFromID(Order.PC);
        Debug.Log("�ð� : " + test[(int)PC.TIME_GOLD]);

    }

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

    public List<object> GetDataFromID(Order order)
    {
        List<object> list = new List<object>();
        Dictionary<string, object> my = data[(int)order];
        foreach (var m in my)
        {
            list.Add(m.Value);
        }
        return list;
    }



}
