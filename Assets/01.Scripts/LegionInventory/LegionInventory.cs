using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LegionInventory : InitLegionInventory
{
    private Dictionary<int, LegionInventoryData> _currentDictionary = new(); 

    private List<LegionInventoryData> _currentLegionList = new();
    private List<LegionInventoryData> _currentRemovePenguinList = new();
    private List<LegionInventoryData> _savedLegionList;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveLegion();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeLegion(LegionInventoryManager.Instance.CurrentLegion);
        }
    }

    /// <summary>
    /// 저장하지 않기
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in _currentLegionList) //현재 군단에서
        {
            if (!_savedLegionList.Contains(data)) //저장 군단에 포함되어 있지 않는 데이터(새로 추가한 데이터)라면
            {
                LegionInventoryManager.Instance.AddPenguin(data.InfoData); //다시 넣어주기
            }
        }
    }

    /// <summary>
    /// 저장하기
    /// </summary>
    public void SaveLegion()
    {
        foreach (var data in _currentLegionList) //현재 군단에서
        {
            if (!_savedLegionList.Contains(data)) //저장 군단에 포함되어 있지 않는 데이터(새로 추가한 데이터)라면
            {
                _savedLegionList.Add(data); //저장 군단에 넣어주기
            }
        }

        foreach(var data in _currentRemovePenguinList) //현재 삭제된 펭귄과
        {
            if(_savedLegionList.Contains(data)) //저장 군단에 있는 데이터가 같다면
            {
                _savedLegionList.Remove(data); //저장군단에서 삭제시켜줌
            }
        }

        ResetLegion(); //리셋
    }

    private void ResetLegion() //리셋
    {
        foreach (var list in slotList) //모든 슬롯을 초기화 시켜줘
        {
            list.ExitSlot(null);
        }

        _currentDictionary = new();
        _currentRemovePenguinList = new();
        _currentLegionList = new();
    }

    /// <summary>
    /// 군단 바꾸기
    /// </summary>
    /// <param name="name">군단 이름</param>
    public void ChangeLegion(string name)
    {
        ResetLegion(); //리셋

        foreach (var list in _savedLegionList.Where(list => list.LegionName == name)) //저장 군단에서 바뀔 군단의 이름과 같다면 
        {
            slotList[list.IndexNumber].EnterSlot(list.InfoData); //그 위치의 슬롯을 업뎃
            _currentLegionList.Add(list); //현재 군단에 넣어줘
            _currentDictionary.Add(list.IndexNumber, list);
        }
    }


    /// <summary>
    /// 임시 군단에 추가하기
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="data"></param>
    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if(_currentDictionary.TryGetValue(idx, out _))
        {
            return;
        }

        LegionInventoryData legionData 
            = new LegionInventoryData(data, LegionInventoryManager.Instance.CurrentLegion, idx);

        _currentLegionList.Add(legionData);
        _currentDictionary.Add(idx, legionData);
    }

    /// <summary>
    /// 펭귄이 죽었으면
    /// </summary>
    /// <param name="data"></param>
    public void DeadPenguin(LegionInventoryData data)
    {
        var savedData = _savedLegionList.FirstOrDefault(saveData => saveData == data);
        if (savedData != null)
        {
            Remove(savedData.IndexNumber);
            SaveLegion();
        }
    }

    /// <summary>
    /// 임시 군단에서 펭귄 지우기
    /// </summary>
    /// <param name="idx"></param>
    public void Remove(int idx)
    {
        if (_currentDictionary.TryGetValue(idx, out LegionInventoryData curData))
        {
            _currentLegionList.Remove(curData);
            _currentDictionary.Remove(idx);
            _currentRemovePenguinList.Add(curData);

            //여기를 상호작용하게 바꿔야함
            LegionInventoryManager.Instance.AddPenguin(curData.InfoData);
        }
    }


    public void ChangeName(string beforeName, string afterName)
    {
        foreach (var list in _savedLegionList.Where(list => list.LegionName == beforeName))
        {
            list.LegionName = afterName;
        }
    }

    public bool ChangedInCurrentLegion()
    {
        return _currentLegionList != null 
            || _currentRemovePenguinList != null;
    }
}