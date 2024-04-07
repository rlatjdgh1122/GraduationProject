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
    private List<LegionInventoryData> _savedLegionList = new();

    private int _saveCnt = 0;

    /// <summary>
    /// �������� �ʱ�
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in _currentLegionList) //���� ���ܿ���
        {
            if (!_savedLegionList.Contains(data)) //���� ���ܿ� ���ԵǾ� ���� �ʴ� ������(���� �߰��� ������)���
            {
                legion.AddPenguin(data.InfoData); //�ٽ� �־��ֱ�
            }
        }
        foreach (var data in _currentRemovePenguinList) //���� ���ܿ��� ������ ��� ��
        {
            legion.RemovePenguin(data.InfoData); //�ٽ� ���ֱ�
        }
    }

    /// <summary>
    /// �����ϱ�
    /// </summary>
    public void SaveLegion()
    {
        foreach (var data in _currentLegionList) //���� ���ܿ���
        {
            if (!_savedLegionList.Contains(data)) //���� ���ܿ� ���ԵǾ� ���� �ʴ� ������(���� �߰��� ������)���
            {
                _savedLegionList?.Add(data); //���� ���ܿ� �־��ֱ�
            }
        }

        foreach(var data in _currentRemovePenguinList) //���� ������ ��ϰ�
        {
            if(_savedLegionList.Contains(data)) //���� ���ܿ� �ִ� �����Ͱ� ���ٸ�
            {
                _savedLegionList.Remove(data); //���屺�ܿ��� ����������
            }
        }

        ResetLegion(); //����
    }

    private void ResetLegion() //����
    {
        foreach (var list in slotList) //��� ������ �ʱ�ȭ ������
        {
            list.ExitSlot(null);
        }

        _currentDictionary = new();
        _currentRemovePenguinList = new();
        _currentLegionList = new();
        _saveCnt = 0;
    }

    /// <summary>
    /// ���� �ٲٱ�
    /// </summary>
    /// <param name="name">���� �̸�</param>
    public void ChangeLegion(string name)
    {
        ResetLegion(); //����

        if (_savedLegionList == null) return;

        foreach (var list in _savedLegionList.Where(list => list.LegionName == name)) //���� ���ܿ��� �ٲ� ������ �̸��� ���ٸ� 
        {
            slotList[list.IndexNumber].EnterSlot(list.InfoData); //�� ��ġ�� ������ ����
            _currentLegionList.Add(list); //���� ���ܿ� �־���
            _currentDictionary.Add(list.IndexNumber, list);
            _saveCnt++;
        }
    }


    /// <summary>
    /// �ӽ� ���ܿ� �߰��ϱ�
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
            = new LegionInventoryData(data, legion.LegionName(legion.CurrentLegion), idx);

        _currentLegionList.Add(legionData);
        _currentDictionary.Add(idx, legionData);
    }

    /// <summary>
    /// ����� �׾�����
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
    /// �ӽ� ���ܿ��� ��� �����
    /// </summary>
    /// <param name="idx"></param>
    public void Remove(int idx)
    {
        if (_currentDictionary.TryGetValue(idx, out LegionInventoryData curData))
        {
            _currentLegionList.Remove(curData);
            _currentDictionary.Remove(idx);
            _currentRemovePenguinList.Add(curData);

            //���⸦ ��ȣ�ۿ��ϰ� �ٲ����
            legion.AddPenguin(curData.InfoData);
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
        return _currentLegionList.Count != _saveCnt;
    }
}