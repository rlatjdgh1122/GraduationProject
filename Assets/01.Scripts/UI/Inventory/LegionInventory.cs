using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Legion
{
    public int price;                               //군단 가격
    public bool Locked;                             //군단이 잠겨있는가
    public Transform LegionPanels;                  //군단 UI 부모
    public int  MaxCount;                           //최대 군단 병사 수
    public int CurrentCount  = 0;      //현재 군단 병사 수
    public bool MaxGereral  { get; set; }           //군단에 장군이 꽉차있는가

    [HideInInspector] public List<LegionInventoryData> LegionInven;
    public Dictionary<PenguinStat, LegionInventoryData> legionDictionary { get; private set; } = new();
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
    [SerializeField] private Transform _generalParent;
    [SerializeField] private Transform _soliderParent;
    [Header("LegionInventoryUI")]
    [SerializeField] private LegionInventoryUI _legionUI;

    private UnitSlotUI[] _generalSlots;
    private UnitSlotUI[] _soliderSlots;

    [Header("PenguinSO")]
    [SerializeField] private PenguinStat[] _generalSO;
    [SerializeField] private PenguinStat[] _soliderSO;

    public override void Awake()
    {
        base.Awake();

        _generalSlots = _generalParent.GetComponentsInChildren<UnitSlotUI>();
        _soliderSlots = _soliderParent.GetComponentsInChildren<UnitSlotUI>();
    }

    private void Start()
    {
        OffSlotByStartScene(_generalSlots, _generalSO);
        OffSlotByStartScene(_soliderSlots, _soliderSO);

        LegionCountInformation(0);
    }

    public void OffSlotByStartScene(UnitSlotUI[] slot, PenguinStat[] so)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i >= so.Length)
            {
                slot[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < so.Length; i++)
        {
            AddPenguin(so[i]);
            RemovePenguin(so[i]);
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
            LegionList[legionNumber].LegionInven.Add(newInven);//군단 인벤에 데이터 추가
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }
    }


    public void RemoveLegion(PenguinStat penguin, int i, int count = 1)
    {
        LegionList[i].legionDictionary.TryGetValue(penguin, out LegionInventoryData legion);

        if (legion.stackSize <= count) //군단인벤에서 완전히 삭제
        {
            LegionList[i].LegionInven.Remove(legion);
            LegionList[i].legionDictionary.Remove(penguin);
        }
        else //군단 인벤에서 빼기
        {
            legion.RemoveStack(count);
        }

        AddPenguin(penguin); //군단에서 빠진 펭귄을 펭귄 인벤에 추가

        UpdateSlotUI();
    }

    public void ChangeLegion(int number)
    {
        for(int i = 0; i < LegionList.Count; i++)
        {
            if(i == number)
            {
                LegionList[i].LegionPanels.gameObject.SetActive(true);
            }
            else
            {
                LegionList[i].LegionPanels.gameObject.SetActive(false);
            }
        }
    }

    public void LegionCountInformation(int i)
    {
        _legionUI.LegionCountInformation(i);
    }

    public void ShowMessage(string message)
    {
        _legionUI.ShowMessage(message);
    }
}
