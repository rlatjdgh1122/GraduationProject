using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class LegionInventory : LegionUI
{
    private Dictionary<int, LegionInventoryData> _currentDictionary = new();

    private List<LegionInventoryData> _currentLegionList = new();
    private List<LegionInventoryData> _currentRemovePenguinList = new();
    private List<LegionInventoryData> _savedLegionList = new();

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
            slotList[list.IndexNumber].EnterSlot(list.InfoData); //�� ��ġ�� ������ ����
            _currentLegionList.Add(list); //���� ���ܿ� �־���

            if (!_currentDictionary.ContainsKey(list.IndexNumber))
            {
                _currentDictionary.Add(list.IndexNumber, list);
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
        if (_currentDictionary.ContainsKey(idx))
        {
            return;
        }

        if (data.PenguinType == PenguinTypeEnum.Basic && // 1�� ����Ʈ
            TutorialManager.Instance.CurTutoQuestIdx == 0)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }

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
    public void DeadPenguin(LegionInventoryData data)
    {
        foreach(var saveData in _savedLegionList.ToList())
        {
            if(saveData.LegionName == data.LegionName && saveData.IndexNumber == data.IndexNumber)
            {
                _savedLegionList.Remove(saveData);
                slotList[saveData.IndexNumber].ExitSlot(null);
                SaveLegion();
            }
        }
    }

    public void DamagePenguin(LegionInventoryData data, float curHP)
    {
        foreach (var saveData in _savedLegionList.ToList())
        {
            if (saveData.LegionName == data.LegionName && saveData.IndexNumber == data.IndexNumber)
            {
                saveData.HPPercent(curHP);

                slotList[saveData.IndexNumber].HpValue(saveData.CurrentHPPercent);
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

            if(curData.CurrentHPPercent == 1)
                legion.AddPenguin(curData.InfoData);
            else
            {

                return;
            }

            currentRemovePenguinCnt++;
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
        if(legion.LegionList()[legionNumber].MaxCount <= currentPenguinCnt - currentRemovePenguinCnt)
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
}