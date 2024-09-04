using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 각 슬라이더에 연결된 UI 요소
    public Slider sliderA;
    public Slider sliderB;
    public Slider sliderC;

    private StatusChange statusChange;

    void Start()
    {
        // 초기 StatusChange 값 설정
        statusChange = new StatusChange(0, 0, 0);
        UpdateSliders();
    }

    // 슬라이더를 업데이트하는 메서드
    public void UpdateSliders()
    {
        sliderA.value = statusChange.statusChangeA;
        sliderB.value = statusChange.statusChangeB;
        sliderC.value = statusChange.statusChangeC;
    }

    // 상태 변경 후 슬라이더 업데이트
    public void ApplyStatusChange(StatusChange change)
    {
        statusChange = change;
        UpdateSliders();
    }
}