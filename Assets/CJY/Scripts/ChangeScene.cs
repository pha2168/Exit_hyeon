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
}
