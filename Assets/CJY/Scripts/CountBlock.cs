using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountBlock : MonoBehaviour
{
    public List<Text> objectCountTexts; // 태그별로 개별 UI 할당
    public List<string> tagsToTrack; // 확인할 태그 리스트

    private int count;

    void Start()
    {
        UpdateAllTagCounts(); // 초기 UI 업데이트
        count = 0;
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
                objectCountTexts[i].text = $" X {count}";
            }
        }
    }

    // 특정 태그를 가진 오브젝트 수를 계산
    public int CountObjectsWithTag(string tagName)
    {
        Tetris_Tetrimino[] tetriminos = FindObjectsOfType<Tetris_Tetrimino>(); // 모든 Tetris_Tetrimino 찾기
        Debug.Log($"Tetris_Tetrimino 개수: {tetriminos.Length}");

        int count = 0;

        foreach (Tetris_Tetrimino tetrimino in tetriminos)
        {
            if (tetrimino.isLocked) // isLocked가 true인 경우만 확인
            {
                Debug.Log($"Tetris_Tetrimino {tetrimino.gameObject.name}의 isLocked가 true이므로 확인");

                foreach (Transform child in tetrimino.transform) // 자식 오브젝트 확인
                {
                    if (child.CompareTag(tagName)) // 특정 태그와 일치하면 카운트 증가
                    {
                        count++;
                    }
                }
            }
        }

        Debug.Log($"태그 [{tagName}]을 가진 자식 오브젝트 개수: {count}");
        return count;
    }

}