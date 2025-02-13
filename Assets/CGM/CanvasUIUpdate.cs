using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIUpdate : MonoBehaviour
{
    void Update()
    {
        // 캔버스 업데이트 강제 실행
        Canvas.ForceUpdateCanvases();

        // 레이아웃 그룹 강제 업데이트
        foreach (var layout in GetComponentsInChildren<LayoutGroup>())
        {
            layout.enabled = false;
            layout.enabled = true;
        }
    }

}
