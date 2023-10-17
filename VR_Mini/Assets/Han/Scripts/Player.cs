using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    public static Player instance;

    //선을 그릴 라인 렌더러
    private LineRenderer lineRenderer = default;
    
    private int currentWeapon = default;

    GameObject shopCanvas;

    private bool isEffectSpawning = false; // 효과를 생성 중인지 여부를 나타내는 플래그
    private Coroutine effectCoroutine; // 코루틴 참조를 저장할 변수 

    private Image hitImage;
    private float fadeDuration = 1.5f; // 페이드 아웃에 걸리는 시간
    private float currentAlpha;       // 현재 알파값
    private float targetAlpha;        // 목표 알파값
    private float timer;              // 타이머
    private float originAlpha;        // 최조 알파값

    private bool isHited = false;     // 피격코루틴 실행여부

    private int projectileDamage = default; // 몬스터 데미지
    // Start is called before the first frame update
    void Start()
    {
        // 인스턴스 생성
        instance = this;
        // 라인렌더러 
        lineRenderer = GetComponent<LineRenderer>();
        // 피격캔버스이미지 가져오기
        hitImage = GameObject.Find("hitImage").GetComponent<Image>();
        // 피격 캔버스이미지 끄기
        hitImage.gameObject.SetActive(false);
        // 페이드 아웃 목표 알파 값 설정
        targetAlpha = 0.00000001f;
        // 데미지 받을때부터 시간 측정 값
        timer = 0f;
        // 최초 알파값 저장
        originAlpha = hitImage.color.a;
        // 무기 번호 지정
        currentWeapon = 1;
        // 괴수 투사체 데미지가져오기
        //projectileDamage = (int)ResourceManager.Instance.GetSingleDataFromID(Order.MONSTER_PROJECTILE, Projectile.DMG);

        /*
        List<object> test = ResourceManager.Instance.GetDataFromID(Order.PC);
        int id = (int)test[(int)PC.ID];
        string dec = test[(int)PC.DESCRIPTION].ToString();

        int id2 = (int)ResourceManager.Instance.GetSingleDataFromID(Order.PC, PC.ID);
        */
    }

    // Update is called once per frame
    void Update()
    {
        // Todo : 게임오버 상태일때는 Return 해버려서 못하게해야한다.
        Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
        RaycastHit hitInfo = default;
        lineRenderer.enabled = true;
        if (Physics.Raycast(ray, out hitInfo, 200f))
        {
            if (hitInfo.collider.gameObject.layer.Equals("Boss")) //|| hitInfo.collider.gameObject.layer.Equals("Projectile"))
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            else
            {
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, ray.origin);
            lineRenderer.SetPosition(1, ray.origin + ARAVRInput.RHandDirection * 200);
        }
        /*
        if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.LTouch))
        {
            if(shopCanvas.activeSelf == true)
            {
                shopCanvas.SetActive(false);
            }
            if (shopCanvas.activeSelf == false)
            {
                shopCanvas.SetActive(true);
            }
        }
        // 게임 상점이 열렸을때
        if (shopCanvas.activeSelf == true)
        {
            Ray ray = new Ray(ARAVRInput.RHandPosition, ARAVRInput.RHandDirection);
            RaycastHit hitInfo = default;
            int layer = 1 << LayerMask.NameToLayer("ShopUI");

            if (ARAVRInput.GetDown(ARAVRInput.Button.One, ARAVRInput.Controller.RTouch) && (Physics.Raycast(ray, out hitInfo, 200f, layer)))
            {
                // 버튼 실행.
            }    
        }


        // 게임 상점이 열린상태일떄는 공격입력을 받지 못하게하는 코드
        if ( shopCanvas.activeSelf == true)
        {
            return;
        }
        */

        // 공격코드
        if (ARAVRInput.Get(ARAVRInput.Button.IndexTrigger, ARAVRInput.Controller.RTouch))
        {
            
            // TODO : 괴수의 피격당하는 함수실행시키기.
            if (hitInfo.collider.gameObject.layer.Equals("Boss"))
            {
                // 이펙트를 만드는 코드
                if (!isEffectSpawning)
                {
                    // 코루틴이 실행 중이 아닌 경우에만 실행
                    effectCoroutine = StartCoroutine(SpawnEffectPeriodically(hitInfo, currentWeapon));
                    isEffectSpawning = true;
                }
            }
           /* else if (hitInfo.collider.gameObject.layer.Equals("Projectile"))
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
    }
    private IEnumerator SpawnEffectPeriodically(RaycastHit hitInfo, int currentWeapon)
    {
        
        if (currentWeapon == 1)
        {
            while (true)
            {
                // effect를 생성하거나 위치를 업데이트하는 코드
                GameObject effect = BulletPoolManager.instance.GetQueue(1);
                effect.transform.position = hitInfo.point;

                yield return new WaitForSeconds(0.5f); // 0.5초 동안 대기
            }
        }
        else if (currentWeapon == 2)
        {
            while (true)
            {
                // effect를 생성하거나 위치를 업데이트하는 코드
                GameObject effect = BulletPoolManager.instance.GetQueue(2);
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

    private void DamageTake()
    {
        //HP 계산하기
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
            // currentAlpha = newColor.a;
            // 코루틴 시작하기
            StartCoroutine(Hited());
        }
        /*
        if (GameManager.playerHp == 0)
        {
            GameManager.gameOver = true;                // 주석 해제하기
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer.Equals("Boss"))
        {
            DamageTake();
        }
    }

    private IEnumerator Hited()
    {
        float alphaPercentage =0f; 
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

}
