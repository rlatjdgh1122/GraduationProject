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
    /// 저장 취소하기
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in _currentRemovePenguinList) //군단에서 지웠는데 저장 취소하면
        {
            //인벤에 들어간 펭귄을 빼줘
            legion.RemovePenguin(PenguinManager.Instance.GetNotCloneInfoDataByInfoDataInPenguin(data));
        }
        foreach (var data in _currentLegionList) //군단에 추가했는데 저장 취소하면
        {
            if (!_savedLegionList.Contains(data))
            {
                //인벤에 들어간 펭귄을 더해줘
                legion.AddPenguin(PenguinManager.Instance.GetNotCloneInfoDataByInfoDataInPenguin(data));
            }
        }
    }

    /// <summary>
    /// 저장하기
    /// </summary>
    public void SaveLegion()
    {
        foreach (var data in _currentLegionList)
        {
            if (!_savedLegionList.Contains(data))
            {
                _savedLegionList?.Add(data);
            }
        }

        foreach (var data in _currentRemovePenguinList)
        {
            if (_savedLegionList.Contains(data))
            {
                _savedLegionList.Remove(data);
            }
        }
        ArrangementManager.Instance.ApplySaveData(_savedLegionList);
        ResetLegion();
    }

    private void ResetLegion()
    {
        foreach (var list in slotList)
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

    public void ChangeLegion(string name)
    {
        ResetLegion();

        if (_savedLegionList == null) return;

        foreach (var list in _savedLegionList.Where(list => list.LegionName == name))
        {
            slotList[list.SlotIdx].EnterSlot(list);
            _currentLegionList.Add(list);

            if (!_currentDictionary.ContainsKey(list.SlotIdx))
            {
                _currentDictionary.Add(list.SlotIdx, list);

                var penguin = PenguinManager.Instance.GetPenguinByInfoData(list);

                float curHp = penguin.HealthCompo.currentHealth;
                float maxHp = penguin.HealthCompo.maxHealth;

                float hpPercent = curHp / maxHp;

                PenguinTakeDamage(list.SlotIdx, hpPercent);
            }
            else break;

            saveCnt++;
            CheckType(list);
        }

        LegionCountTextSetting();
    }

    private void PenguinTakeDamage(int idx, float hp) => slotList[idx].HpValue(hp);

    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if (_currentDictionary.ContainsKey(idx))
        {
            return;
        }

        data = Instantiate(data);

        data.LegionName = legion.LegionList()[legion.CurrentLegion].Name;
        data.SlotIdx = idx;

        int questIdx = TutorialManager.Instance.CurTutoQuestIdx;
        
        if (data.PenguinType == PenguinTypeEnum.Basic && questIdx == 0)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }
        if (data.PenguinType == PenguinTypeEnum.Archer && questIdx == 2)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }
        if (data.PenguinType == PenguinTypeEnum.Shield && questIdx == 6)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }
        if (data.PenguinType == PenguinTypeEnum.Mop && questIdx == 6)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.Second);
        }

        slotList[idx].HpValue(1);

        _currentLegionList.Add(data);
        _currentDictionary.Add(idx, data);

        CheckType(data);

        LegionCountTextSetting();
    }

    public void DeadPenguin(string legionName, int slotIdx)
    {
        var saveList = _savedLegionList.ToList();

        foreach (var saveData in saveList)
        {
            if (saveData.LegionName == legionName
                && saveData.SlotIdx == slotIdx)
            {
                _savedLegionList.Remove(saveData);
                _currentLegionList.Remove(saveData);
                slotList[saveData.SlotIdx].ExitSlot(null);
            }
        }
    }

    public void RemovePenguinInCurrentLegion(int idx)
    {
        if (_currentDictionary.TryGetValue(idx, out EntityInfoDataSO curData))
        {
            var penguin = PenguinManager.Instance.GetPenguinByInfoData(curData);

            float curHp = penguin.HealthCompo.currentHealth;
            float maxHp = penguin.HealthCompo.maxHealth;

            float hpPercent = curHp / maxHp;

            if (hpPercent >= 1) //피가 풀피면
            {
                legion.AddPenguin(PenguinManager.Instance.GetNotCloneInfoDataByPenguin(penguin));

                _currentLegionList.Remove(curData);
                _currentDictionary.Remove(idx);
                _currentRemovePenguinList.Add(curData);

                slotList[idx].ExitSlot(null);

                currentRemovePenguinCnt++;
                currentPenguinCnt--;
            }
            else
            {
                legion.ShowPenguinSituation(curData, hpPercent);
                return;
            }
        }

        LegionCountTextSetting();
    }

    public void ChangeLegionNameInSaveData(string beforeName, string afterName)
    {
        foreach (var list in _savedLegionList.Where(list => list.LegionName == beforeName))
        {
            list.LegionName = afterName;
        }
    }
}