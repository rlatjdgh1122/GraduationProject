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

    private Legion CurrentLegion;

    private void Awake()
    {
        _data = null; //데이터 초기화
        CurrentLegion = LegionInventory.Instance.LegionList[_legionNumber - 1];
    }

    public override void CleanUpSlot() //슬롯 초기화
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
        if(_data != null)
        {
            _info.InfoDataSlot(_data);
        }

        if (_data != null && Input.GetKey(KeyCode.LeftControl)) //이 슬롯의 데이터를 군단 인벤에서 제거
        {
            if(_data.penguinData.JobType == PenguinJobType.General)
            {
                CurrentLegion.MaxGereral = false;
            }

            LegionInventory.Instance.RemoveLegion(_data.penguinData, _legionNumber - 1);

            CleanUpSlot(); //슬롯 초기화

            CurrentLegion.CurrentCount--;
        }

        //UnitInformationUI의 데이터가 비어있지 않고 이 슬롯의 데이터가 비어있고 병사들이 군단에 꽉차있지 않으면
        if (_data == null && _info._data != null && CurrentLegion.CurrentCount < CurrentLegion.MaxCount)
        {
            if (CurrentLegion.MaxGereral
                && _info._data.penguinData.JobType == PenguinJobType.General)
            {

                LegionInventory.Instance.ShowMessage("장군이 군단에 포함되어 있습니다!");
                return; //만약 군단에 장군이 있다면 리턴

            }


            _data = _info._data; //이 슬롯의 데이터는 UnitInformationUI의 데이터

            if(_data.penguinData.JobType == PenguinJobType.General)
            {
                CurrentLegion.MaxGereral = true;
            }

            _unitImage.enabled = true;


            LegionInventory.Instance.RemovePenguin(_info._data.penguinData); //UnitInformationUI의 데이터를 지우고
            LegionInventory.Instance.AddToLegion(_data.penguinData, _legionNumber - 1); //이 슬롯의 데이터와 이 슬롯의 군단번호를 군단 인벤에 넘겨줌

            UpdateSlot(_data);

            CurrentLegion.CurrentCount++;
        }

        if(CurrentLegion.CurrentCount >= CurrentLegion.MaxCount)
        {
            LegionInventory.Instance.ShowMessage("군단이 가득찼습니다!");
        }
        

        LegionInventory.Instance.LegionCountInformation(_legionNumber - 1);
    }

    #region UI Show Event

    public void OnPointerEnter(PointerEventData eventData)
    {
        _slotImage.color = new Color(255, 255, 255, 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _slotImage.color = new Color(255,255,255,0);
    }

    #endregion
}
