using UnityEngine;
using UnityEngine.EventSystems;

public class HoverCanvasActivator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas targetCanvas; // 활성화할 Canvas

    void Start()
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // 처음에는 비활성화
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(true); // 마우스 호버 시 활성화
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (targetCanvas != null)
        {
            targetCanvas.gameObject.SetActive(false); // 마우스 벗어나면 비활성화
        }
    }
}
