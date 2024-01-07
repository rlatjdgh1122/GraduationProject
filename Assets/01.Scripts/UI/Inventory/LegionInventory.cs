using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Legion
{
    public bool generalLimit;
    public List<LegionInventoryData> legionInven;
    public Dictionary<PenguinUIDataSO, LegionInventoryData> legionDictionary = new();
}

public class LegionInventory : Singleton<LegionInventory>
{
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> generalDictionary = new();

    //병사 인벤토리
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> soliderDictionary = new();

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

    public void AddPenguin(PenguinUIDataSO type)
    {
        if (type.JobType == PenguinJobType.General)
        {
            AddToWarLoad(type);
        }
        else if (type.JobType == PenguinJobType.Solider)
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

    public void AddToWarLoad(PenguinUIDataSO penguin)
    {
        if (generalDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {
            legionInven.AddStack();
        }
        else
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            generalInven.Add(newInven);
            generalDictionary.Add(penguin, newInven);
        }
    }

    public void AddToSolider(PenguinUIDataSO penguin)
    {
        if (soliderDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {
            legionInven.AddStack();
        }
        else
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            soliderInven.Add(newInven);
            soliderDictionary.Add(penguin, newInven);
        }
    }

    public void AddToLegion(PenguinUIDataSO penguin, int legionNumber)
    {

        if (LegionList[legionNumber].legionDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {
            legionInven.AddStack();
        }
        else
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            LegionList[legionNumber].legionInven.Add(newInven);
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }

        for (int i = 0; i < LegionList.Count; i++)
        {
            LegionList[i].generalLimit = HasGeneralInLegion(i);
        }
    }

    public bool HasGeneralInLegion(int i)
    {
        foreach (var legion in LegionList[i].legionInven)
        {
            if (legion.penguinData.JobType == PenguinJobType.General)
            {
                return true;
            }
        }

        return false;
    }

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

    public void RemoveLegion(PenguinUIDataSO penguin, int i, int count = 1)
    {
        LegionList[i].legionDictionary.TryGetValue(penguin, out LegionInventoryData legion);

        if (legion.stackSize <= count)
        {
            LegionList[i].legionInven.Remove(legion);
            LegionList[i].legionDictionary.Remove(penguin);
        }
        else
        {
            legion.RemoveStack(count);
        }

        AddPenguin(penguin);

        UpdateSlotUI();
    }
}
