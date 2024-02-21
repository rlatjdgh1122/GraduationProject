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
    //펭귄 인벤토리

    //장군
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinStat, LegionInventoryData> generalDictionary = new();

    //병사
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinStat, LegionInventoryData> soliderDictionary = new();
    //==========================================

    [Header("LegionInventory")]
    //군단 인벤토리
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
        if(_soliderSlots.Length != _soliderSO.Length) Debug.LogError("병사 슬롯이 너무 많거나 적음");
        if(_generalSlots.Length != _generalSO.Length) Debug.LogError("장군 슬롯이 너무 많거나 적음");

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

    public void AddPenguin(PenguinStat type) //펭귄 추가하는 함수(펭귄 타입으로 분류)
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

    #region 펭귄 인벤에 펭귄 추가

    public void AddToWarLoad(PenguinStat penguin)
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

    public void AddToSolider(PenguinStat penguin)
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


    public void RemoveLegion(PenguinStat penguin, int i, int count = 1)
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
