using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LegionInfo
{
    public int Price;                               //���� ����
    public bool Locked;                             //������ ����ִ°�
    public int MaxCount;                           //�ִ� ���� ���� ��
}

public enum SlotType
{
    Unit,
    Legion
}

public class LegionInventoryManager : Singleton<LegionInventoryManager>
{
    public string CurrentLegionID;


    private UnitInventory     _unitInven     = null;
    private LegionInventory   _legionInven   = null;
    private UnitInformationUI _unitInfo      = null;

    private UnitInventoryData _selectData;
    public UnitInventoryData SelectData => _selectData;

    public override void Awake()
    {
        _unitInven   = FindObjectOfType<UnitInventory>();
        _legionInven = FindObjectOfType<LegionInventory>();
        _unitInfo    = FindObjectOfType<UnitInformationUI>();
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="selectSO">������</param>
    /// <param name="size"></param>
    public void SelectInfoData(UnitInventoryData selectSO, SlotType type = SlotType.Unit)
    {
        _selectData = selectSO;

        _unitInfo.ShowInformation(_selectData);

        if(type == SlotType.Legion)
        {
            _selectData = null;
        }
    }

    /// <summary>
    /// �κ��� �߰��� ���
    /// </summary>
    /// <param name="data"></param>
    public void AddPenguin(EntityInfoDataSO data)
    {
        _unitInven.PenguinSlotEnter(data);
    }


    /// <summary>
    /// �κ����� ��� �����
    /// </summary>
    /// <param name="data">������ ��� ������</param>
    public void RemovePenguin(EntityInfoDataSO data)
    {
        _unitInven.PenguinSlotExit(data);

        _selectData.RemoveStack();
    }

    public void AddLegion(LegionInventoryData data)
    {

    }

    public void RemoveLegion(LegionInventoryData data)
    {

    }
}