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
    //펭귄 인벤토리

    //장군
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> generalDictionary = new();

    //병사
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinUIDataSO, LegionInventoryData> soliderDictionary = new();
    //==========================================


    //군단 인벤토리
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

    public void AddPenguin(PenguinUIDataSO type) //펭귄 추가하는 함수(펭귄 타입으로 분류)
    {
        if (type.JobType == PenguinJobType.General) //만약 장군이면
        {
            AddToWarLoad(type);
        }
        else if (type.JobType == PenguinJobType.Solider) //만약 병사면
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

    #region 펭귄 인벤에 펭귄 추가

    public void AddToWarLoad(PenguinUIDataSO penguin)
    {
        if (generalDictionary.TryGetValue(penguin, out LegionInventoryData legionInven)) //만약 펭귄 인벤에 있으면
        {
            legionInven.AddStack(); //값 증가
        }
        else //없으면
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            generalInven.Add(newInven); //장군에 데이터 추가
            generalDictionary.Add(penguin, newInven);
        }
    }

    public void AddToSolider(PenguinUIDataSO penguin)
    {
        if (soliderDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))//만약 펭귄 인벤에 있으면
        {
            legionInven.AddStack();//값 증가
        }
        else//없으면
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            soliderInven.Add(newInven);//병사에 데이터 추가
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
        {//만약 군단에 이 데이터가 있다면
            legionInven.AddStack(); //증가
        }
        else//없다면
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            LegionList[legionNumber].legionInven.Add(newInven);//군단 인벤에 데이터 추가
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }
    }


    public void RemoveLegion(PenguinUIDataSO penguin, int i, int count = 1)
    {
        LegionList[i].legionDictionary.TryGetValue(penguin, out LegionInventoryData legion);

        if (legion.stackSize <= count) //군단인벤에서 완전히 삭제
        {
            LegionList[i].legionInven.Remove(legion);
            LegionList[i].legionDictionary.Remove(penguin);
        }
        else //군단 인벤에서 빼기
        {
            legion.RemoveStack(count);
        }

        AddPenguin(penguin); //군단에서 빠진 펭귄을 펭귄 인벤에 추가

        UpdateSlotUI();
    }
}
