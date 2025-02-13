using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobbyDay : MonoBehaviour
{
    public Image DayImage; // UI에서 표시할 이미지
    public List<Sprite> daySprites; // Day1, Day2, Day3에 해당하는 스프라이트 리스트

    void Start()
    {
        //Datamanager.Instance.LoadGameData();
        UpdateDayImage(Datamanager.Instance.data.NowDay);
    }

    void UpdateDayImage(int day)
    {
        if (day >= 1 && day <= daySprites.Count) // 유효한 범위 확인
        {
            DayImage.sprite = daySprites[day - 1]; // NowDay 값에 맞는 스프라이트 적용
        }
    }
}
