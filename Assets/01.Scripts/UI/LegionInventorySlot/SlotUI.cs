using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SlotUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    protected Image unitImage;
    protected Image bkImage;
    protected Color bkColor;

    protected virtual void Awake()
    {
        bkImage = transform.Find("Image").GetComponent<Image>();

        bkColor = bkImage.color;
    }

    public abstract void EnterSlot(EntityInfoDataSO data);
    public abstract void ExitSlot(EntityInfoDataSO data);

    public abstract void UpdateSlot();

    public abstract void OnPointerDown(PointerEventData eventData);

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        bkColor.a = 1;
        bkImage.color = bkColor;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        bkColor.a = 0;
        bkImage.color = bkColor;
    }
}
