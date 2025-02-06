using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FactionType
{
    PublicAuthority,
    RevolutionaryArmy,
    Cult,
    CrimeSyndicate
}

[CreateAssetMenu(fileName = "NewSpecialQuest", menuName = "Quest/SpecialQuest")]
public class SpecialQuest : ScriptableObject
{
    public string questTitle;          // 퀘스트 제목
    public string questDescription1;    // 퀘스트 설명
    public string questDescription2;    // 퀘스트 설명


    // 파괴해야 할 블록 수 (공권력, 혁명군, 범죄집단)
    public int requiredBlockCount_WeaponStore;
    public int requiredBlockCount_CleanHouse;
    public int requiredBlockCount_Store;

    // 필요 스테이더스 (혁명군, 사이비, 범죄집단)
    public int statusRequired_sentiment;
    public int statusRequired_clear;
    public int statusRequired_trouble;

    public int floorLimit;             // 층수 제한 (공권력, 사이비)

    public FactionType factionType;    // 세력 타입 (공권력, 혁명군, 사이비, 범죄집단)
    public int questStep;              // 퀘스트 단계 (1, 2, 3)
}

