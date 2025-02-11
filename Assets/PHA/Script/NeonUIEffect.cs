using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI; // UI 요소 사용을 위해 필요

public class NeonUIEffect : MonoBehaviour
{
    public UnityEngine.UI.Image neonImage; // Image 타입 명확하게 지정
    public Color[] neonColors = { Color.cyan, Color.magenta, Color.yellow, Color.green }; // 네온 색상 목록
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f); // 기본 화면 색상 (투명도 30%)
    public AudioSource glitchSound; // 치지직 효과음 (선택 사항)

    private bool isFlickering = false;

    void Start()
    {
        if (neonImage == null)
        {
            UnityEngine.Debug.LogError("네온 이미지가 설정되지 않았습니다!");
            return;
        }

        neonImage.color = normalColor; // 처음에는 기본 화면 색상 유지
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = UnityEngine.Random.Range(2.0f, 3.5f); // 원래 화면 유지 시간 (더 길게)
            float flickerTime = UnityEngine.Random.Range(0.05f, 0.2f); // 네온 색상 유지 시간 (더 짧게)
            bool glitch = UnityEngine.Random.value > 0.85f; // 15% 확률로 치지직 효과 발생

            // 기본 화면 유지 (투명도 30%)
            StartCoroutine(ChangeColor(normalColor, normalTime));
            yield return new WaitForSeconds(normalTime);

            if (glitch)
            {
                StartCoroutine(GlitchEffect());
                yield return new WaitForSeconds(0.1f);
            }

            // 네온 색상 랜덤 변경 (투명도 유지)
            Color newNeonColor = neonColors[UnityEngine.Random.Range(0, neonColors.Length)];
            newNeonColor.a = 0.3f; // 투명도 30% 유지
            StartCoroutine(ChangeColor(newNeonColor, flickerTime));
            yield return new WaitForSeconds(flickerTime);
        }
    }

    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play(); // 치지직 소리 재생

        neonImage.color = new Color(1f, 1f, 1f, 0.3f); // 순간적으로 밝게
        yield return new WaitForSeconds(0.05f);
        neonImage.color = normalColor; // 원래 색상으로 복구
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        if (isFlickering) yield break;

        isFlickering = true;
        float elapsed = 0f;
        Color startColor = neonImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            neonImage.color = Color.Lerp(startColor, targetColor, elapsed / duration);
            yield return null;
        }

        isFlickering = false;
    }
}
