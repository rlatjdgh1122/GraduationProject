using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Legion
{
    public int price;
    public bool Locked;
    public List<LegionInventoryData> legionInven;
    public Dictionary<PenguinStat, LegionInventoryData> legionDictionary = new();
}

public class LegionInventory : Singleton<LegionInventory>
{
    [Header("PenguinInventory")]
    //==========================================
    //��� �κ��丮

    //�屺
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinStat, LegionInventoryData> generalDictionary = new();

    //����
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinStat, LegionInventoryData> soliderDictionary = new();
    //==========================================

    [Header("LegionInventory")]
    //���� �κ��丮
    public List<Legion> LegionList;

    [Header("Inventory UI")]
    [SerializeField] private Transform _soliderParent;
    [SerializeField] private Transform _warloadParent;

    private UnitSlotUI[] _soliderSlots;
    private UnitSlotUI[] _generalSlots;

    [Header("PenguinSO")]
    [SerializeField] private PenguinStat[] _generalSO;
    [SerializeField] private PenguinStat[] _soliderSO;

    public override void Awake()
    {
        base.Awake();

        _soliderSlots = _soliderParent.GetComponentsInChildren<UnitSlotUI>();
        _generalSlots = _warloadParent.GetComponentsInChildren<UnitSlotUI>();
    }

    private void Start()
    {
        if(_soliderSlots.Length != _soliderSO.Length) Debug.LogError("���� ������ �ʹ� ���ų� ����");
        if(_generalSlots.Length != _generalSO.Length) Debug.LogError("�屺 ������ �ʹ� ���ų� ����");

        InsertToStart(_generalSO);
        InsertToStart(_soliderSO);
    }

    public void InsertToStart(PenguinStat[] penguinSO)
    {
        for (int i = 0; i < penguinSO.Length; i++)
        {
            AddPenguin(penguinSO[i]);
            RemovePenguin(penguinSO[i]);
        }
    }

    public void AddPenguin(PenguinStat type) //��� �߰��ϴ� �Լ�(��� Ÿ������ �з�)
    {
        if (type.JobType == PenguinJobType.General) //���� �屺�̸�
        {
            AddToWarLoad(type);
        }
        else if (type.JobType == PenguinJobType.Solider) //���� �����
        {
            AddToSolider(type);
        }

        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < _soliderSlots.Length; i++)
        {
            _soliderSlots[i].CleanUpSlot();
        }
        for (int i = 0; i < _generalSlots.Length; i++)
        {
            _generalSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < generalInven.Count; ++i)
        {
            _generalSlots[i].UpdateSlot(generalInven[i]);
        }
        for (int i = 0; i < soliderInven.Count; ++i)
        {
            _soliderSlots[i].UpdateSlot(soliderInven[i]);
        }

    }

    #region ��� �κ��� ��� �߰�

    public void AddToWarLoad(PenguinStat penguin)
    {
        if (generalDictionary.TryGetValue(penguin, out LegionInventoryData legionInven)) //���� ��� �κ��� ������
        {
            legionInven.AddStack(); //�� ����
        }
        else //������
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            generalInven.Add(newInven); //�屺�� ������ �߰�
            generalDictionary.Add(penguin, newInven);
        }
    }

    public void AddToSolider(PenguinStat penguin)
    {
        if (soliderDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))//���� ��� �κ��� ������
        {
            legionInven.AddStack();//�� ����
        }
        else//������
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            soliderInven.Add(newInven);//���翡 ������ �߰�
            soliderDictionary.Add(penguin, newInven);
        }
    }

#endregion

    public void RemovePenguin(PenguinStat penguin, int count = 1)
    {
        if (generalDictionary.TryGetValue(penguin, out LegionInventoryData warloadPenguin))
        {
            warloadPenguin.RemoveStack(count);
            //if (warloadPenguin.stackSize <= count)
            //{
            //    generalInven.Remove(warloadPenguin);
            //    generalDictionary.Remove(penguin);
            //}
            //else
            //{
            //    warloadPenguin.RemoveStack(count);
            //}
        }
        else if (soliderDictionary.TryGetValue(penguin, out LegionInventoryData soliderPenguin))
        {
            soliderPenguin.RemoveStack(count);
            //if (soliderPenguin.stackSize <= count)
            //{
            //    soliderInven.Remove(soliderPenguin);
            //    soliderDictionary.Remove(penguin);
            //}
            //else
            //{
            //    soliderPenguin.RemoveStack(count);
            //}
        }
        UpdateSlotUI();
    }

    public void AddToLegion(PenguinStat penguin, int legionNumber)
    {

        if (LegionList[legionNumber].legionDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {//���� ���ܿ� �� �����Ͱ� �ִٸ�
            legionInven.AddStack(); //����
        }
        else//���ٸ�
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            LegionList[legionNumber].legionInven.Add(newInven);//���� �κ��� ������ �߰�
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }
    }


    public void RemoveLegion(PenguinStat penguin, int i, int count = 1)
    {
        LegionList[i].legionDictionary.TryGetValue(penguin, out LegionInventoryData legion);

        if (legion.stackSize <= count) //�����κ����� ������ ����
        {
            LegionList[i].legionInven.Remove(legion);
            LegionList[i].legionDictionary.Remove(penguin);
        }
        else //���� �κ����� ����
        {
            legion.RemoveStack(count);
        }

        AddPenguin(penguin); //���ܿ��� ���� ����� ��� �κ��� �߰�

        UpdateSlotUI();
    }
}
