using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // 총 12개의 퀘스트를 저장하는 리스트

    public Data gameData; // 게임 데이터를 저장하는 객체
    public Datamanager datamanager;

    // 세력별 + 단계별 퀘스트를 저장할 Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    public Text questText1; // 첫 번째 퀘스트 텍스트
    public Text questText2; // 두 번째 퀘스트 텍스트
    public Text questText1_1; // 첫 번째 퀘스트 순서 텍스트
    public Text questText2_1; // 두 번째 퀘스트 순서 텍스트

    private void Start()
    {
        Datamanager.Instance.LoadGameData();

        Debug.Log("퀘스트 단계: " + Datamanager.Instance.data.PublicAuthority_Step);

    }

    private void Awake()
    {
        InitializeQuestDictionary(); // 게임 시작 시 Dictionary를 초기화
    }

    private void InitializeQuestDictionary()
    {
        questDictionary = new Dictionary<FactionType, Dictionary<int, SpecialQuest>>(); // Dictionary 객체 생성

        // 모든 퀘스트를 하나씩 순회하면서 Dictionary에 추가
        foreach (var quest in quests)
        {
            // 해당 세력 타입이 Dictionary에 없는 경우 새로 추가
            if (!questDictionary.ContainsKey(quest.factionType))
            {
                questDictionary[quest.factionType] = new Dictionary<int, SpecialQuest>(); // 세력 타입에 대한 Dictionary 생성
            }

            // 세력 타입 내에서 해당 단계에 대한 퀘스트를 저장
            questDictionary[quest.factionType][quest.questStep] = quest;
        }
    }

    // 특정 세력과 단계에 맞는 퀘스트를 가져오는 함수
    public SpecialQuest GetQuest(FactionType faction, int step)
    {
        // Dictionary에서 해당 세력 타입이 존재하는지 확인
        if (questDictionary.TryGetValue(faction, out var stepDict))
        {
            // 세력 내에서 해당 단계의 퀘스트가 존재하는지 확인 후 반환
            if (stepDict.TryGetValue(step, out var quest))
            {
                return quest; // 해당하는 퀘스트 반환
            }
        }
        return null; // 퀘스트가 존재하지 않으면 null 반환
    }
}
