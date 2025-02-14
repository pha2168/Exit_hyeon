using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;

public class DialogueManager : MonoBehaviour
{
    public StoryLog storyLog; // ScriptableObject 데이터
    public UnityEngine.UI.Text dialogueText; // 대화 텍스트 UI
    public UnityEngine.UI.Text characterNameText; // 캐릭터 이름 표시용 UI
    public UnityEngine.UI.Image characterImage; // 캐릭터 이미지
    public float typingSpeed = 0.05f; // 타이핑 속도
    public int questProgress = 1; // 현재 퀘스트 진행도 (1일차, 2일차 등)

    public CanvasGroup fadeCanvas; // 화면 페이드 효과
    public float fadeDuration = 1.0f; // 페이드 인/아웃 속도
    public float fadeHoldTime = 1.5f; // 어두운 상태 유지 시간

    private string[] currentLogs; // 현재 표시할 로그 배열
    private int logIndex = 0; // 현재 출력 중인 줄 인덱스
    private Coroutine typingCoroutine; // 현재 실행 중인 타이핑 효과
    private bool isTyping = false; // 타이핑 중인지 여부
    private bool isFading = false; // 페이드 중인지 여부

    public LogManager logManager;

    void Start()
    {
        fadeCanvas.alpha = 0; // 시작 시 밝은 화면
        characterImage.gameObject.SetActive(false); // 처음에는 이미지 숨김
        dialogueText.text = "";
        characterNameText.text = ""; // 캐릭터 이름 초기화
        //LoadDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFading)
        {
            if (isTyping)
            {
                // 타이핑 중이면 전체 문장 즉시 출력
                StopCoroutine(typingCoroutine);
                dialogueText.text = currentLogs[logIndex - 1];
                isTyping = false;
            }
            else
            {
                // 다음 문장 출력
                ShowNextLine();
            }
        }
    }

    public void setImage(UnityEngine.UI.Image image)
    {
        characterImage = image;
    }

    public void setStoryLog(StoryLog log)
    {
        storyLog = log;
    }

    public void setProgress(int progress)
    {
        questProgress = progress;
    }

    public void setName(string name)
    {
        characterNameText.text = name;
    }

    public void LoadDialogue()
    {
        string dayName = "day" + questProgress; // 예: "Day1", "Day2"

        foreach (var entry in storyLog.LogEntries)
        {
            if (entry.dayName == dayName)
            {
                currentLogs = entry.logs;
                logIndex = 0;
                characterImage.gameObject.SetActive(true); // 대화 시작 시 이미지 활성화
                //characterNameText.text = "캐릭터 이름"; // 캐릭터 이름 설정 (필요 시 변경 가능)
                ShowNextLine();
                return;
            }
        }

        // 해당 퀘스트 진행도에 맞는 로그가 없을 경우
        dialogueText.text = "대화가 없습니다.";
    }

    void ShowNextLine()
    {
        if (!isFading && (currentLogs == null || logIndex >= currentLogs.Length))
        {
            StartCoroutine(FadeToBlack());
            return;
        }

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // 기존 타이핑 중지
        }

        typingCoroutine = StartCoroutine(TypeSentence(currentLogs[logIndex])); // 새 문장 출력
        logIndex++; // 다음 줄로 이동
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = ""; // 텍스트 초기화

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator FadeToBlack()
    {
        isFading = true;

        // 1. 화면을 어둡게 (페이드 아웃)
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            yield return null;
        }

        // 어두운 상태에서 이미지 숨김 & 텍스트 초기화
        characterImage.gameObject.SetActive(false);
        dialogueText.text = "";
        characterNameText.text = "";

        yield return new WaitForSeconds(fadeHoldTime); // 일정 시간 유지

        logManager.playStory = false;

        // 2. 화면을 다시 밝게 (페이드 인)
        elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvas.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        isFading = false;
    }
}
