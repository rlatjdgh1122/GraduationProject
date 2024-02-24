using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSlotUI : SlotUI, IPointerDownHandler
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private UnitInformationUI _info;
    [SerializeField] private Image _lockImg;

    private bool _locked;

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
            _locked = false;
            _text.text = $"{_data.stackSize} 마리";
        }
        else
        {
            _locked = true;
            _text.text = "0 마리";
        }
        
        _lockImg.gameObject.SetActive(_locked);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!_locked)
        {
            _info.UpdateSlot(_data);
        }
    }
}
