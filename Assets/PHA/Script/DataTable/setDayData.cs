using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setDayData : MonoBehaviour
{
    private int day;

    [SerializeField]
    private DayData day1;
    public DayData Day1 { set { day1 = value; } }

    [SerializeField]
    private DayData day2;
    public DayData Day2 { set { day2 = value; } }

    [SerializeField]
    private DayData day3;
    public DayData Day3 { set { day3 = value; } }

    void Start()
    {
        Datamanager.Instance.LoadGameData();
        day = Datamanager.Instance.data.NowDay;
        UnityEngine.Debug.Log(day);
    }

    public float setDropSpeed()
    {
        switch (day)
        {
            case 0:
                return day1.DropSpeed;
            case 1:
                return day1.DropSpeed;
            case 2:
                return day2.DropSpeed;
            case 3:
                return day3.DropSpeed;
            default:
                return 1.0f;
        }
    }

    public int setScoreMax()
    {
        switch (day)
        {
            case 0:
                return day1.ScoreMax;
            case 1:
                return day1.ScoreMax;
            case 2:
                return day2.ScoreMax;
            case 3:
                return day3.ScoreMax;
            default:
                return 100;
        }
    }
}
