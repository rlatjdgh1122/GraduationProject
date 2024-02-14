using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Legion
{
    public List<LegionInventoryData> legionInven;
    public Dictionary<PenguinUIDataSO, LegionInventoryData> legionDictionary = new();
}

public class LegionInventory : Singleton<LegionInventory>
{
    //==========================================
    //��� �κ��丮

    //�屺
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> generalDictionary = new();

    //����
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> soliderDictionary = new();
    //==========================================


    //���� �κ��丮
    public List<Legion> LegionList;

    [Header("Inventory UI")]
    [SerializeField] private Transform _soliderParent;
    [SerializeField] private Transform _warloadParent;

    private UnitSlotUI[] _soliderSlots;
    private UnitSlotUI[] _warloadSlots;

    public override void Awake()
    {
        base.Awake();

        _soliderSlots = _soliderParent.GetComponentsInChildren<UnitSlotUI>();
        _warloadSlots = _warloadParent.GetComponentsInChildren<UnitSlotUI>();
    }

    public void AddPenguin(PenguinUIDataSO type) //��� �߰��ϴ� �Լ�(��� Ÿ������ �з�)
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
        for (int i = 0; i < _warloadSlots.Length; i++)
        {
            _warloadSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < generalInven.Count; ++i)
        {
            _warloadSlots[i].UpdateSlot(generalInven[i]);
        }
        for (int i = 0; i < soliderInven.Count; ++i)
        {
            _soliderSlots[i].UpdateSlot(soliderInven[i]);
        }

    }

    #region ��� �κ��� ��� �߰�

    public void AddToWarLoad(PenguinUIDataSO penguin)
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

    public void AddToSolider(PenguinUIDataSO penguin)
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

    public void RemovePenguin(PenguinUIDataSO penguin, int count = 1)
    {
        if (generalDictionary.TryGetValue(penguin, out LegionInventoryData warloadPenguin))
        {
            if (warloadPenguin.stackSize <= count)
            {
                generalInven.Remove(warloadPenguin);
                generalDictionary.Remove(penguin);
            }
            else
            {
                warloadPenguin.RemoveStack(count);
            }
        }
        else if (soliderDictionary.TryGetValue(penguin, out LegionInventoryData soliderPenguin))
        {
            if (soliderPenguin.stackSize <= count)
            {
                soliderInven.Remove(soliderPenguin);
                soliderDictionary.Remove(penguin);
            }
            else
            {
                soliderPenguin.RemoveStack(count);
            }
        }
        UpdateSlotUI();
    }

    public void AddToLegion(PenguinUIDataSO penguin, int legionNumber)
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


    public void RemoveLegion(PenguinUIDataSO penguin, int i, int count = 1)
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
