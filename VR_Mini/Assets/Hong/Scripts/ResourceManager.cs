using System;
using System.Collections.Generic;
using UnityEngine;


// 순서
public enum Order
{
    PC, LASER_WEAPON, LIGHTING_WEAPON, ICE_WEAPON, MONSTER ,MONSTER_MOVE_SKILL, MONSTER_NORMAL_SKILL,
    MONSTER_SOUL_SKILL, MONSTER_METEOR_SKILL, MONSTER_BREATHE_SKILL
}

// KEYS

// ID 1
public enum PC 
{
    ID, DESCRIPTION, HP,TIME_GOLD, INIT_GOLD
}


// HIT_GOLD = 맞출때마다 골드

// ID 1000
//! Laser HIT_GOLD는 초당, OVERHIT_MAX 최대과열, OVERHIT_INCREASE 과열증가량,
//  OVERHIT_DECREASE 과열 감소량, OVERHIT_PENALTY 과열오버시딜레이(초)
public enum LASER_WEAPON
{
    ID, NAME, DESCRIPTION, DMG, HIT_GOLD, OVERHIT_MAX, OVERHIT_INCREASE, OVERHIT_DECREASE, OVERHIT_PENALTY
}

// ID 1001
public enum LIGHTING_WEAPON
{
    ID, NAME, DESCRIPTION, DMG, HIT_GOLD, BUY_GOLD, ATTACK_SPEED
}

// ID 1002
//! PROJECTILE_SPEED 투사체 속도, CHARGING_PER_DMG 충전 초당 데미지증가량, CHARGING_MAX_TIME 최대 충전가능시간
// CHARGING_MAX_SCALE 최대충전스케일
public enum ICE_WEAPON
{
    ID, NAME, DESCRIPTION, DMG, HIT_GOLD, BUY_GOLD, ATTACK_SPEED, PROJECTILE_SPEED, CHARGING_PER_DMG,
    CHARGING_MAX_TIME, CHARGING_MAX_SCALE
}


//! P1_HP, P2_HP, P3_HP  페이즈별 HP

// ID 100
//! WEAKPOINT_ACT_TIME 약점활성화시간, WEAKPOINT_COUNT 약점개수, WEAKPOINT_HP 약점HP, FAR 플레이어와 괴물사이의 거리 
public enum MONSTER
{
    ID, DESCRIPTION, WEAKPOINT_ACT_TIME, WEAKPOINT_COUNT, WEAKPOINT_HP, WEAKPOINT_DMG, P1_HP, P2_HP, P3_HP, FAR
}

// ID 200
public enum MONSTER_MOVE_SKILL
{
    ID, DESCRIPTION, MOVE_TIME, MOVE_SPEED_P1, MOVE_SPEED_P2, MOVE_SPEED_P3, PROB_P1, PROB_P2, PROB_P3
}

// ID 201
public enum MONSTER_NORMAL_SKILL
{
    ID, DESCRIPTION, CASTING_TIME, SKILL_DMG, SKILL_SPEED, PROJECTILE_HP, PROB_P1, PROB_P2, PROB_P3
}

// ID 202
public enum MONSTER_SOUL_SKILL
{
    ID, DESCRIPTION, CASTING_TIME, SKILL_DMG, SKILL_SPEED, PROB_P2, PROB_P3
}

// ID 203
public enum MONSTER_METEOR_SKILL
{
    ID, DESCRIPTION, CASTING_TIME, SKILL_DMG, PROJECTILE_SPEED, PROB_P3
}

// ID 204
public enum MONSTER_BREATHE_SKILL
{
    ID, DESCRIPTION, CASTING_TIME, SKILL_DMG
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
    private string iceWeaponPath = "CSVFiles/IceWeapon";
    private string laserWeaponPath = "CSVFiles/LaserWeapon";
    private string lightingWeaponPath = "CSVFiles/LightingWeapon";
    private string mBreatheSkillPath = "CSVFiles/MonsterBreatheSkill";
    private string mMeteorSkillPath = "CSVFiles/MonsterMeteorSkill";
    private string mMoveSkillPath = "CSVFiles/MonsterMoveSkill";
    private string mNormalSkillPath = "CSVFiles/MonsterNormalSkill";
    private string mSoulSkillPath = "CSVFiles/MonsterSoulSkill";
    private string monsterTablePath = "CSVFiles/MonsterTable";
    private string pcTablePath = "CSVFiles/PCTable";

    // Data
    private List<Dictionary<string, object>> data = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> iceWeaponTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> laserWeaponTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> lightingWeaponTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mBreatheSkillTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mMeteorSkillTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mMoveSkillTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mNormalSkillTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> mSoulSkillTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> monsterTable = new List<Dictionary<string, object>>();
    private List<Dictionary<string, object>> pcTable = new List<Dictionary<string, object>>();
    #endregion

    public void Awake()
    {
        Instance = this;
        InitTable();
    }

    public int hp1;

    public void Start()
    {

        //test
        //List<object> test = GetDataFromID(Order.LIGHTING_WEAPON);
        //Debug.Log("attackSpeed : " + test[(int)LIGHTING_WEAPON.ATTACK_SPEED].GetType());
        //Debug.Log("attackSpeed f : " + ChangeType<float>("float", GetSingleDataFromID
        //    (Order.LIGHTING_WEAPON, LIGHTING_WEAPON.ATTACK_SPEED)).GetType());
        //Debug.Log("attackSpeed type : " + test[(int)LIGHTING_WEAPON.ATTACK_SPEED]);

        //List<object> test2 = GetDataFromID(Order.MONSTER);
        //Debug.Log("monster ph_1 : " + test2[(int)MONSTER.P1_HP].GetType());
        //Debug.Log("monster ph_2 : " + test2[(int)MONSTER.P2_HP].GetType());
        //Debug.Log("monster ph_3 : " + test2[(int)MONSTER.P3_HP].GetType());

        //hp1 = (int)test2[(int)MONSTER.P1_HP];
        //Debug.Log("hp1 : "+hp1);
        //Debug.Log("hp1 type : "+hp1.GetType());
    }

    #region INIT
    private void InitTable()
    {      
        InitItemTable();
        InitAllData();
    }


    private void InitItemTable()
    {
        iceWeaponTable = CSVReader.Read(iceWeaponPath);
        laserWeaponTable = CSVReader.Read(laserWeaponPath);
        lightingWeaponTable = CSVReader.Read(lightingWeaponPath);
        mBreatheSkillTable = CSVReader.Read(mBreatheSkillPath);
        mMeteorSkillTable = CSVReader.Read(mMeteorSkillPath);
        mMoveSkillTable = CSVReader.Read(mMoveSkillPath);
        mNormalSkillTable = CSVReader.Read(mNormalSkillPath);
        mSoulSkillTable = CSVReader.Read(mSoulSkillPath);
        monsterTable = CSVReader.Read(monsterTablePath);
        pcTable = CSVReader.Read(pcTablePath);
    }

    private void InitAllData()
    {
        data.AddRange(pcTable);
        data.AddRange(laserWeaponTable);
        data.AddRange(lightingWeaponTable);
        data.AddRange(iceWeaponTable);
        data.AddRange(monsterTable);
        data.AddRange(mMoveSkillTable);
        data.AddRange(mNormalSkillTable);
        data.AddRange(mSoulSkillTable);
        data.AddRange(mMeteorSkillTable);
        data.AddRange(mBreatheSkillTable);
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
