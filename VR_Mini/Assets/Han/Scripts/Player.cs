using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState
{
    LASER, LIGHTING, ICE
}

public class Player: MonoBehaviour
{
    public const string iceMuzzle = "IceMuzzle";
    public const string electricMuzzle = "ElectricMuzzle";
    public const string chargeEffectName = "WaterCharge";
    public const string weaponMuzzleName = "Aurous_Crystal";

    Ray ray = default;                           // 레이 변수   
    public RaycastHit hitInfo = default;                // 레이캐스트 힛인포 변수   
    public int userWeaponState;                  // 웨폰스테이트 객체
    public static Player instance;               // 스태틱 객체
    private LineRenderer lineRenderer = default; // 선을 그릴 라인 렌더러
    //=================== 무기 업그레이드를 위한 변수 ============================================
    private GameObject weaponMuzzle;             // 일반무기 오브젝트
    private GameObject chargeEffect;             // 차지 이펙트 오브젝트
    private bool chargingStop;                   // 차징 멈추는 플래그
    private GameObject weaponIceMuzzle;          // 무기 아이스투사체 생성위치
    public GameObject weaponElectricMuzzle;     // 무기 전기투사체 생성위치
    GameObject iceProjectile =default;           // 아이스 투사체
    public Material laserMaterial = default;     // 레이저 메테리얼
    public Material normalMaterial = default;    // 기본 메테리얼
    private bool isAttacking = default;          // 공격중임을 판단하는 변수
    float effectTime = default;
    float damageTime = default;

    //=================== 보스 피격 이펙트를 위한 변수 ============================================
    private Image hitImage;                      // 플레이어피격이미지
    private float fadeDuration = 1.5f;           // 페이드 아웃에 걸리는 시간
    private float currentAlpha;                  // 현재 알파값
    private float targetAlpha;                   // 목표 알파값
    private float timer;                         // 타이머
    private float originAlpha;                   // 최조 알파값
    private bool isHited = false;                // 피격코루틴 실행여부
    //=================== 보스 피격 이펙트를 위한 변수 ============================================
    private int projectileDamage = default;      // 몬스터 데미지
    private MonsterHP boss = default;            // 보스 체력 스크립트
    // Start is called before the first frame update
    void Start()
    {
        instance = this;                                              // 인스턴스 생성
        lineRenderer = GetComponent<LineRenderer>();                  // 라인렌더러
        hitImage = GameObject.Find("hitImage").GetComponent<Image>(); // 피격캔버스이미지 가져오기
        hitImage.gameObject.SetActive(false);                         // 피격 캔버스이미지 끄기
        targetAlpha = 0.00000001f;                                    // 페이드 아웃 목표 알파 값 설정
        timer = 0f;                                                   // 데미지 받을때부터 시간 측정 값
        originAlpha = hitImage.color.a;                               // 최초 알파값 저장   
        boss = FindFirstObjectByType<MonsterHP>();                    // 보스 체력 스크립트 가져오기
        weaponIceMuzzle = GameObject.Find(iceMuzzle);                 // 아이스 발사되는 위치
        weaponElectricMuzzle = GameObject.Find(electricMuzzle);       // 전기 발사되는 위치
        chargeEffect = GameObject.Find(chargeEffectName);             // 차지 이펙트
        chargeEffect.SetActive(false);                                // 차지 이펙트 꺼두기
        weaponMuzzle = GameObject.Find(weaponMuzzleName);             // 무기 머즐
        userWeaponState = (int)WeaponState.LASER;
        /* Resource 파일에서 불러오는 예시
        List<object> test = ResourceManager.Instance.GetDataFromID(Order.PC);
        int id = (int)test[(int)PC.ID];
        string dec = test[(int)PC.DESCRIPTION].ToString();
        int id2 = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.ID);
        */
    }

    // Update is called once per frame
    void Update()
    {
        //=================== 각종 상태동안 작동안하도록하는 처리 ============================================
        if (GameManager.Instance.gameOver == true)
        {
            return;
        }
        if (GameManager.Instance.shopOpen == true)
        {
            return;
        }

        //========================================= 레이 쏘기 =============================================
        ShotRayCast();

        //========================================= 공격 하기 =============================================
        AttackEnemy();
    }
    

    //======================================= 피격 계산 함수 ==============================================
    #region 피격함수,코루틴
    public void DamageTake(int value)
    {
        //HP 계산하기
        GameManager.Instance.playerHp -= value;
        //GameManager.playerHP -= projectileDamage;   // 추후 주석 해제하기
        if (isHited == false)
        {
            isHited = true;
            //히트 캔버스 활성화하기
            hitImage.gameObject.SetActive(true);
            //시간 0초로 돌리기
            timer = 0f;
            //컬러 최초의 색으로 돌리기
            Color newColor = hitImage.color;
            newColor.a = originAlpha;
            hitImage.color = newColor;
            // 코루틴 시작하기
            StartCoroutine(Hited());
        }
    }
    private IEnumerator Hited()
    {
        float alphaPercentage = 0f;
        while (timer <= fadeDuration)
        {
            timer += Time.deltaTime;
            // 가중치를 timer에서 fadeDuration을 나눈값으로 설정한다.
            alphaPercentage = timer / fadeDuration;
            //러프값을줘서 천천히 alpha값이 떨어짐
            currentAlpha = Mathf.Lerp(originAlpha, targetAlpha, alphaPercentage);
            // 컬러를 업데이트한다.
            Color newColor = hitImage.color;
            newColor.a = currentAlpha;
            hitImage.color = newColor;
            // 다음 프레임까지 기다린다.
            yield return null;
        }
        // 알파 값이 목표값 이하로 떨어지면 스크립트 비활성화
        hitImage.gameObject.SetActive(false);
        isHited = false;
    }
    #endregion
    //======================================= 피격 계산 함수 ==============================================



    //======================================= 레이 쏘기 ===================================================
    #region 레이쏘기
    private void ShotRayCast()
    {
        ray = new Ray(weaponMuzzle.transform.position, ARAVRInput.RHandDirection);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        if (Physics.Raycast(ray, out hitInfo, 600f))
        {
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boss")) //|| hitInfo.collider.gameObject.layer.Equals("Projectile"))
            {
                lineRenderer.SetPosition(0, weaponMuzzle.transform.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, weaponMuzzle.transform.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
        }
    }
    #endregion 레이쏘기
    //======================================= 레이 쏘기 ===================================================



    //======================================= 공격 함수 ===================================================
    #region 공격함수
    public void AttackEnemy()
    {

        if (userWeaponState == (int)WeaponState.LASER)
        {
            if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                chargeEffect.SetActive(true);
                lineRenderer.material = laserMaterial;
                lineRenderer.startWidth = 0.07f;
                lineRenderer.endWidth = 1f;
            }
            else if (ARAVRInput.GetUp(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                isAttacking = false;
                chargeEffect.SetActive(false);
                lineRenderer.material = normalMaterial;
                lineRenderer.startWidth = 0.001f;
                lineRenderer.endWidth = 0.001f;
            }
            if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    effectTime += Time.deltaTime;
                    damageTime += Time.deltaTime;
                    if (effectTime >0.1f)
                    {

                        effectTime = 0;
                        GameObject gameObject = EffectPoolManager.instance.GetQueue(userWeaponState);
                        gameObject.transform.position = hitInfo.point;
                    }
                    
                    // 보스 체력 깎기
                    if (damageTime >1.0f && boss != null)
                    {
                        // TODO : 보스 체력깎는 코드
                        damageTime = 0.0f;
                    }
                    // 이펙트를 만드는 코드
                }
                else
                {
                    effectTime = 0f;
                    damageTime = 0.0f;
                }
            }
            else
            {
                effectTime = 0f;
                damageTime = 0.0f;
            }
        }


        if (userWeaponState == (int)WeaponState.LIGHTING)
        {

            if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                if (isAttacking == false)
                {
                    isAttacking = true;
                    // 코루틴만들기
                    StartCoroutine(AttacKElectric());
                }

                // hitInfo가 boss일때 맞는 판정으로 만들기s
                if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
                {
                    if (boss != null)
                    {

                        // TODO : 보스 체력깎는 코드
                    }
                }
            }
            else
            {
                isAttacking = false;
            }
        }

        if (userWeaponState == (int)WeaponState.ICE)
        {
            // Ice
            if (ARAVRInput.GetDown(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                chargingStop = false;
                chargeEffect.SetActive(true);
                iceProjectile = ProjectilePool.instance.GetQueue(userWeaponState);
                iceProjectile.transform.position = weaponIceMuzzle.transform.position;
                StartCoroutine(chargeDMG(iceProjectile));
                StartCoroutine(chargeScale(iceProjectile));
            }
            else if (ARAVRInput.GetUp(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
            {
                chargingStop = true;
                chargeEffect.SetActive(false);
                if (iceProjectile != null)
                {
                    iceProjectile.transform.LookAt(Player.instance.hitInfo.point);
                    iceProjectile.GetComponent<IceProjectile>().shot();
                }
            }


        }
    }
    #endregion 공격함수
    //======================================= 공격 함수 ===================================================



    //======================================= 차지,크기 코루틴 함수 =========================================
    #region 차지, 크기 코루틴
    //데미지 올려주는 코루틴.
    private IEnumerator chargeDMG(GameObject iceProjectile)
    {
        int timer = 0;
        int chargeTime = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_MAX_TIME);
        int originDMG = iceProjectile.GetComponent<IceProjectile>().originDMG;
        int chargeDMG = iceProjectile.GetComponent<IceProjectile>().chargeDMG;
        while (timer <= chargeTime)
        {
            if(chargingStop == true)
            {
                break;
            }
            timer++;
            iceProjectile.GetComponent<IceProjectile>().currentDMG += chargeDMG;
            yield return new WaitForSeconds(1.0f);
        }    
    }

    //크기를 키워주는 코루틴
    private IEnumerator chargeScale(GameObject iceProjectile)
    {
        float timer = 0;
        int chargeTime = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_MAX_TIME);
        Transform currentScale = iceProjectile.GetComponent<IceProjectile>().currentScale;
        float percentage = 0f;
        float originScale = 1f;
        float maxScale = (int)ResourceManager.Instance.GetSingleDataFromID(Order.ICE_WEAPON, ICE_WEAPON.CHARGING_MAX_SCALE);
        while (timer <= chargeTime)
        {
            if (chargingStop == true)
            {
                break;
            }
            timer += Time.deltaTime;
            percentage = timer / chargeTime;
            currentScale.localScale = new Vector3 (Mathf.Lerp(originScale, maxScale, percentage), Mathf.Lerp(originScale, maxScale, percentage), Mathf.Lerp(originScale, maxScale, percentage));
            yield return null;
        }
    }
    #endregion
    //======================================= 차지,크기 코루틴 함수 =========================================

    //=========================================== 전기 코루틴 함수 =========================================
    #region
    private IEnumerator AttacKElectric()
    {
       
        while (true)
        {
            if (isAttacking == false)
            {
                break;
            }
            GameObject lighting = ProjectilePool.instance.GetQueue(userWeaponState);
            lighting.transform.position = weaponElectricMuzzle.transform.position;
            yield return new WaitForSeconds(1.0f);
        }
    }
    #endregion
    //============================================ 전기 코루틴 함수 =========================================
}
