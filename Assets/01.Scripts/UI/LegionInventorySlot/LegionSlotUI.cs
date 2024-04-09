using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegionSlotUI : SlotUI, IPointerEnterHandler, IPointerExitHandler
{
    public int Idx;

    private EntityInfoDataSO _data;

    private LegionInventoryManager _legion;

    private KeyCode _removeKey;
    private KeyCode _infoKey;

    protected override void Awake()
    {
        base.Awake();

        unitImage = transform.Find("Penguin").GetComponent<Image>();
        _legion  = LegionInventoryManager.Instance;
    }


    /// <summary>
    /// ������ ������� �� �����
    /// </summary>
    /// <param name="Index">��ġ</param>
    /// <param name="removeSlot">������ �����ִ� Ű</param>
    /// <param name="penguinInfo">�������� Ű</param>
    public void CreateSlot(int Index, KeyCode removeSlot, KeyCode penguinInfo)
    {
        Idx        = Index;
        _removeKey = removeSlot;
        _infoKey   = penguinInfo;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_data == null && _legion.SelectData == null) return;

        if (_data != null)
        {
            if (Input.GetKey(_removeKey)) //���� ����Ű�� ������
            {
                _legion.RemovePenguinInCurrentLegion(Idx);
                ExitSlot(null);
                return;
            }

            else if (Input.GetKey(_infoKey)) //���� ��������(���ܿ� �� ����� �����Ұ��� ȸ���� ����) Ű�� ������
            {
                //_legion.LegionInven.OpenPanel();
            }

            else if (_legion.SelectData != null) 
                return;

            else //���� �����͸� ��� ����â�� ����
            {
                UnitInventoryData data = new UnitInventoryData(_data, 1, SlotType.Legion);

                _legion.SelectInfoData(data);
            }
        }

        if(_legion.SelectData.InfoData.JobType == PenguinJobType.General && _legion.LimitOfGeneral()) //�屺�� �̹� ���ܿ� �ְ� �屺�� ���Կ� �߰��ҷ��� �ϸ�
        {
            UIManager.Instance.ShowWarningUI("�屺�� �̹� ���ܿ� ���ԵǾ� �ֽ��ϴ�!");
            return;
        }

        if (_legion.SelectData.InfoData.JobType != PenguinJobType.General && _legion.ExcedLimitOfLegion()) //���� ���� �ȿ� ����� �ִ� ��� ���� �Ѿ�����
        {
            return;
        }


        if(_data == null && _legion.SelectData != null) //���Կ� ������ �ֱ�
        {
            EnterSlot(_legion.SelectData.InfoData); //���� ����

            _legion.RemovePenguin(_legion.SelectData.InfoData);
            _legion.RemoveStack();

            _legion.LegionRegistration(Idx, _data); //����� ���� ���ܿ� ����ϱ�
        }
    }

    /// <summary>
    /// ������ �����͸� �ٲ���
    /// </summary>
    /// <param name="data"></param>
    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data == null) return;

        _data = data;
        UpdateSlot();
    }

    public override void UpdateSlot()
    {
        unitImage.enabled = true;
        unitImage.sprite = _data.PenguinIcon;
    }

    /// <summary>
    /// ���� ������ �ʱ�ȭ
    /// </summary>
    /// <param name="data"></param>
    public override void ExitSlot(EntityInfoDataSO data)
    {
        _data = null;
        unitImage.sprite = null;
        unitImage.enabled = false;
    }
}