using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;


public class LegionInventory : LegionUI
{
    private Dictionary<int, EntityInfoDataSO> _currentDictionary = new();

    private List<EntityInfoDataSO> _currentLegionList = new();
    private List<EntityInfoDataSO> _currentRemovePenguinList = new();
    public List<EntityInfoDataSO> _savedLegionList = new();

    /// <summary>
    /// �������� �ʱ�
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in _currentLegionList) //���� ���ܿ���
        {
            if (!_savedLegionList.Contains(data)) //���� ���ܿ� ���ԵǾ� ���� �ʴ� ������(���� �߰��� ������)���
            {
                legion.AddPenguin(data); //�ٽ� �־��ֱ�
            }
        }
        foreach (var data in _currentRemovePenguinList) //���� ���ܿ��� ������ ��� ��
        {
            legion.RemovePenguin(data); //�ٽ� ���ֱ�
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

        foreach (var data in _currentRemovePenguinList) //���� ������ ��ϰ�
        {
            if (_savedLegionList.Contains(data)) //���� ���ܿ� �ִ� �����Ͱ� ���ٸ�
            {
                _savedLegionList.Remove(data); //���屺�ܿ��� ����������
            }
        }
        ArrangementManager.Instance.ApplySaveData(_savedLegionList);
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
        saveCnt = 0;
        currentPenguinCnt = 0;
        currentRemovePenguinCnt = 0;
        currentGeneral = 0;
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
            slotList[list.SlotIdx].EnterSlot(list); //�� ��ġ�� ������ ����
            _currentLegionList.Add(list); //���� ���ܿ� �־���

            if (!_currentDictionary.ContainsKey(list.SlotIdx))
            {
                _currentDictionary.Add(list.SlotIdx, list);
            }
            else break;

            saveCnt++;
            CheckType(list);
        }

        LegionCountTextSetting();
    }


    /// <summary>
    /// ���� ���ܿ� �߰��ϱ�
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="data"></param>
    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if (_currentDictionary.TryGetValue(idx, out _))
        {
            return;
        }

        data = Instantiate(data);

        data.LegionName = legion.LegionList()[legion.CurrentLegion].Name;
        data.SlotIdx = idx;

        //���߿� �� �޾ƿ���

        //slotList[idx].HpValue(��������.CurrentHPPercent);

        _currentLegionList.Add(data);
        _currentDictionary.Add(idx, data);

        CheckType(data);
        LegionCountTextSetting();
    }


    /// <summary>
    /// ����� �׾�����
    /// </summary>
    /// <param name="data"></param>
    public void DeadPenguin(EntityInfoDataSO so, string legionName, int legionPosition)
    {
        /*foreach (var saveData in _savedLegionList.ToList())
        {
            if (saveData.LegionName == legionName && saveData.SlotIdx == data.SlotIdx)
            {
                _savedLegionList.Remove(saveData);
                slotList[saveData.SlotIdx].ExitSlot(null);
                SaveLegion();
            }
        }*/
    }
    public void DeadPenguin(string legionName, int slotIdx)
    {
        var saveList = _savedLegionList.ToList();

        foreach (var saveData in saveList)
        {
            Debug.Log("B : " + saveData.LegionName + ", " + saveData.SlotIdx);
            if (saveData.LegionName == legionName
                && saveData.SlotIdx == slotIdx)
            {
                Debug.Log("����");
                _savedLegionList.Remove(saveData);
                slotList[saveData.SlotIdx].ExitSlot(null);
                SaveLegion();
            }
        }
    }

    /// <summary>
    /// ���� ���ܿ��� ��� �����
    /// </summary>
    /// <param name="idx"></param>
    public void RemovePenguinInCurrentLegion(int idx)
    {
        if (_currentDictionary.TryGetValue(idx, out EntityInfoDataSO curData))
        {
            _currentLegionList.Remove(curData);
            _currentDictionary.Remove(idx);
            _currentRemovePenguinList.Add(curData);

            

            //if (curData.CurrentHPPercent == 1)
            //    legion.AddPenguin(curData.InfoData);
            //else
            //{
            //    UIManager.Instance.ShowWarningUI("����� ü���� ����ֽ��ϴ�!");
            //    legion.ShowPenguinSituation(curData.InfoData, curData.CurrentHPPercent, (curData.InfoData as PenguinInfoDataSO).Price);
            //    return;
            //}

            currentRemovePenguinCnt++;
            currentPenguinCnt--;
        }
    }

    /// <summary>
    /// ������ �ٲ�� ����� ����� ���� �̸��� �ٲ���
    /// </summary>
    /// <param name="beforeName">�� �� �̸�</param>
    /// <param name="afterName">�ٲ� �̸�</param>
    public void ChangeLegionNameInSaveData(string beforeName, string afterName)
    {
        foreach (var list in _savedLegionList.Where(list => list.LegionName == beforeName)) //����� ���ܿ��� �� ���� �̸��� ������ ã��
        {
            list.LegionName = afterName; //�ٲ� ���� �̸����� �����ϱ�
        }
    }

    /// <summary>
    /// ���� ���� ���ܿ��� �ٲ�� �ִٸ�
    /// </summary>
    /// <returns></returns>
    public bool ChangedInCurrentLegion()
    {
        return currentPenguinCnt + currentGeneral - currentRemovePenguinCnt != saveCnt;
    }

    public bool ExcedLimitOfLegion(int legionNumber)
    {
        if (legion.LegionList()[legionNumber].MaxCount <= currentPenguinCnt - currentRemovePenguinCnt)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool LimitOfGeneral()
    {
        return currentGeneral > 0 ? true : false;
    }

    private void OnDestroy()
    {
        ResetLegion();

        if (_savedLegionList.Count > 0)
            _savedLegionList.Clear();
    }
}