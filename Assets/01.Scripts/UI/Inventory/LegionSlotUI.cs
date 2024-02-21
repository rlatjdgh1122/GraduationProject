using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegionSlotUI : SlotUI, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _slotImage;
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
        _unitImage.enabled = false;
        _unitImage.sprite = null;
    }

    public override void UpdateSlot(LegionInventoryData data)
    {
        _unitImage.sprite = data.penguinData.PenguinIcon;

        //내가 추가한거 - 성호

        var info = new ArrangementInfo
        {
            legionIdx = _legionNumber,
            SlotIdx = _slotNumber - 1, //배열은 0부터 시작하는거 까먹엇당 ㅎ
            Type = data.penguinData.PenguinType
        };

        ArrangementTest.Instance.AddArrangementInfo(info);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_data != null && Input.GetKey(KeyCode.LeftControl))
        {
            LegionInventory.Instance.RemoveLegion(_data.penguinData, _legionNumber - 1); //이 슬롯의 데이터를 군단 인벤에서 제거

            CleanUpSlot(); //슬롯 초기화
        }

        if (_data == null && _info._data != null) //UnitInformationUI의 데이터가 비어있지 않고 이 슬롯의 데이터가 비어있으면
        {
            _data = _info._data; //이 슬롯의 데이터는 UnitInformationUI의 데이터


            _unitImage.enabled = true;
            //if (_data.penguinData.JobType == PenguinJobType.General &&
            //    LegionInventory.Instance.LegionList[_legionNumber - 1].generalLimit) return;

            LegionInventory.Instance.RemovePenguin(_info._data.penguinData); //UnitInformationUI의 데이터를 지우고
            LegionInventory.Instance.AddToLegion(_data.penguinData, _legionNumber - 1); //이 슬롯의 데이터와 이 슬롯의 군단번호를 군단 인벤에 넘겨줌

            UpdateSlot(_data);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _slotImage.color = new Color(255, 255, 255, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        _slotImage.color = new Color(255,255,255,0);
    }
}
