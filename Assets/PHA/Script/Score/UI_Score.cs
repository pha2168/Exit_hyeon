using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_Score : MonoBehaviour
{
    public Slider scoreSlider;
    public TextMeshProUGUI scoreText;
    public Transform textPosition;         // 점수 획득 이펙트 위치
    public TextMeshProUGUI floatingTextPrefab;
    public int maxScore = 100;
    private int currentScore = 0;
    private TextMeshProUGUI activeFloatingText;

    public setDayData dataManger;

    void Start()
    {
        maxScore = dataManger.setScoreMax();
        UnityEngine.Debug.Log(maxScore);

        if (scoreSlider != null)
        {
            scoreSlider.maxValue = maxScore;
            scoreSlider.value = currentScore;
        }

        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        currentScore = Mathf.Clamp(currentScore, 0, maxScore);

        if (scoreSlider != null)
        {
            scoreSlider.value = currentScore;
        }

        UpdateScoreUI();
        ShowFloatingText(amount);
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore} / {maxScore}";

        }
    }

    private void ShowFloatingText(int amount)
    {
        // 기존 텍스트가 있으면 삭제
        if (activeFloatingText != null)
        {
            StopAllCoroutines(); // 기존 애니메이션 중단
            Destroy(activeFloatingText.gameObject);
        }

        // 새 텍스트 생성
        activeFloatingText = Instantiate(floatingTextPrefab, textPosition.position, Quaternion.identity, textPosition);
        activeFloatingText.text = $"+{amount}";

        StartCoroutine(FadeOutAndMove(activeFloatingText));
    }

    private IEnumerator FadeOutAndMove(TextMeshProUGUI floatingText)
    {
        float duration = 1f; // 애니메이션 지속 시간
        float elapsed = 0f;
        Color color = floatingText.color;
        Vector3 startPos = floatingText.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 20, 0); // 위로 50px 이동

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            floatingText.color = new Color(color.r, color.g, color.b, alpha);
            floatingText.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
            yield return null;
        }

        Destroy(floatingText.gameObject); // 애니메이션 종료 후 삭제
        activeFloatingText = null; // 현재 활성화된 텍스트 초기화
    }
}
