using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // 총 12개의 퀘스트를 저장하는 리스트

    // 세력별 + 단계별 퀘스트를 저장할 Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    private SpecialQuest NowPublicAuthorityQuest;
    private SpecialQuest NowRevolutionaryArmyQuest;
    private SpecialQuest NowCultQuest;
    private SpecialQuest NowCrimeSyndicateQuest;

    public Text PublicAuthorityText1; // 공권력 첫 번째 퀘스트 텍스트
    public Text PublicAuthorityText2;
    public Text PublicAuthorityText3;

    public Text RevolutionaryArmyText1; // 혁명군 첫 번째 퀘스트 텍스트
    public Text RevolutionaryArmyText2;
    public Text RevolutionaryArmyText3;

    public Text CultText1; // 사이비 첫 번째 퀘스트 텍스트
    public Text CultText2;
    public Text CultText3;

    public Text CrimeSyndicateText1; // 범죄집단 첫 번째 퀘스트 텍스트
    public Text CrimeSyndicateText2;
    public Text CrimeSyndicateText3;

    private void Start()
    {
        // 게임 데이터 로드 후 세력별 퀘스트를 설정
        NowPublicAuthorityQuest = GetQuest(FactionType.PublicAuthority, Datamanager.Instance.data.PublicAuthority_Step);
        NowRevolutionaryArmyQuest = GetQuest(FactionType.RevolutionaryArmy, Datamanager.Instance.data.RevolutionaryArmy_Step);
        NowCultQuest = GetQuest(FactionType.Cult, Datamanager.Instance.data.Cult_Step);
        NowCrimeSyndicateQuest = GetQuest(FactionType.CrimeSyndicate, Datamanager.Instance.data.CrimeSyndicate_Step);

        // 퀘스트 정보 출력 (디버그용)
        Debug.Log(NowPublicAuthorityQuest.questTitle);  // 퀘스트 제목
        Debug.Log(NowPublicAuthorityQuest.questDescription1);  // 퀘스트 설명

        //세력별 퀘스트 유아이
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        //UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        //UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        //UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
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
            if (!questDictionary[quest.factionType].ContainsKey(quest.questStep))
            {
                questDictionary[quest.factionType][quest.questStep] = quest;
            }
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

    private void UpdatePublicText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n 무기상점  ({quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore})";
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1} \n무기상점 ({quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}) 주거공간 ({quest.countBlock_CleanHouse}/{quest.requiredBlock_CleanHouse})";
            }

            questOrderText2.text = $"{quest.questDescription2} ({quest.floorLimitcount}/{quest.floorLimit})";
        }
    }

    private void UpdateRevolutionaryArmyText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n 현재 민심  ({quest.statusCount_sentiment})";
            }

            questOrderText2.text = $"{quest.questDescription2} \n 현재 무기상점 갯수 ({quest.countBlock_WeaponStore})";
        }
    }

    private void UpdateCultText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n 현재 청결도  ({quest.statusCount_clear})";
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1} \n 현재 청결도  ({quest.statusCount_clear}) 현재 분쟁 ({quest.statusCount_trouble})";
            }

            questOrderText2.text = $"{quest.questDescription2} ({quest.floorLimitcount}/{quest.floorLimit})";
        }
    }

    private void UpdateCrimeSyndicateText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1} \n 현재 분쟁  ({quest.statusCount_trouble})";
            }

            questOrderText2.text = $"{quest.questDescription2}  ({quest.countBlock_CleanHouse_Store}/{quest.requiredBlock_CleanHouse_Store})";
        }
    }
}