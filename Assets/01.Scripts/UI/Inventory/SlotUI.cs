using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class SlotUI : MonoBehaviour, IPointerDownHandler
{
    protected Image unitImage;

    public abstract void EnterSlot(EntityInfoDataSO data);
    public abstract void ExitSlot(EntityInfoDataSO data);

    public abstract void UpdateSlot();

    public abstract void OnPointerDown(PointerEventData eventData);
}
