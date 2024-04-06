using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LegionInfo
{
    public int Price;                               //군단 가격
    public bool Locked;                             //군단이 잠겨있는가
    public int MaxCount;                           //최대 군단 병사 수
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
    /// 선택한 데이터
    /// </summary>
    /// <param name="selectSO">데이터</param>
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
    /// 인벤에 추가할 펭귄
    /// </summary>
    /// <param name="data"></param>
    public void AddPenguin(EntityInfoDataSO data)
    {
        _unitInven.PenguinSlotEnter(data);
    }


    /// <summary>
    /// 인벤에서 펭귄 지우기
    /// </summary>
    /// <param name="data">지워질 펭귄 데이터</param>
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