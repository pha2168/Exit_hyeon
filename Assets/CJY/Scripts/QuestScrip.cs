using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType1
{
    Destroy,  // 파괴해야 하는 퀘스트
    Count     // 특정 수만큼 오브젝트가 존재해야 하는 퀘스트
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/Create New Quest")]
public class QuestScrip : ScriptableObject
{
    [Header("Quest Information")]
    public string title;          // 퀘스트 이름
    public string tag;            // 퀘스트에 해당하는 태그
    public int requiredCount;     // 목표 오브젝트 수 (파괴 또는 존재)
    public QuestType1 questType;   // 퀘스트 타입 (파괴 또는 카운트)
    public string questOrder;        // 퀘스트 순서

    [Header("Quest Progress")]
    public bool isCompleted;      // 퀘스트 완료 여부
    public int currentCount;      // 현재 파괴된 오브젝트 수

    // 파괴 퀘스트의 카운트를 증가시키고 퀘스트 완료 여부 확인
    public void IncrementCount()
    {
        if (questType == QuestType1.Destroy)
        {
            currentCount++;
        }
        if (currentCount >= requiredCount)
        {
            isCompleted = true;
            Debug.Log($"{title} quest completed!");
        }
    }

    // 퀘스트 초기화 메서드
    public void ResetQuest()
    {
        currentCount = 0;
        isCompleted = false;
    }
}
