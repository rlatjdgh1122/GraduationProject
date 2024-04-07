using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LegionInfo
{
    public string Name;
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
    public int CurrentLegion { get; private set; }

    public LegionInventory LegionInven { get; private set; } = null;
    public LegionChange  LegionChange  { get; private set; } = null;

    private UnitInventory     _unitInven     = null;
    private UnitInformationUI _unitInfo      = null;

    private UnitInventoryData _selectData;
    public UnitInventoryData SelectData => _selectData;

    public List<LegionInfo> _legionList = new();
    public int LegionCount => _legionList.Count;

    public override void Awake()
    {
        _unitInven   = FindObjectOfType<UnitInventory>();
        LegionInven  = FindObjectOfType<LegionInventory>();
        _unitInfo    = FindObjectOfType<UnitInformationUI>();
        LegionChange = FindObjectOfType<LegionChange>();
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

    /// <summary>
    /// ���ܿ� �ִ� ����� �׾�����
    /// </summary>
    /// <param name="data"></param>
    public void DeadLegionPenguin(LegionInventoryData data)
    {
        LegionInven.DeadPenguin(data);
    }

    public void LegionNameChange(int legionNumber, string name)
    {
        LegionInven.ChangeName(_legionList[legionNumber].Name, name);

        _legionList[legionNumber].Name = name;
    }

    public string LegionName(int legionNumber)
    {
        return _legionList[legionNumber].Name;
    }

    public List<LegionInfo> LegionList()
    {
        return _legionList;
    }

    public void ChangeLegionNumber(int number)
    {
        CurrentLegion = number;
    }
}