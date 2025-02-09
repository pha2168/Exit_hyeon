using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public Text objectCountText; // 모든 태그의 정보를 표시할 하나의 텍스트
    public List<string> tagsToTrack; // 확인할 태그 리스트

    void Start()
    {
        UpdateAllTagCounts(); // 초기 UI 업데이트
    }

    void Update()
    {
        // 매 프레임마다 업데이트하지 않고, 일정 주기마다 갱신
        if (Time.frameCount % 30 == 0) // 30프레임마다 실행
        {
            UpdateAllTagCounts();
        }
    }

    // 모든 태그의 오브젝트 수를 업데이트
    private void UpdateAllTagCounts()
    {
        string updatedText = ""; // 텍스트를 저장할 변수

        foreach (var tag in tagsToTrack)
        {
            int count = CountObjectsWithTag(tag);
            updatedText += $"{tag}: {count}개\n"; // 한 줄씩 추가
        }

        objectCountText.text = updatedText; // 텍스트 UI 갱신
    }

    // 특정 태그를 가진 오브젝트 수를 계산
    public int CountObjectsWithTag(string tagName)
    {
        return GameObject.FindGameObjectsWithTag(tagName).Length;
    }
}
