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
    public Text questText1_1; // 첫 번째 퀘스트 순서 텍스트
    public Text questText2_1; // 두 번째 퀘스트 순서 텍스트

    public Image questImage1;
    public Image questImage2;
    public List<Sprite> questSprites; // 퀘스트 아이콘 리스트
    public List<string> questTags; // 퀘스트 관련 태그 리스트

    public UI_Score uI_Score;

    private bool isGameStarted = false;  // 게임 시작 여부 플래그

    private void Start()
    {
        InitializeQuests(); // 퀘스트 초기화
        GenerateDynamicQuests(); // 동적으로 조합된 퀘스트 생성
        UpdateQuestUI();    // UI 초기화
    }

    private void Update()
    {
        if (!isGameStarted) return;  // 게임이 시작되기 전에는 퀘스트 검사 X

        // 퀘스트 진행 상황 업데이트
        // 주의: currentCount가 자동 증가하는지 확인
        foreach (QuestScrip quest in activeQuests)
        {
            if (!quest.isCompleted && quest.questType == QuestType1.Count)
            {
                //Debug.Log($"[DEBUG] {quest.title}: {quest.currentCount}/{quest.requiredCount}");
                if (quest.currentCount >= quest.requiredCount)
                {
                    quest.isCompleted = true;
                    
                    GenerateNextQuest();
                }
            }
        }

        UpdateQuestUI(); // UI 업데이트
    }
    void EnableQuestChecking()
    {
        isGameStarted = true;  // 퀘스트 검사 활성화
    }

    private void InitializeQuests()
    {
        foreach (QuestScrip quest in questScripts)
        {
            quest.ResetQuest();
            quest.isCompleted = false;
            quest.currentCount = 0; // 명확한 초기화
            quest.requiredCount = Mathf.Max(1, quest.requiredCount); // 최소 1 보장
        }
    }

    private void GenerateDynamicQuests()
    {
        activeQuests.Clear();

        // 랜덤으로 두 개의 퀘스트를 선택
        List<QuestScrip> randomQuests = SelectRandomQuests();

        foreach (QuestScrip baseQuest in randomQuests)
        {
            // 동적으로 생성된 퀘스트 인스턴스 생성
            QuestScrip dynamicQuest = ScriptableObject.CreateInstance<QuestScrip>();

            // 기존 퀘스트 데이터 복사
            dynamicQuest.title = baseQuest.title;
            dynamicQuest.questType = baseQuest.questType;
            dynamicQuest.questOrder = baseQuest.questOrder;
            dynamicQuest.tag = baseQuest.tag; // 태그는 그대로 유지

            // 퀘스트의 내용을 동적으로 수정
            ModifyQuestContent(baseQuest, dynamicQuest);

            dynamicQuest.ResetQuest(); // 초기화

            activeQuests.Add(dynamicQuest);
        }

        //Debug.Log($"총 {activeQuests.Count}개의 동적 퀘스트가 생성되었습니다.");

        // UI 업데이트 호출
        UpdateQuestUI();
    }

    private List<QuestScrip> SelectRandomQuests()
    {
        // questScripts 리스트를 섞습니다.
        for (int i = questScripts.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            QuestScrip temp = questScripts[i];
            questScripts[i] = questScripts[randomIndex];
            questScripts[randomIndex] = temp;
        }

        // 처음 두 개의 퀘스트를 선택합니다.
        List<QuestScrip> randomQuests = new List<QuestScrip>();

        if (questScripts.Count >= 2)
        {
            randomQuests.Add(questScripts[0]);
            randomQuests.Add(questScripts[1]);

            // 선택된 퀘스트를 리스트의 맨뒤로 이동합니다.
            questScripts.Add(questScripts[0]);
            questScripts.Add(questScripts[1]);

            // 맨앞의 두 퀘스트를 삭제합니다.
            questScripts.RemoveAt(0);
            questScripts.RemoveAt(0);
        }

        return randomQuests;
    }


    private void GenerateNextQuest()
    {
        // 완료된 퀘스트가 있으면 그 자리에 새로운 퀘스트를 추가
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i].isCompleted)
            {
                activeQuests[i].ResetQuest(); // 기존 퀘스트 리셋

                if (questScripts.Count > 0)
                {
                    Debug.Log("일반퀘스트 클리어");
                    uI_Score.AddScore(30);

                    // 리스트의 다음 퀘스트를 가져오고, 맨뒤로 이동
                    QuestScrip nextQuest = questScripts[0];
                    questScripts.RemoveAt(0);
                    questScripts.Add(nextQuest);

                    // 새 퀘스트로 데이터 갱신
                    activeQuests[i].title = nextQuest.title;
                    activeQuests[i].questType = nextQuest.questType;
                    activeQuests[i].questOrder = nextQuest.questOrder;
                    activeQuests[i].tag = nextQuest.tag;

                    if (nextQuest.questType == QuestType1.Destroy)
                    {
                        activeQuests[i].requiredCount = Random.Range(3, 10);
                    }
                    else
                    {
                        activeQuests[i].requiredCount = Mathf.Max(1, nextQuest.requiredCount); // 최소 1개 이상
                    }

                    activeQuests[i].ResetQuest(); // 초기화
                }

                break;
            }
        }

        UpdateQuestUI();
    }

    private void ModifyQuestContent(QuestScrip baseQuest, QuestScrip dynamicQuest)
    {
        if (baseQuest.questType == QuestType1.Destroy)
        {
            dynamicQuest.requiredCount = Random.Range(3, 10); // 최소 3개 이상 필요
        }
        else
        {
            dynamicQuest.requiredCount = baseQuest.requiredCount; // 기존 값 유지
        }

        dynamicQuest.currentCount = 0; // 명확한 초기화 추가
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
                    // 퀘스트 완료 시 자동으로 다음 퀘스트 보이기
                    GenerateNextQuest();
                }
            }
        }
        // UI 업데이트 호출
        UpdateQuestUI();
    }

    private void UpdateQuestUI()
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            QuestScrip quest = activeQuests[i];

            if (i == 0)
            {
                UpdateQuestText(quest, questText1, questText1_1);
                UpdateQuestImage(quest, questImage1);
            }
            else if (i == 1)
            {
                UpdateQuestText(quest, questText2, questText2_1);
                UpdateQuestImage(quest, questImage2);
            }
        }

        // 퀘스트가 부족하면 UI 요소 비활성화
        if (activeQuests.Count < 1) questText1.gameObject.SetActive(false);
        if (activeQuests.Count < 2) questText2.gameObject.SetActive(false);
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

    private string GetRandomTag()
    {
        // 랜덤 태그를 반환 (사용자 요구에 따라 변경 가능)
        string[] possibleTags = { "CleanHouse", "WeaponStore", "Crime", "Hospital", "TrashHouse", "Store" };
        return possibleTags[Random.Range(0, possibleTags.Length)];
    }

    private void UpdateQuestImage(QuestScrip quest, Image questImage)
    {
        if (quest != null && questImage != null && questSprites.Count == questTags.Count)
        {
            int index = questTags.IndexOf(quest.tag); // 퀘스트 태그에 맞는 이미지 찾기
            if (index != -1)
            {
                questImage.sprite = questSprites[index]; // 이미지 설정
                questImage.gameObject.SetActive(true);
            }
            else
            {
                questImage.gameObject.SetActive(false); // 이미지 없음
            }
        }
    }
}
