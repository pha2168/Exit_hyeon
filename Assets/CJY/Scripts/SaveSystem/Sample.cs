using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
    void Start()
    {
        Datamanager.Instance.LoadGameData();

        Debug.Log("게임 데이터 파일 경로: " + Application.persistentDataPath);

    }

    void Update()
    {

    }

    //게임을 종료하면 자동저장
    private void OnApplicationQuit()
    {
        Datamanager.Instance.SaveGameData();
    }


    public void ClearDayGame()
    {
        if (Datamanager.Instance.data.NowDay < 3)
            Datamanager.Instance.data.NowDay += 1;

        Datamanager.Instance.SaveGameData();
    }

    public void BackDayGame()
    {
        if (Datamanager.Instance.data.NowDay > 0)
            Datamanager.Instance.data.NowDay -= 1;

        Datamanager.Instance.SaveGameData();
    }
}
