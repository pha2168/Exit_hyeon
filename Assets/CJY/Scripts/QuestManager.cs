using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // UI 텍스트로 퀘스트 상태를 보여줌
    public Text questUIText;

    // 퀘스트 상태
    private string currentQuest;
    private bool questCompleted;

    void Start()
    {
        // 퀘스트 시작 시 초기화
        currentQuest = "Collect 5 apples";
        questCompleted = false;
        UpdateQuestUI();
    }

    // 퀘스트를 업데이트하는 함수
    public void UpdateQuest(string newQuest)
    {
        currentQuest = newQuest;
        questCompleted = false;
        UpdateQuestUI();
    }

    // 퀘스트를 완료했을 때 호출하는 함수
    public void CompleteQuest()
    {
        questCompleted = true;
        UpdateQuestUI();
    }

    // UI에 퀘스트 상태를 업데이트하는 함수
    private void UpdateQuestUI()
    {
        if (questCompleted)
        {
            questUIText.text = "Quest Completed: " + currentQuest;
        }
        else
        {
            questUIText.text = "Current Quest: " + currentQuest;
        }
    }
}
