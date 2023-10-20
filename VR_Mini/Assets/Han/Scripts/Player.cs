using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    public static Player instance;               // 스태틱 객체
    private LineRenderer lineRenderer = default; // 선을 그릴 라인 렌더러
    //=================== 무기 업그레이드를 위한 변수 ============================================
    private int currentWeapon = default;         // 현재 무기번호
    private GameObject gun1;                     // 일반무기 오브젝트
    private GameObject gun2;                     // 강화무기 오브젝트
    //=================== 보스 피격 이펙트를 위한 변수 ============================================
    private bool isEffectSpawning = false;       // 보스피격이펙트를 생성 중인지 여부를 나타내는 플래그
    private Coroutine effectCoroutine;           // 코루틴 참조를 저장할 변수 
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
        currentWeapon = 1;                                            // 무기 번호 초기화
        gun1 = GameObject.Find("Gun1");                               // 일반무기 오브젝트 가져오기
        gun2 = GameObject.Find("Gun2");                               // 강화무기 오브젝트 가져오기
        boss = FindFirstObjectByType<MonsterHP>();                    // 보스 체력 스크립트 가져오기
        // 괴수 투사체 데미지가져오기
        // projectileDamage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_PROJECTILE, Projectile.DMG);

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
        if(GameManager.Instance.shopOpen == true)
        {
            return;
        }
        //=================== 각종 상태동안 작동안하도록하는 처리 ============================================

        //========================================= 레이 쏘기 =============================================
        Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        RaycastHit hitInfo = default;
        if(!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        if (Physics.Raycast(ray, out hitInfo, 600f))
        {
            //Debug.Log("레이가 맞기는해?");
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boss")) //|| hitInfo.collider.gameObject.layer.Equals("Projectile"))
            {
                //Debug.Log("보스레이로 인식하고있어?");
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
        }
        //========================================= 레이 쏘기 =============================================

        //========================================= 공격 하기 =============================================
        if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            // TODO : 괴수의 피격당하는 함수실행시키기.
            if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Boss"))
            {
                // 보스 체력 깎기
                if (boss != null)
                {
                    //boss.OnHit((int)ResourceManager.Instance.GetSingleDataFromID(Order.WEAPON1, Weapon.DMG));
                }
                // 이펙트를 만드는 코드
                if (!isEffectSpawning)
                {
                    // 코루틴이 실행 중이 아닌 경우에만 실행
                    effectCoroutine = StartCoroutine(SpawnEffectPeriodically(hitInfo, currentWeapon));
                    isEffectSpawning = true;
                }
            }
            /* else if (hitInfo.collider.gameObject.layer == LayerMask.NameToLayer("Projectile"))
             {
                 // 이펙트를 만드는 코드
                 if (!isEffectSpawning)
                 {
                     // 코루틴이 실행 중이 아닌 경우에만 실행
                     effectCoroutine = StartCoroutine(SpawnEffectPeriodically(hitInfo, currentWeapon));
                     isEffectSpawning = true;
                 }
             }*/
            else
            {
                StopEffectCoroutine();
            }
        }
        else
        {
            StopEffectCoroutine();
        }
        //========================================= 공격 하기 =============================================
    }

    //======================================= 이펙트 터지는 코루틴 ==========================================
    private IEnumerator SpawnEffectPeriodically(RaycastHit hitInfo, int currentWeapon)
    {
        Debug.Log("코루틴속 1번째확인");
        if (currentWeapon == 1)
        {
            while (true)
            {
                // effect를 생성하거나 위치를 업데이트하는 코드
                GameObject effect = EffectPoolManager.instance.GetQueue(1);
                effect.transform.position = hitInfo.point;

                yield return new WaitForSeconds(0.5f); // 0.5초 동안 대기
            }
        }
        else if (currentWeapon == 2)
        {
            while (true)
            {
                Debug.Log("코루틴속 2번째확인");
                // effect를 생성하거나 위치를 업데이트하는 코드
                GameObject effect = EffectPoolManager.instance.GetQueue(2);
                effect.transform.position = hitInfo.point;

                yield return new WaitForSeconds(0.5f); // 0.5초 동안 대기
            }
        }
    }
    

    private void StopEffectCoroutine()
    {
        if (effectCoroutine != null)
        {
            StopCoroutine(effectCoroutine);
            isEffectSpawning = false;
        }
    }
    //======================================= 이펙트 터지는 코루틴 ==========================================


    //======================================= 피격 계산 함수 ===============================================
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
        /*
        if (GameManager.playerHp <= 0)
        {
            GameManager.gameOver = true;                // 주석 해제하기
        }
        */
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

    //======================================= 피격 계산 함수 ===============================================
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.layer.Equals("Boss"))
    //    {
    //        DamageTake();
    //    }
    //}

    public void InforceWeapon()
    {
        //TODO : 지속시간 끝나면 원래대로 돌아가는 코드도 짜야함.
        gun1.SetActive(false);
        gun2.SetActive(true);
        currentWeapon = 2;
    }
}
