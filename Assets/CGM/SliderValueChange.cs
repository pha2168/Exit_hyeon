using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueChange : MonoBehaviour
{
    public Slider slider;
    public Text valueText;

    void Start()
    {
        if (slider != null)
        {
            slider.onValueChanged.AddListener(UpdateValueText);
            UpdateValueText(slider.value); // 초기화 시에도 값 업데이트
        }
    }

    void UpdateValueText(float value)
    {
        if (valueText != null)
        {
            valueText.text = $"{value} / {slider.maxValue}";
        }
    }
}