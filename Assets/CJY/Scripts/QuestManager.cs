using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [Header("Quest Settings")]
    public List<QuestScrip> questScripts; // 기본 퀘스트 데이터 리스트 (스크립터블 오브젝트)
    private List<QuestScrip> activeQuests = new List<QuestScrip>(); // 동적으로 조합된 퀘스트 리스트

    public Text questText1; // 첫 번째 퀘스트 텍스트
    public Text questText2; // 두 번째 퀘스트 텍스트
    public Text questText3; // 세 번째 퀘스트 텍스트
    public Text questText1_1; // 첫 번째 퀘스트 순서 텍스트
    public Text questText2_1; // 두 번째 퀘스트 순서 텍스트
    public Text questText3_1; // 세 번째 퀘스트 순서 텍스트

    private void Start()
    {
        InitializeQuests(); // 퀘스트 초기화
        GenerateDynamicQuests(); // 동적으로 조합된 퀘스트 생성
        UpdateQuestUI();    // UI 초기화
    }

    private void Update()
    {
        foreach (QuestScrip quest in activeQuests)
        {
            if (!quest.isCompleted && quest.questType == QuestType1.Count)
            {
                // 태그가 일치하는 오브젝트의 개수를 확인
                GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(quest.tag);
                quest.currentCount = objectsWithTag.Length;

                if (quest.currentCount >= quest.requiredCount)
                {
                    quest.isCompleted = true;
                    Debug.Log($"{quest.title} quest completed!");
                }
            }
        }

        // Tab 키로 퀘스트 순서 회전
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            RotateQuests();
        }

        UpdateQuestUI();
    }

    private void InitializeQuests()
    {
        // 모든 퀘스트 초기화
        foreach (QuestScrip quest in questScripts)
        {
            quest.ResetQuest();
        }
    }

    private void GenerateDynamicQuests()
    {
        activeQuests.Clear();

        foreach (QuestScrip baseQuest in questScripts)
        {
            // 동적으로 생성된 퀘스트 인스턴스 생성
            QuestScrip dynamicQuest = ScriptableObject.CreateInstance<QuestScrip>();

            // 기존 퀘스트 데이터 복사
            dynamicQuest.title = baseQuest.title;
            dynamicQuest.questType = baseQuest.questType;
            dynamicQuest.questOrder = baseQuest.questOrder;
            dynamicQuest.tag = baseQuest.tag; // 태그는 그대로 유지

            // Destroy 타입일 경우에만 requiredCount를 랜덤으로 변경
            if (baseQuest.questType == QuestType1.Destroy)
            {
                dynamicQuest.requiredCount = Random.Range(3, 10); // 랜덤 범위 설정 (예: 3 ~ 10)
            }
            else
            {
                dynamicQuest.requiredCount = baseQuest.requiredCount; // Count 타입은 기존 값 유지
            }

            dynamicQuest.ResetQuest(); // 초기화

            activeQuests.Add(dynamicQuest);
        }

        Debug.Log($"총 {activeQuests.Count}개의 동적 퀘스트가 생성되었습니다.");

        // UI 업데이트 호출
        UpdateQuestUI();
    }


    public void OnObjectDestroyed(string objectTag)
    {
        foreach (QuestScrip quest in activeQuests)
        {
            // 태그가 일치하고 파괴 퀘스트이며 완료되지 않은 경우
            if (quest.tag == objectTag && quest.questType == QuestType1.Destroy && !quest.isCompleted)
            {
                quest.IncrementCount(); // 카운트 증가
                Debug.Log($"[Destroy] {objectTag} destroyed. Progress: {quest.currentCount}/{quest.requiredCount}");

                // 퀘스트 완료 확인
                if (quest.currentCount >= quest.requiredCount)
                {
                    quest.isCompleted = true;
                    Debug.Log($"퀘스트 완료: {quest.title}");
                }
            }
        }

        // UI 업데이트 호출
        UpdateQuestUI();
    }


    private void UpdateQuestUI()
    {
        // activeQuests 리스트를 순회하며 UI 업데이트
        for (int i = 0; i < activeQuests.Count; i++)
        {
            QuestScrip quest = activeQuests[i];

            if (i == 0) UpdateQuestText(quest, questText1, questText1_1);
            else if (i == 1) UpdateQuestText(quest, questText2, questText2_1);
            else if (i == 2) UpdateQuestText(quest, questText3, questText3_1);
        }

        // 나머지 슬롯은 비활성화
        if (activeQuests.Count < 3)
        {
            if (questText1 != null && activeQuests.Count < 1) questText1.gameObject.SetActive(false);
            if (questText2 != null && activeQuests.Count < 2) questText2.gameObject.SetActive(false);
            if (questText3 != null && activeQuests.Count < 3) questText3.gameObject.SetActive(false);
        }
    }


    private void UpdateQuestText(QuestScrip quest, Text questText, Text questOrderText)
    {
        if (quest != null && questText != null && questOrderText != null)
        {
            questText.text = $"{quest.title} ({quest.currentCount}/{quest.requiredCount})";
            questOrderText.text = "QUEST. " + quest.questOrder;
            questText.gameObject.SetActive(true);
            questOrderText.gameObject.SetActive(true);
        }
        else if (questText != null && questOrderText != null)
        {
            questText.gameObject.SetActive(false);
            questOrderText.gameObject.SetActive(false);
        }
    }

    private void RotateQuests()
    {
        if (activeQuests.Count == 0) return;

        QuestScrip firstQuest = activeQuests[0];
        activeQuests.RemoveAt(0);
        activeQuests.Add(firstQuest);

        Debug.Log("퀘스트 순서를 변경했습니다.");
        UpdateQuestUI();
    }

    private string GetRandomTag()
    {
        // 랜덤 태그를 반환 (사용자 요구에 따라 변경 가능)
        string[] possibleTags = { "CleanHouse", "WeaponStore", "Crime", "Hospital", "TrashHouse", "Store" };
        return possibleTags[Random.Range(0, possibleTags.Length)];
    }
}
