using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LegionSlotUI : SlotUI, IPointerDownHandler
{
    [SerializeField] protected UnitInformationUI _info;
    [SerializeField] private int _legionNumber;
    [SerializeField] private int _slotNumber;

 

    private void Awake()
    {
        _data = null;
    }

    public override void CleanUpSlot()
    {
        _data = null;
        _unitImage.sprite = _emptyImage;
    }

    public override void UpdateSlot(LegionInventoryData data)
    {
        _unitImage.sprite = data.penguinData.PenguinIcon;

        //내가 추가한거 - 성호

        var info = new ArrangementInfo
        {
            legionIdx = _legionNumber,
            SlotIdx = _slotNumber,
            Type = data.penguinData.PenguinType
        };

        ArrangementTest.Instance.InfoList.Add(info);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_data != null && Input.GetKey(KeyCode.LeftControl))
        {
            LegionInventory.Instance.RemoveLegion(_data.penguinData, _legionNumber - 1);

            CleanUpSlot();
        }

        if (_data == null && _info._data != null)
        {
            _data = _info._data;

            //if (_data.penguinData.JobType == PenguinJobType.General &&
            //    LegionInventory.Instance.LegionList[_legionNumber - 1].generalLimit) return;

            LegionInventory.Instance.RemovePenguin(_info._data.penguinData);
            LegionInventory.Instance.AddToLegion(_data.penguinData, _legionNumber - 1);

            UpdateSlot(_data);
        }
    }
}
