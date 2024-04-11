using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class LegionInfo
{
    public string Name;
    public int Price;                               //군단 가격
    public bool Locked;                             //군단이 잠겨있는가
    public int MaxCount;                           //최대 군단 병사 수
}

public class LegionInventoryManager : Singleton<LegionInventoryManager>
{
    public int CurrentLegion { get; private set; }

    public LegionChange LegionChange { get; private set; } = null;

    private LegionInventory _legionInven = null;
    private UnitInventory _unitInven = null;
    private UnitInformationUI _unitInfo = null;

    private UnitInventoryData _selectData;
    public UnitInventoryData SelectData => _selectData;

    public List<LegionInfo> _legionList = new();
    public int LegionCount => _legionList.Count;


    public override void Awake()
    {
        _unitInven = FindObjectOfType<UnitInventory>();
        _legionInven = FindObjectOfType<LegionInventory>();
        _unitInfo = FindObjectOfType<UnitInformationUI>();
        LegionChange = FindObjectOfType<LegionChange>();
    }

    /// <summary>
    /// 선택한 데이터
    /// </summary>
    /// <param name="selectSO">데이터</param>
    /// <param name="size"></param>
    public void SelectInfoData(UnitInventoryData selectSO)
    {
        _selectData = selectSO;

        _unitInfo.ShowInformation(_selectData);
    }
    public void RemoveStack()
    {
        _selectData.RemoveStack();
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
    }

    /// <summary>
    /// 군단에 있는 펭귄이 죽었으면
    /// </summary>
    /// <param name="so">펭귄 정보</param>
    /// <param name="legionName">군단 이름</param>
    /// <param name="legionPosition">군단 안에서의 위치</param>
    public void DeadLegionPenguin(EntityInfoDataSO so, string legionName, int legionPosition)
    {
        /*LegionInventoryData data = new LegionInventoryData(so, legionName, legionPosition);
        Debug.Log(so.GetInstanceID());
        _legionInven.DeadPenguin(data);*/
    }
    public void DeadLegionPenguin(string legionName, int slotIdx)
    {
        _legionInven.DeadPenguin(legionName, slotIdx);
    }

    public void TakeDamagePenguinInLegion(EntityInfoDataSO so, string legionName, int legionPosition, int maxHP, int curHP)
    {
        LegionInventoryData data = new LegionInventoryData(so, legionName, legionPosition);

        _legionInven.DamagePenguin(data, (float)curHP / (float)maxHP);
    }

    /// <summary>
    /// 군단 이름 바꾸기
    /// </summary>
    /// <param name="legionNumber">바꿀 군단 번호</param>
    /// <param name="name">바꿀 이름</param>
    public void LegionNameChange(int legionNumber, string name)
    {
        _legionInven.ChangeLegionNameInSaveData(_legionList[legionNumber].Name, name);

        _legionList[legionNumber].Name = name;
    }

    /// <summary>
    /// 현재 군단 바꾸기
    /// </summary>
    /// <param name="number"></param>
    public void ChangeLegionNumber(int number)
    {
        CurrentLegion = number;
    }

    public int GetLegionIdxByLegionName(string name)
    {
        for (int i = 0; i < _legionList.Count; i++)
        {
            if (_legionList[i].Name == name) return i;
        }
        Debug.Log("군단 이름 못찾음 뭐하냐");
        return -1;
    }

    /// <summary>
    /// 현재 군단에 펭귄 등록하기
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <param name="infoData"></param>
    public void LegionRegistration(int slotPosition, EntityInfoDataSO infoData)
    {
        _legionInven.LegionRegistration(slotPosition, infoData);
    }

    /// <summary>
    /// 현재 군단에서 펭귄 빼기
    /// </summary>
    /// <param name="slotPosition"></param>
    public void RemovePenguinInCurrentLegion(int slotPosition)
    {
        _legionInven.RemovePenguinInCurrentLegion(slotPosition);
    }

    /// <summary>
    /// 군단 저장하기
    /// </summary>
    public void SaveLegion()
    {
        _legionInven.SaveLegion();
    }

    /// <summary>
    /// 군단 저장 취소하기
    /// </summary>
    public void UndoLegion()
    {
        _legionInven.UndoLegion();
    }

    /// <summary>
    /// 현재 군단에서 변경 사항이 있다면
    /// </summary>
    public bool ChangedInCurrentLegion()
    {
        return _legionInven.ChangedInCurrentLegion();
    }

    /// <summary>
    /// 군단 바꾸기
    /// </summary>
    public void ChangeLegion(int legionNumber)
    {
        _legionInven.ChangeLegion(_legionList[legionNumber].Name);
    }

    public bool LimitOfGeneral()
    {
        return _legionInven.LimitOfGeneral();
    }

    /// <summary>
    /// 현재 군단의 최대 인원 수를 초과했다면
    /// </summary>
    public bool ExcedLimitOfLegion()
    {
        return _legionInven.ExcedLimitOfLegion(CurrentLegion);
    }

    public List<LegionInfo> LegionList()
    {
        return _legionList;
    }
}