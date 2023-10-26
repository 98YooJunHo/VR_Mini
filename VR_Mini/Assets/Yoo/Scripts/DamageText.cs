using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private const float FADEOUT_TIME = 1.5f;
    private const float UP_SPEED = 20f;
    private float time = default;

    private void OnEnable()
    {
        DoFadeOut();
    }

    void DoFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        time = 0;
        TMP_Text damageText = GetComponentInChildren<TMP_Text>();
        Color originColor = damageText.color;
        Color color = damageText.color;
        float originYPos = transform.localPosition.y;
        float yPos;
        Vector3 pos;
        float alpha;
        while (time < FADEOUT_TIME)
        {
            time += Time.deltaTime;
            alpha = (FADEOUT_TIME - time) / FADEOUT_TIME;
            color.a = alpha;
            damageText.color = color;

            yPos = transform.position.z / 600 * UP_SPEED * time;
            pos = new Vector3(transform.localPosition.x, originYPos + yPos, transform.localPosition.z);
            transform.localPosition = pos;
            yield return null;
        }
        damageText.color = originColor;
        DamageTextPool.Instance.Set(gameObject);
        yield break;
    }
}
