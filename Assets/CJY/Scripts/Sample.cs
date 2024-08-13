using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        Datamanager.Instance.LoadGameData();
    }

    void Update()
    {
        
    }

    //게임을 종료하면 자동저장
    private void OnApplicationQuit()
    {
        Datamanager.Instance.SaveGameData();
    }

    public void ChapterUnlock(int chapterNum)
    {
        //챕터 잠금해제
        Datamanager.Instance.data.isUnlock[chapterNum] = true;

        Datamanager.Instance.SaveGameData();
    }
}
