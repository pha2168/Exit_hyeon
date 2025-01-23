using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasFadeInOut : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup; // 캔버스 그룹 참조
    [SerializeField] private float fadeInTime = 1f; // 페이드 인 시간
    [SerializeField] private float holdTime = 1f; // 유지 시간
    [SerializeField] private float fadeOutTime = 1f; // 페이드 아웃 시간

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        StartCoroutine(FadeSequence());
    }

    private IEnumerator FadeSequence()
    {
        // 페이드 인
        yield return StartCoroutine(Fade(0, 1, fadeInTime));

        // 유지
        yield return new WaitForSeconds(holdTime);

        // 페이드 아웃
        yield return StartCoroutine(Fade(1, 0, fadeOutTime));
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha; // 마지막 값 보정
    }
}