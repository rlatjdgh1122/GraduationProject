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
    public int Price;                               //���� ����
    public bool Locked;                             //������ ����ִ°�
    public int MaxCount;                           //�ִ� ���� ���� ��
}

public class LegionInventoryManager : Singleton<LegionInventoryManager>
{
    public int CurrentLegion { get; private set; }

    public LegionChange LegionChange { get; private set; } = null;
    private LegionInventory _legionInven = null;
    private UnitInventory _unitInven = null;
    private UnitInformationUI _unitInfo = null;
    private PenguinSituationUI _penguinSituation = null;

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
        _penguinSituation = FindObjectOfType<PenguinSituationUI>();
    }

    /// <summary>
    /// ������ ������
    /// </summary>
    /// <param name="selectSO">������</param>
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
    }

    /// <summary>
    /// ���ܿ� �ִ� ����� �׾�����
    /// </summary>
    /// <param name="so">��� ����</param>
    /// <param name="legionName">���� �̸�</param>
    /// <param name="legionPosition">���� �ȿ����� ��ġ</param>
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
    /// ���� �̸� �ٲٱ�
    /// </summary>
    /// <param name="legionNumber">�ٲ� ���� ��ȣ</param>
    /// <param name="name">�ٲ� �̸�</param>
    public void LegionNameChange(int legionNumber, string name)
    {
        _legionInven.ChangeLegionNameInSaveData(_legionList[legionNumber].Name, name);

        _legionList[legionNumber].Name = name;
    }

    /// <summary>
    /// ���� ���� �ٲٱ�
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
        Debug.Log("���� �̸� ��ã�� ���ϳ�");
        return -1;
    }

    /// <summary>
    /// ���� ���ܿ� ��� ����ϱ�
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <param name="infoData"></param>
    public void LegionRegistration(int slotPosition, EntityInfoDataSO infoData)
    {
        _legionInven.LegionRegistration(slotPosition, infoData);
    }

    /// <summary>
    /// ���� ���ܿ��� ��� ����
    /// </summary>
    /// <param name="slotPosition"></param>
    public void RemovePenguinInCurrentLegion(int slotPosition)
    {
        _legionInven.RemovePenguinInCurrentLegion(slotPosition);
    }

    /// <summary>
    /// ���� �����ϱ�
    /// </summary>
    public void SaveLegion()
    {
        _legionInven.SaveLegion();
    }

    /// <summary>
    /// ���� ���� ����ϱ�
    /// </summary>
    public void UndoLegion()
    {
        _legionInven.UndoLegion();
    }

    /// <summary>
    /// ���� ���ܿ��� ���� ������ �ִٸ�
    /// </summary>
    public bool ChangedInCurrentLegion()
    {
        return _legionInven.ChangedInCurrentLegion();
    }

    /// <summary>
    /// ���� �ٲٱ�
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
    /// ���� ������ �ִ� �ο� ���� �ʰ��ߴٸ�
    /// </summary>
    public bool ExcedLimitOfLegion()
    {
        return _legionInven.ExcedLimitOfLegion(CurrentLegion);
    }

    public List<LegionInfo> LegionList()
    {
        return _legionList;
    }

    public void ShowPenguinSituation(EntityInfoDataSO so, float percent, int penguinPrice)
    {
        _penguinSituation.SetPenguinSituation(so, percent, penguinPrice);
    }
}