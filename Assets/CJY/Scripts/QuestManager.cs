using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public Text questUIText;  // 퀘스트 정보를 보여줄 UI 텍스트
    private List<Quest> quests = new List<Quest>();  // 퀘스트 리스트

    void Start()
    {
        // 예시로 몇 가지 퀘스트 추가 (파괴 퀘스트와 카운트 퀘스트)
        AddQuest("Count 5 치료시설", "치료시설", 5, QuestType.Count);
        AddQuest("Count 3 쓰레기집", "쓰레기집", 3, QuestType.Count);
        AddQuest("Destroy 4 치료시설", "치료시설", 4, QuestType.Destroy);

        // UI 업데이트
        UpdateQuestUI();
    }

    void Update()
    {
        // Update마다 특정 태그를 가진 오브젝트 수가 미션 조건을 충족하는지 확인
        CompleteQuestIfTagCountMet("치료시설", 5); // "Clear" 태그를 가진 오브젝트가 5개일 때 퀘스트 완료
    }

    // 퀘스트 추가 함수
    public void AddQuest(string title, string tag, int requiredCount, QuestType questType)
    {
        quests.Add(new Quest(title, tag, requiredCount, questType));
        UpdateQuestUI();
    }

    // 특정 오브젝트가 파괴될 때 호출되는 함수 (파괴 퀘스트에만 해당)
    public void OnObjectDestroyed(GameObject destroyedObject)
    {
        string objectTag = destroyedObject.tag;

        // 파괴된 오브젝트의 태그를 가진 퀘스트를 확인하고 카운트 증가
        foreach (Quest quest in quests)
        {
            if (quest.tag == objectTag && !quest.isCompleted && quest.questType == QuestType.Destroy)
            {
                quest.IncrementCount();
                Debug.Log($"{objectTag} destroyed. Count: {quest.currentCount}/{quest.requiredCount}");

                // 퀘스트 완료 여부 확인
                CompleteQuestIfTagCountMet(objectTag, quest.requiredCount);
            }
        }

        // UI 업데이트
        UpdateQuestUI();
    }

    // 특정 태그를 가진 오브젝트 수가 목표치에 도달하면 퀘스트 완료 처리 (카운트 퀘스트에만 해당)
    public void CompleteQuestIfTagCountMet(string tag, int requiredCount)
    {
        // 태그를 가진 오브젝트 배열 가져오기
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // 현재 태그를 가진 오브젝트 수를 출력해 디버그하기
        Debug.Log($"Found {objectsWithTag.Length} objects with tag '{tag}'");

        // 카운트 퀘스트에 해당하는 오브젝트 수가 목표치에 도달하면 퀘스트 완료
        foreach (Quest quest in quests)
        {
            if (quest.tag == tag && !quest.isCompleted)
            {
                if (quest.questType == QuestType.Count)
                {
                    // 오브젝트 수 퀘스트의 currentCount를 업데이트
                    quest.currentCount = objectsWithTag.Length;

                    // 목표치를 달성했는지 확인
                    if (quest.currentCount >= requiredCount)
                    {
                        CompleteQuest(quest.title);
                    }
                }
            }
        }

        // UI 업데이트
        UpdateQuestUI();
    }

    // 퀘스트 완료 함수
    public void CompleteQuest(string title)
    {
        foreach (Quest quest in quests)
        {
            if (quest.title == title && !quest.isCompleted)
            {
                quest.isCompleted = true;
                Debug.Log($"{title} quest completed!");
                break;
            }
        }
        UpdateQuestUI();
    }

    // UI에 퀘스트 리스트 업데이트 함수
    private void UpdateQuestUI()
    {
        string questText = "";

        foreach (Quest quest in quests)
        {
            if (quest.isCompleted)
            {
                questText += "[Completed] " + quest.title + "\n";  // 완료된 퀘스트 표시
            }
            else
            {
                questText += "[In Progress] " + quest.title + $" ({quest.currentCount}/{quest.requiredCount})\n";  // 진행 중인 퀘스트 표시
            }
        }
        questUIText.text = questText;
    }
}
