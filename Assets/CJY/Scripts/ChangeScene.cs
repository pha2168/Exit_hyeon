using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void Select()
    {
        SceneManager.LoadScene("Select");
    }

    public void CJYScene()
    {
        SceneManager.LoadScene("CJYScene");
    }

    public void MainTittleScene()
    {
        SceneManager.LoadScene("MainTittleScene");
    }

    public void InGameScene()
    {
        SceneManager.LoadScene("InGame");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // 에디터 종료
#else
    Application.Quit();  // 빌드된 게임 종료
#endif
    }
}
