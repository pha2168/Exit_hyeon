using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class UI_Score : MonoBehaviour
{
    public Slider scoreSlider;
    public TextMeshProUGUI scoreText;
    public Transform textPosition;         // 점수 획득 이펙트 위치
    public TextMeshProUGUI floatingTextPrefab;
    public int maxScore = 100;
    private int currentScore = 0;
    private TextMeshProUGUI activeFloatingText;

    public SpecialQuestManager SpecialQuestManager;

    public CanvasGroup canvasGroup; // CanvasGroup 참조
    public float fadeDuration = 3.0f; // 페이드 지속 시간
    private bool isQuestChecked = false; // 한 번만 실행되도록 플래그 추가

    public setDayData dataManger;
    public GameObject Clear;

    void Start()
    {
        canvasGroup.alpha = 0; // 처음엔 투명

        maxScore = dataManger.setScoreMax();
        UnityEngine.Debug.Log(maxScore);

        if (scoreSlider != null)
        {
            scoreSlider.maxValue = maxScore;
            scoreSlider.value = currentScore;
        }

        if (SpecialQuestManager == null)
        {
            SpecialQuestManager = FindObjectOfType<SpecialQuestManager>();
        }

        if (SpecialQuestManager == null)
        {
            Debug.LogError("UI_Score: SpecialQuestManager를 찾을 수 없습니다!");
        }

        UpdateScoreUI();
    }

    private void Update()
    {
        if (currentScore == maxScore && !isQuestChecked)
        {
            isQuestChecked = true; // 실행 후 다시 실행되지 않도록 설정
            SpecialQuestManager.CheckPublicAuthorityQuestComplete();
            SpecialQuestManager.CheckRevolutionaryArmyQuestComplete();
            SpecialQuestManager.CheckCultQuestComplete();
            SpecialQuestManager.CheckCrimeSyndicateQuestComplete();

            Clear.SetActive(true);

            Invoke("StartFadeOut", 1f);
        }
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        float startAlpha = canvasGroup.alpha; // 0이어야 함

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1, elapsedTime / fadeDuration); // 0 → 1
            yield return null;
        }

        canvasGroup.alpha = 1; // 완전히 불투명해짐
        Debug.Log("페이드 인 완료!");

        SceneManager.LoadScene("StoryScene");
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
