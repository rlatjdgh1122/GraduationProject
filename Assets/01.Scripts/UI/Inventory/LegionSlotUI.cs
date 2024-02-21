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
        if (_data != null && Input.GetKey(KeyCode.LeftControl))
        {
            LegionInventory.Instance.RemoveLegion(_data.penguinData, _legionNumber - 1); //�� ������ �����͸� ���� �κ����� ����

            CleanUpSlot(); //���� �ʱ�ȭ
        }

        if (_data == null && _info._data != null) //UnitInformationUI�� �����Ͱ� ������� �ʰ� �� ������ �����Ͱ� ���������
        {
            _data = _info._data; //�� ������ �����ʹ� UnitInformationUI�� ������


            _unitImage.enabled = true;
            //if (_data.penguinData.JobType == PenguinJobType.General &&
            //    LegionInventory.Instance.LegionList[_legionNumber - 1].generalLimit) return;

            LegionInventory.Instance.RemovePenguin(_info._data.penguinData); //UnitInformationUI�� �����͸� �����
            LegionInventory.Instance.AddToLegion(_data.penguinData, _legionNumber - 1); //�� ������ �����Ϳ� �� ������ ���ܹ�ȣ�� ���� �κ��� �Ѱ���

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
