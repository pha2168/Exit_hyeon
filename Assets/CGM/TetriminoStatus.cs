using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetriminoStatus : MonoBehaviour
{
    // 추적할 태그 리스트
    public string[] tagsToTrack = { "CleanHouse", "WeaponStore", "Crime", "Hospital", "TrashHouse", "Store" };

    // 태그별 카운트를 Inspector에서 직접 확인할 수 있도록 public 변수로 선언
    private float TAG_A_Count;
    private float TAG_B_Count;
    private float TAG_C_Count;
    private float TAG_D_Count;
    private float TAG_E_Count;
    private float TAG_F_Count;

    public float TAG_Count_Mid;
    public float TAG_Count_X;

    // 각 슬라이더에 연결된 UI 요소
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;

    // 태그별로 영역 안의 오브젝트를 저장하는 딕셔너리
    private Dictionary<string, List<GameObject>> taggedObjects = new Dictionary<string, List<GameObject>>();
    private Dictionary<string, int> taggedObjectCounts = new Dictionary<string, int>();

    void Start()
    {
        sliderA.maxValue = TAG_Count_Mid * 2;
        sliderB.maxValue = TAG_Count_Mid * 2;
        sliderC.maxValue = TAG_Count_Mid * 2;

        // 모든 태그에 대해 초기화
        foreach (string tag in tagsToTrack)
        {
            taggedObjects[tag] = new List<GameObject>();
            taggedObjectCounts[tag] = 0; // 초기 개수를 0으로 설정
        }
    }

    void Update()
    {

        // 매 프레임마다 카운트를 업데이트
        UpdatePublicCounts();

        sliderA.value = TAG_Count_Mid + (TAG_A_Count * TAG_Count_X - TAG_B_Count * TAG_Count_X);
        sliderB.value = TAG_Count_Mid + (TAG_C_Count * TAG_Count_X - TAG_D_Count * TAG_Count_X);
        sliderC.value = TAG_Count_Mid + (TAG_E_Count * TAG_Count_X - TAG_F_Count * TAG_Count_X);

        // 삭제된 오브젝트를 정리하는 함수 호출
        CleanupTaggedObjects();
    }

    private void CleanupTaggedObjects()
    {
        foreach (string tag in tagsToTrack)
        {
            // 현재 태그에 대한 오브젝트 리스트를 가져오기
            List<GameObject> objects = taggedObjects[tag];

            // 삭제되었거나 null인 오브젝트를 리스트에서 제거
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                if (objects[i] == null)
                {
                    objects.RemoveAt(i);
                    taggedObjectCounts[tag]--; // 카운트 감소
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        foreach (string tag in tagsToTrack)
        {
            if (other.CompareTag(tag))
            {
                if (!taggedObjects[tag].Contains(other.gameObject))
                {
                    taggedObjects[tag].Add(other.gameObject);
                    taggedObjectCounts[tag]++;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (string tag in tagsToTrack)
        {
            if (other.CompareTag(tag))
            {
                if (taggedObjects[tag].Contains(other.gameObject))
                {
                    taggedObjects[tag].Remove(other.gameObject);
                    taggedObjectCounts[tag]--;
                }
            }
        }
    }

    // Inspector에 노출되는 public 변수 업데이트
    private void UpdatePublicCounts()
    {
        TAG_A_Count = taggedObjectCounts.ContainsKey("CleanHouse") ? taggedObjectCounts["CleanHouse"] : 0;
        TAG_B_Count = taggedObjectCounts.ContainsKey("WeaponStore") ? taggedObjectCounts["WeaponStore"] : 0;
        TAG_C_Count = taggedObjectCounts.ContainsKey("Crime") ? taggedObjectCounts["Crime"] : 0;
        TAG_D_Count = taggedObjectCounts.ContainsKey("Hospital") ? taggedObjectCounts["Hospital"] : 0;
        TAG_E_Count = taggedObjectCounts.ContainsKey("TrashHouse") ? taggedObjectCounts["TrashHouse"] : 0;
        TAG_F_Count = taggedObjectCounts.ContainsKey("Store") ? taggedObjectCounts["Store"] : 0;
    }
    public float GetSliderAValue()
    {
        return sliderA.value;
    }

    public float GetSliderBValue()
    {
        return sliderB.value;
    }

    public float GetSliderCValue()
    {
        return sliderC.value;
    }

}