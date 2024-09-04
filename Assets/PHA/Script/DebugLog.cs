using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLog : MonoBehaviour
{
    private QuestSuccessLogVisible questSuccessLogVisible;

    // Start is called before the first frame update
    void Start()
    {
        GameObject questLogObject = GameObject.Find("TextLogCanvas");

        if (questLogObject != null)
        {
            questSuccessLogVisible = questLogObject.GetComponent<QuestSuccessLogVisible>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(4);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(5);
            }
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            if (questSuccessLogVisible != null)
            {
                questSuccessLogVisible.ActivateLog(6);
            }
        }
    }
}
