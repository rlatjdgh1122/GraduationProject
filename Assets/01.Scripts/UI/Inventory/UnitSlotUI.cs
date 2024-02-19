using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSlotUI : SlotUI, IPointerDownHandler
{
    [SerializeField] protected TextMeshProUGUI _text;
    [SerializeField] protected UnitInformationUI _info;

    public override void CleanUpSlot()
    {
        _data = null;

        _unitImage.sprite = _emptyImage;
        _text.text = string.Empty;
        _info.CleanUpSlot();
    }


    public override void UpdateSlot(LegionInventoryData data)
    {
        _data = data;

        _unitImage.sprite = _data.penguinData.PenguinIcon;

        if (_data.stackSize > 0)
        {
            _text.text = _data.stackSize.ToString();
        }
        else
        {
            _text.text = string.Empty;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _info.UpdateSlot(_data);
    }
}
