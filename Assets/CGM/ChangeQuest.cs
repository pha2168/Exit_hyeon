using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeQuest : MonoBehaviour
{
    public GameObject[] objects; // 4개의 UI 오브젝트 배열
    private int currentIndex = 0; // 현재 활성화된 시작 인덱스

    void Start()
    {
        UpdateObjects();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchObjects();
        }
    }

    void SwitchObjects()
    {
        // 현재 활성화된 두 개의 오브젝트를 비활성화
        objects[currentIndex].SetActive(false);
        objects[(currentIndex + 1) % 4].SetActive(false);

        // 다음 두 개의 오브젝트를 활성화
        currentIndex = (currentIndex + 1) % 4;
        int nextIndex = (currentIndex + 1) % 4;

        // 3번, 4번 → 4번, 1번이 되도록 순서 조정 + Hierarchy 순서 변경
        if (currentIndex == 3)
        {
            objects[3].SetActive(true); // 4번 활성화
            objects[0].SetActive(true); // 1번 활성화

            // Hierarchy에서 4번이 1번보다 위로 가도록 설정
            objects[3].transform.SetAsLastSibling(); // 4번을 마지막으로 이동
            objects[0].transform.SetAsLastSibling(); // 1번을 마지막으로 이동 (4번 위에 배치)
        }
        else
        {
            objects[currentIndex].SetActive(true);
            objects[nextIndex].SetActive(true);

            // Hierarchy에서 올바른 순서로 UI 정렬
            objects[currentIndex].transform.SetAsLastSibling();
            objects[nextIndex].transform.SetAsLastSibling();
        }
    }

    void UpdateObjects()
    {
        // 초기 설정: 1번, 2번만 활성화
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i < 2);
        }
    }
}