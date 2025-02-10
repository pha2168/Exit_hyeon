using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NeonUIEffect : MonoBehaviour
{
    public Image neonImage; // 네온 효과를 적용할 UI 이미지
    public Color baseColor = Color.cyan; // 기본 네온 색상
    public Color normalColor = Color.white; // 원래 화면 색상
    public AudioSource glitchSound; // 치지직 효과음 (선택 사항)

    private bool isFlickering = false;

    void Start()
    {
        if (neonImage == null)
        {
            Debug.LogError("네온 이미지가 설정되지 않았습니다!");
            return;
        }

        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = Random.Range(2.0f, 3.5f); // 원래 화면이 유지되는 시간 (더 길게)
            float flickerTime = Random.Range(0.05f, 0.2f); // 네온 빛나는 시간 (더 짧게)
            bool glitch = Random.value > 0.85f; // 15% 확률로 치지직 효과 발생

            // 원래 화면 유지 (더 오래)
            StartCoroutine(ChangeColor(normalColor, normalTime));
            yield return new WaitForSeconds(normalTime);

            if (glitch)
            {
                StartCoroutine(GlitchEffect());
                yield return new WaitForSeconds(0.1f);
            }

            // 짧게 네온 효과 반짝임
            StartCoroutine(ChangeColor(baseColor, flickerTime));
            yield return new WaitForSeconds(flickerTime);
        }
    }

    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play(); // 치지직 소리 재생

        neonImage.color = baseColor * 0.8f; // 살짝 어두워지도록
        yield return new WaitForSeconds(0.05f);
        neonImage.color = normalColor; // 원래 색으로 복구
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
