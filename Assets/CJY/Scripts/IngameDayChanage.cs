using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameDayChanage : MonoBehaviour
{
    public Text DayText;
    private int Day;

    void Start()
    {
        //Datamanager.Instance.LoadGameData();
        Day = Datamanager.Instance.data.NowDay;
        DayText.text = $"{Day}";
    }

    // Update is called once per frame
    void Update()
    {
    }
}
