using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Legion
{
    public List<LegionInventoryData> legionInvent;
    public Dictionary<PenguinUIDataSO, LegionInventoryData> legionDictionary = new();
}

public class LegionInventory : Singleton<LegionInventory>
{
    public List<LegionInventoryData> warLoadInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> warLoadDictionary = new();

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
        if (type.JobType == PenguinJobType.WarLoard)
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

        for (int i = 0; i < warLoadInven.Count; ++i)
        {
            _warloadSlots[i].UpdateSlot(warLoadInven[i]);
        }
        for (int i = 0; i < soliderInven.Count; ++i)
        {
            _soliderSlots[i].UpdateSlot(soliderInven[i]);
        }
    }

    public void AddToWarLoad(PenguinUIDataSO penguin)
    {
        if (warLoadDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {
            legionInven.AddStack();
        }
        else
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            warLoadInven.Add(newInven);
            warLoadDictionary.Add(penguin, newInven);
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
            LegionList[legionNumber].legionInvent.Add(newInven);
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }
    }

    public void RemovePenguin(PenguinUIDataSO penguin, int count = 1)
    {
        if (warLoadDictionary.TryGetValue(penguin, out LegionInventoryData warloadPenguin))
        {
            if (warloadPenguin.stackSize <= count)
            {
                warLoadInven.Remove(warloadPenguin);
                warLoadDictionary.Remove(penguin);
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
            LegionList[i].legionInvent.Remove(legion);
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
