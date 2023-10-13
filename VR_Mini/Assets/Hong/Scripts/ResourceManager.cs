using System.Collections.Generic;
using UnityEngine;

// 순서
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

/* !필독 변수가져오는법
 *  1. 먼저 List<object> name = GetDataFromID(Order.(내가 원하는 대상))
 *  !Order와 변수가 뭐가 있는지 F12를 눌러서 뭐가 있는지 확인하세요.
 *  2. name[(원하는 대상).(원하는 변수)]로 값을 받아 올 수 있습니다.
 *  
 *  EX) 
 *  List<object> test = GetDataFromID(Order.PC) // pc의 변수를 가져오고 싶으면
 *  가져오고 싶은 변수가 시작골드라면 INIT_GOLD 입니다.
 *  int gold = test[(int)PC.TIME_GOLD]] 이런 식으로 접근하면 됩니다.
 *  모르는거 있으면 물어보셈
 */
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    // 경로
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
        Debug.Log("시간 : " + test[(int)PC.TIME_GOLD]);

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
