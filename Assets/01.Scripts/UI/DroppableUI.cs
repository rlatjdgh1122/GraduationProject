using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DroppableUI : MonoBehaviour, IPointerEnterHandler, IDropHandler, IPointerExitHandler
{
    private Image image;
    private RectTransform rect;
    private Color color; //초기 컬러 저장용

    private void Awake()
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        color = image.color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.transform.SetParent(transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = rect.position;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.black;
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = color;
    }
}
