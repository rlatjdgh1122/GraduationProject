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
        _data = null; //������ �ʱ�ȭ
        CurrentLegion = LegionInventory.Instance.LegionList[_legionNumber - 1];
    }

    public override void CleanUpSlot() //���� �ʱ�ȭ
    {
        _data = null;
        _unitImage.enabled = false;
        _unitImage.sprite = null;
    }

    public override void UpdateSlot(LegionInventoryData data)
    {
        _unitImage.sprite = data.penguinData.PenguinIcon;

        //���� �߰��Ѱ� - ��ȣ

        var info = new ArrangementInfo
        {
            legionIdx = _legionNumber,
            SlotIdx = _slotNumber - 1, //�迭�� 0���� �����ϴ°� ��Ծ��� ��
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

        if (_data != null && Input.GetKey(KeyCode.LeftControl)) //�� ������ �����͸� ���� �κ����� ����
        {
            if(_data.penguinData.JobType == PenguinJobType.General)
            {
                CurrentLegion.MaxGereral = false;
            }

            LegionInventory.Instance.RemoveLegion(_data.penguinData, _legionNumber - 1);

            CleanUpSlot(); //���� �ʱ�ȭ

            CurrentLegion.CurrentCount--;
        }

        //UnitInformationUI�� �����Ͱ� ������� �ʰ� �� ������ �����Ͱ� ����ְ� ������� ���ܿ� �������� ������
        if (_data == null && _info._data != null && CurrentLegion.CurrentCount < CurrentLegion.MaxCount)
        {
            if (CurrentLegion.MaxGereral
                && _info._data.penguinData.JobType == PenguinJobType.General)
            {

                LegionInventory.Instance.ShowMessage("�屺�� ���ܿ� ���ԵǾ� �ֽ��ϴ�!");
                return; //���� ���ܿ� �屺�� �ִٸ� ����

            }


            _data = _info._data; //�� ������ �����ʹ� UnitInformationUI�� ������

            if(_data.penguinData.JobType == PenguinJobType.General)
            {
                CurrentLegion.MaxGereral = true;
            }

            _unitImage.enabled = true;


            LegionInventory.Instance.RemovePenguin(_info._data.penguinData); //UnitInformationUI�� �����͸� �����
            LegionInventory.Instance.AddToLegion(_data.penguinData, _legionNumber - 1); //�� ������ �����Ϳ� �� ������ ���ܹ�ȣ�� ���� �κ��� �Ѱ���

            UpdateSlot(_data);

            CurrentLegion.CurrentCount++;
        }

        if(CurrentLegion.CurrentCount >= CurrentLegion.MaxCount)
        {
            LegionInventory.Instance.ShowMessage("������ ����á���ϴ�!");
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
