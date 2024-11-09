using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private QuestManager questManager;  // QuestManager 인스턴스에 대한 참조

    void Start()
    {
        // QuestManager 객체를 찾고 해당 컴포넌트를 가져옴
        questManager = GameObject.FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager를 씬에서 찾을 수 없습니다.");  // QuestManager가 없을 경우 에러 로그 출력
        }
    }

    void Update()
    {
        
    }

    // 오브젝트가 파괴될 때 자동으로 호출되는 함수
    void OnDestroy()
    {
        // 오브젝트가 파괴될 때 QuestManager의 OnObjectDestroyed 호출
        if (questManager != null)
        {
            questManager.OnObjectDestroyed(gameObject);  // 현재 오브젝트를 전달
        }
    }
}
