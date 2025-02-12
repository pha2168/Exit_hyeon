using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 요소 사용을 위해 필요

public class NeonUIEffect : MonoBehaviour
{
    public Image neonImage;
    public Color[] neonColors = { Color.cyan, Color.magenta, Color.yellow, Color.green };
    public Color normalColor = new Color(1f, 1f, 1f, 0.3f);
    public AudioSource glitchSound;

    private bool isFlickering = false;
    private Color currentColor;
    private int colorIndex = 0;

    void Start()
    {
        if (neonImage == null)
        {
            Debug.LogError("네온 이미지가 설정되지 않았습니다!");
            return;
        }

        currentColor = normalColor;
        neonImage.color = currentColor;
        StartCoroutine(FlickerEffect());
    }

    IEnumerator FlickerEffect()
    {
        while (true)
        {
            float normalTime = Random.Range(2.0f, 3.5f);
            float flickerTime = Random.Range(0.5f, 1.5f); // 자연스러운 색상 변화 시간

            bool glitch = Random.value > 0.85f;

            // 현재 색상을 저장
            Color previousColor = neonColors[colorIndex];

            // 다음 네온 색상으로 변경 (순환)
            colorIndex = (colorIndex + 1) % neonColors.Length;
            Color newNeonColor = neonColors[colorIndex];
            newNeonColor.a = 0.3f;

            // 이전 색상과 자연스럽게 연결되도록
            yield return StartCoroutine(ChangeColor(newNeonColor, flickerTime));

            if (glitch)
            {
                yield return StartCoroutine(GlitchEffect());
            }
        }
    }


    IEnumerator GlitchEffect()
    {
        if (glitchSound != null)
            glitchSound.Play();

        Color glitchColor = new Color(1f, 1f, 1f, 0.6f);
        StartCoroutine(ChangeColor(glitchColor, 0.1f));
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(ChangeColor(currentColor, 0.2f));
    }

    IEnumerator ChangeColor(Color targetColor, float duration)
    {
        if (isFlickering) yield break;

        isFlickering = true;
        float elapsed = 0f;
        Color startColor = neonImage.color;
        currentColor = targetColor;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            neonImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        isFlickering = false;
    }
}
