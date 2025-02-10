using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialQuestManager : MonoBehaviour
{
    public List<SpecialQuest> quests; // 총 12개의 퀘스트를 저장하는 리스트


    // 세력별 + 단계별 퀘스트를 저장할 Dictionary
    private Dictionary<FactionType, Dictionary<int, SpecialQuest>> questDictionary;

    private TetriminoStatus tetriminoStatus;
    private CountBlock CountBlock;

    private SpecialQuest NowPublicAuthorityQuest;
    private SpecialQuest NowRevolutionaryArmyQuest;
    private SpecialQuest NowCultQuest;
    private SpecialQuest NowCrimeSyndicateQuest;

    public Text PublicAuthorityText1; // 공권력 첫 번째 퀘스트 텍스트
    public Text PublicAuthorityText2;
    public Text PublicAuthorityText3;
    public Text PublicCountWeaponText;
    public Text PublicCountCleanText;
    public GameObject PublicCleanImage;
    public Text PublicFloorText;

    public Text RevolutionaryArmyText1; // 혁명군 첫 번째 퀘스트 텍스트
    public Text RevolutionaryArmyText2;
    public Text RevolutionaryArmyText3;
    public Text RevolutionsentimentyText;
    public Text RevolutionWeaponText;

    public Text CultText1; // 사이비 첫 번째 퀘스트 텍스트
    public Text CultText2;
    public Text CultText3;
    public Text CultCleanText;
    public Text CultTroubleText;
    public GameObject CultTroubleImage;
    public Text CultFloorText;

    public Text CrimeSyndicateText1; // 범죄집단 첫 번째 퀘스트 텍스트
    public Text CrimeSyndicateText2;
    public Text CrimeSyndicateText3;
    public Text CrimeTroubleText;
    public Text CrimeCountText;

    private void Start()
    {
        foreach (var quest in quests)
        {
            quest.ResetQuest();
            //Debug.Log($"[QuestManager] {quest.name} 초기화됨");
        }

        tetriminoStatus = FindObjectOfType<TetriminoStatus>();
        CountBlock = FindObjectOfType<CountBlock>();
        
        // 게임 데이터 로드 후 세력별 퀘스트를 설정
        NowPublicAuthorityQuest = GetQuest(FactionType.PublicAuthority, Datamanager.Instance.data.PublicAuthority_Step);
        NowRevolutionaryArmyQuest = GetQuest(FactionType.RevolutionaryArmy, Datamanager.Instance.data.RevolutionaryArmy_Step);
        NowCultQuest = GetQuest(FactionType.Cult, Datamanager.Instance.data.Cult_Step);
        NowCrimeSyndicateQuest = GetQuest(FactionType.CrimeSyndicate, Datamanager.Instance.data.CrimeSyndicate_Step);

        // 퀘스트 정보 출력 (디버그용)
        //Debug.Log(NowPublicAuthorityQuest.questTitle);  // 퀘스트 제목
        //Debug.Log(NowRevolutionaryArmyQuest.countBlock_WeaponStore); // 퀘스트 설명
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);


        //세력별 퀘스트 유아이
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
    }

    private void Update()
    {
        //공권력 퀘스트
        NowPublicAuthorityQuest.floorLimitcount = Grid3D.GridHeightLine();
        //Debug.Log((Grid3D.GridHeightLine()));
        //Debug.Log(NowPublicAuthorityQuest.floorLimitcount);

        //혁명군 퀘스트
        NowRevolutionaryArmyQuest.statusCount_sentiment = (int)tetriminoStatus.GetSliderAValue();
        NowRevolutionaryArmyQuest.countBlock_WeaponStore = CountBlock.CountObjectsWithTag("WeaponStore");
        //Debug.Log(CountBlock.CountObjectsWithTag("WeaponStore"));
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);
        //Debug.Log(NowRevolutionaryArmyQuest.statusCount_sentiment);

        //사이비 퀘스트
        NowCultQuest.statusCount_clear = (int)tetriminoStatus.GetSliderBValue();
        NowCultQuest.statusCount_trouble = (int)tetriminoStatus.GetSliderCValue();
        NowCultQuest.floorLimitcount = Grid3D.GridHeightLine();

        //범죄집단 퀘스트
        NowCrimeSyndicateQuest.statusCount_trouble = (int)tetriminoStatus.GetSliderCValue();

        //테스트 용도 텍스트 값 바꿔줘야함
        UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        UpdateRevolutionaryArmyText(NowRevolutionaryArmyQuest, RevolutionaryArmyText1, RevolutionaryArmyText2, RevolutionaryArmyText3);
        UpdateCultText(NowCultQuest, CultText1, CultText2, CultText3);
        UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);

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

    public void SpecialQuestObjectDestroyed(string objectTag)
    {
        if (NowPublicAuthorityQuest != null)
        {
            // 무기 상점 파괴 카운트 증가
            if (objectTag == "WeaponStore")
            {
                NowPublicAuthorityQuest.countBlock_WeaponStore++;
            }
            // 주거 공간 파괴 카운트 증가 (2단계 이상일 때만 적용)
            else if (objectTag == "CleanHouse" && NowPublicAuthorityQuest.questStep >= 2)
            {
                NowPublicAuthorityQuest.countBlock_CleanHouse++;
            }
            UpdatePublicText(NowPublicAuthorityQuest, PublicAuthorityText1, PublicAuthorityText2, PublicAuthorityText3);
        }

        if (NowCrimeSyndicateQuest != null)
        {
            // 주거공간 또는 상점 파괴 카운트 증가
            if (objectTag == "CleanHouse" || objectTag == "Store")
            {
                NowCrimeSyndicateQuest.countBlock_CleanHouse_Store++;
            }
            //UpdateCrimeSyndicateText(NowCrimeSyndicateQuest, CrimeSyndicateText1, CrimeSyndicateText2, CrimeSyndicateText3);
        }

    }

    public void CheckPublicAuthorityQuestComplete()
    {
        if (NowPublicAuthorityQuest == null)
            return;

        // 기본 무기상점 파괴 조건 확인
        bool weaponStoreCleared = NowPublicAuthorityQuest.countBlock_WeaponStore >= NowPublicAuthorityQuest.requiredBlock_WeaponStore;

        // 층수 제한 확인
        bool floorLimitCleared = NowPublicAuthorityQuest.floorLimitcount <= NowPublicAuthorityQuest.floorLimit;

        // 주거공간 파괴 조건 (2단계 이상일 때만 체크)
        bool cleanHouseCleared = NowPublicAuthorityQuest.questStep == 1 ||
                                  NowPublicAuthorityQuest.countBlock_CleanHouse >= NowPublicAuthorityQuest.requiredBlock_CleanHouse;

        // 모든 조건 충족 시 퀘스트 완료 처리
        if (weaponStoreCleared && cleanHouseCleared && floorLimitCleared)
        {
            Datamanager.Instance.data.PublicAuthority_Step++;
            Datamanager.Instance.SaveGameData();

            //Debug.Log("공권력 퀘스트 완료! 다음 단계로 이동.");
        }
    }

    public void CheckRevolutionaryArmyQuestComplete()
    {
        if (NowRevolutionaryArmyQuest == null)
            return;

        // 민심 조건 확인
        bool sentimentCleared = NowRevolutionaryArmyQuest.statusCount_sentiment >= NowRevolutionaryArmyQuest.statusRequired_sentiment;

        // 무기 상점 개수 조건 확인
        bool weaponStoreCleared = NowRevolutionaryArmyQuest.countBlock_WeaponStore >= NowRevolutionaryArmyQuest.requiredBlock_WeaponStore;

        // 모든 조건 충족 시 퀘스트 완료 처리
        if (sentimentCleared && weaponStoreCleared)
        {
            Datamanager.Instance.data.RevolutionaryArmy_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("혁명군 퀘스트 완료! 다음 단계로 이동.");
        }
    }

    public void CheckCultQuestComplete()
    {
        if (NowCultQuest == null)
            return;

        // 청결도 조건 확인
        bool cleanlinessCleared = NowCultQuest.statusCount_clear >= NowCultQuest.statusRequired_clear;

        // 분쟁 수치 조건 (2단계 이상에서만 체크)
        bool troubleCleared = NowCultQuest.questStep == 1 || NowCultQuest.statusCount_trouble >= NowCultQuest.statusRequired_trouble;

        // 층수 제한 조건 확인
        bool floorLimitCleared = NowCultQuest.floorLimitcount >= NowCultQuest.floorLimit;

        // 모든 조건 충족 시 퀘스트 완료 처리
        if (cleanlinessCleared && troubleCleared && floorLimitCleared)
        {
            Datamanager.Instance.data.Cult_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("사이비 퀘스트 완료! 다음 단계로 이동.");
        }
    }

    public void CheckCrimeSyndicateQuestComplete()
    {
        if (NowCrimeSyndicateQuest == null)
            return;

        // 분쟁 수치 조건 확인
        bool troubleCleared = NowCrimeSyndicateQuest.statusCount_trouble >= NowCrimeSyndicateQuest.statusRequired_trouble;

        // 주거공간 + 상점 파괴 개수 조건 확인
        bool cleanHouseStoreCleared = NowCrimeSyndicateQuest.countBlock_CleanHouse_Store >= NowCrimeSyndicateQuest.requiredBlock_CleanHouse_Store;

        // 모든 조건 충족 시 퀘스트 완료 처리
        if (troubleCleared && cleanHouseStoreCleared)
        {
            Datamanager.Instance.data.CrimeSyndicate_Step++;
            Datamanager.Instance.SaveGameData();

            Debug.Log("범죄집단 퀘스트 완료! 다음 단계로 이동.");
        }
    }

    private void UpdatePublicText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1}";
                PublicCountWeaponText.text = $"{quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}";
                PublicCountCleanText.text = "";
                PublicCleanImage.SetActive(false);
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1}";
                PublicCountWeaponText.text = $"{quest.countBlock_WeaponStore}/{quest.requiredBlock_WeaponStore}";
                PublicCountCleanText.text = $"{quest.countBlock_CleanHouse}/{quest.requiredBlock_CleanHouse}";
                PublicCleanImage.SetActive(true);
            }

            questOrderText2.text = $"{quest.questDescription2}";
            PublicFloorText.text = $"{quest.floorLimitcount}/{quest.floorLimit}";
        }
    }

    private void UpdateRevolutionaryArmyText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            questOrderText1.text = $"{quest.questDescription1}";
            RevolutionsentimentyText.text = $"{quest.statusCount_sentiment}/{quest.statusRequired_sentiment}";

            questOrderText2.text = $"{quest.questDescription2}";
            RevolutionWeaponText.text = $"{quest.countBlock_WeaponStore}개";
        }
    }

    private void UpdateCultText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            if (quest.questStep == 1)
            {
                questOrderText1.text = $"{quest.questDescription1}";
                CultCleanText.text = $"{quest.statusCount_clear}/{quest.statusRequired_clear}";
                CultTroubleText.text = $"";
                CultTroubleImage.SetActive(false);
            }
            else
            {
                questOrderText1.text = $"{quest.questDescription1}";
                CultCleanText.text = $"{quest.statusCount_clear}/{quest.statusRequired_clear}";
                CultTroubleText.text = $"{quest.statusCount_trouble}/{quest.statusRequired_trouble}";
                CultTroubleImage.SetActive(true);
            }

            questOrderText2.text = $"{quest.questDescription2}";
            CultFloorText.text = $"{quest.floorLimitcount}/{quest.floorLimit}";
        }
    }

    private void UpdateCrimeSyndicateText(SpecialQuest quest, Text questText, Text questOrderText1, Text questOrderText2)
    {
        if (quest != null && questText != null && questOrderText1 != null && questOrderText1 != null)
        {
            questText.text = $"QUEST. {quest.questStep} {quest.questTitle}";

            questOrderText1.text = $"{quest.questDescription1}";
            CrimeTroubleText.text = $"{quest.statusCount_trouble}/{quest.statusRequired_trouble}";
            
            questOrderText2.text = $"{quest.questDescription2}";
            CrimeCountText.text = $"{quest.countBlock_CleanHouse_Store}/{quest.requiredBlock_CleanHouse_Store}";
        }
    }
}