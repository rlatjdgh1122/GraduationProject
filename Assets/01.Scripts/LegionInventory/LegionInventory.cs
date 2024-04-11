using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LegionInventory : LegionUI
{
    private Dictionary<int, LegionInventoryData> _currentDictionary = new();

    private List<LegionInventoryData> _currentLegionList = new();
    private List<LegionInventoryData> _currentRemovePenguinList = new();
    public List<LegionInventoryData> _savedLegionList = new();

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
            Debug.Log(list);
            slotList[list.SlotIdx].EnterSlot(list.InfoData); //�� ��ġ�� ������ ����
            _currentLegionList.Add(list); //���� ���ܿ� �־���

            if (!_currentDictionary.ContainsKey(list.SlotIdx))
            {
                _currentDictionary.Add(list.SlotIdx, list);
            }
            else break;

            saveCnt++;
            CheckType(list.InfoData);
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

        LegionInventoryData legionData
            = new LegionInventoryData(data, legion.LegionList()[legion.CurrentLegion].Name, idx);

        legionData.HPPercent(1);

        slotList[idx].HpValue(legionData.CurrentHPPercent);

        _currentLegionList.Add(legionData);
        _currentDictionary.Add(idx, legionData);

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
        foreach (var saveData in _savedLegionList.ToList())
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

    public void DamagePenguin(LegionInventoryData data, float curHP)
    {
        foreach (var saveData in _savedLegionList.ToList())
        {
            if (saveData.LegionName == data.LegionName && saveData.SlotIdx == data.SlotIdx)
            {
                saveData.HPPercent(curHP);

                slotList[saveData.SlotIdx].HpValue(saveData.CurrentHPPercent);
            }
        }
    }

    /// <summary>
    /// ���� ���ܿ��� ��� �����
    /// </summary>
    /// <param name="idx"></param>
    public void RemovePenguinInCurrentLegion(int idx)
    {
        if (_currentDictionary.TryGetValue(idx, out LegionInventoryData curData))
        {
            _currentLegionList.Remove(curData);
            _currentDictionary.Remove(idx);
            _currentRemovePenguinList.Add(curData);

            if (curData.CurrentHPPercent == 1)
                legion.AddPenguin(curData.InfoData);
            else
            {

                return;
            }

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