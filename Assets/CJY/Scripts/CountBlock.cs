using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public List<Text> objectCountTexts; // 태그별로 개별 UI 할당
    public List<string> tagsToTrack; // 확인할 태그 리스트

    void Start()
    {
        UpdateAllTagCounts(); // 초기 UI 업데이트
    }

    void Update()
    {
        if (Time.frameCount % 30 == 0) // 30프레임마다 실행
        {
            UpdateAllTagCounts();
        }
    }

    // 모든 태그의 오브젝트 수를 업데이트
    private void UpdateAllTagCounts()
    {
        for (int i = 0; i < tagsToTrack.Count; i++)
        {
            if (i < objectCountTexts.Count) // UI 요소가 부족할 경우 대비
            {
                int count = CountObjectsWithTag(tagsToTrack[i]);
                objectCountTexts[i].text = $"{tagsToTrack[i]}: X {count}개";
            }
        }
    }

    // 특정 태그를 가진 오브젝트 수를 계산
    public int CountObjectsWithTag(string tagName)
    {
        return GameObject.FindGameObjectsWithTag(tagName).Length;
    }
}
