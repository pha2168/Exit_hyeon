using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingQuest : MonoBehaviour
{
    public enum EndingType
    {
        NormalEnding,
        Ending1,
        Ending2
    }

    // 퀘스트 상태 관리
    private Dictionary<string, bool> questStatus = new Dictionary<string, bool>();
    private string currentActiveQuest = ""; // 현재 활성화된 퀘스트

    // UI 컴포넌트 참조
    public Text activeQuestText; // 현재 활성화된 퀘스트 표시

    private void Start()
    {
        // 모든 퀘스트 초기화
        InitializeQuests();

        // 초기 퀘스트 활성화
        ActivateQuest("1-1");

        // UI 초기화
        UpdateUI();
    }

    private void InitializeQuests()
    {
        // 엔딩 1 관련 퀘스트
        questStatus["1-1"] = false;
        questStatus["1-2"] = false;
        questStatus["1-3"] = false;

        // 엔딩 2 관련 퀘스트
        questStatus["2-1"] = false;
        questStatus["2-2"] = false;
        questStatus["2-3"] = false;
    }

    private void ActivateQuest(string questId)
    {
        if (!questStatus.ContainsKey(questId))
        {
            Debug.LogWarning($"퀘스트 {questId}는 존재하지 않습니다.");
            return;
        }

        currentActiveQuest = questId; // 활성화된 퀘스트 설정
        //Debug.Log($"퀘스트 {questId}가 활성화되었습니다!");
        UpdateUI();
    }

    public void CompleteQuest(string questId)
    {
        if (!questStatus.ContainsKey(questId))
        {
            //Debug.LogWarning($"퀘스트 {questId}는 존재하지 않습니다.");
            return;
        }

        if (currentActiveQuest != questId)
        {
            //Debug.LogWarning($"현재 활성화된 퀘스트가 아닙니다: {questId}");
            return;
        }

        // 완료 처리
        questStatus[questId] = true;
        Debug.Log($"퀘스트 {questId} 완료!");

        // 이후 퀘스트 활성화 처리
        UnlockNextQuests(questId);
    }

    private void UnlockNextQuests(string completedQuestId)
    {
        switch (completedQuestId)
        {
            case "1-1":
                ActivateQuest("1-2");
                break;
            case "1-2":
                ActivateQuest("1-3");
                break;
            case "1-3":
                //Debug.Log("엔딩 1 퀘스트 완료!");
                currentActiveQuest = ""; // 퀘스트 종료
                break;
            case "2-1":
                ActivateQuest("2-2");
                break;
            case "2-2":
                ActivateQuest("2-3");
                break;
            case "2-3":
                //Debug.Log("엔딩 2 퀘스트 완료!");
                currentActiveQuest = ""; // 퀘스트 종료
                break;
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (!string.IsNullOrEmpty(currentActiveQuest))
        {
            activeQuestText.text = $"현재 퀘스트: {currentActiveQuest}";
        }
        else
        {
            activeQuestText.text = "모든 퀘스트 완료!";
        }
    }

    private void Update()
    {
        // 1번 키를 눌렀을 때 1-1 퀘스트 완료
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentActiveQuest == "1-1")
        {
            CompleteQuest("1-1");
        }

        // 2번 키를 눌렀을 때 1-2 퀘스트 완료
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentActiveQuest == "1-2")
        {
            CompleteQuest("1-2");
        }

        // 3번 키를 눌렀을 때 1-3 퀘스트 완료
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentActiveQuest == "1-3")
        {
            CompleteQuest("1-3");
        }
    }
}
