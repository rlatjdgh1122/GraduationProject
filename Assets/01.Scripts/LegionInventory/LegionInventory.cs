using System.Linq;


public class LegionInventory : LegionUI
{
    /// <summary>
    /// 저장 취소하기
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in currentRemovePenguinList) //군단에서 지웠는데 저장 취소하면
        {

            //인벤에 들어간 펭귄을 빼줘
            legion.RemovePenguin(PenguinManager.Instance.GetDefaultInfoDataByType(data.PenguinType));
        }
        foreach (var data in currentLegionList) //군단에 추가했는데 저장 취소하면
        {
            if (!savedLegionList.Contains(data))
            {
                //인벤에 들어간 펭귄을 더해줘
                legion.AddPenguin(PenguinManager.Instance.GetDefaultInfoDataByType(data.PenguinType));
            }
        }
    }

    /// <summary>
    /// 저장하기
    /// </summary>
    public void SaveLegion()
    {
        foreach (var data in currentLegionList)
        {
            if (!savedLegionList.Contains(data))
            {
                savedLegionList?.Add(data);
            }
        }

        foreach (var data in currentRemovePenguinList)
        {
            if (savedLegionList.Contains(data))
            {
                savedLegionList.Remove(data);
            }
        }

        ArrangementManager.Instance.ApplySaveData(savedLegionList);
        //SignalHub.OnModifyCurArmy?.Invoke();

        ResetLegion();
    }

    private void ResetLegion()
    {
        foreach (var list in slotList)
        {
            list.ExitSlot(null);
        }

        if (currentDictionary.Count > 0)
            currentDictionary.Clear();

        if (currentRemovePenguinList.Count > 0)
            currentRemovePenguinList.Clear();

        if (currentLegionList.Count > 0)
            currentLegionList.Clear();

        currentPenguinCnt = 0;
        currentRemovePenguinCnt = 0;
        currentGeneral = 0;
    }

    public void ChangeLegion(string name)
    {
        ResetLegion();

        if (savedLegionList == null) return;

        foreach (var list in savedLegionList.Where(list => list.LegionName == name))
        {
            slotList[list.SlotIdx].EnterSlot(list);
            currentLegionList.Add(list);

            if (!currentDictionary.ContainsKey(list.SlotIdx))
            {
                currentDictionary.Add(list.SlotIdx, list);

                //var penguin = PenguinManager.Instance.GetPenguinByInfoData(list);

                //float curHp = penguin.HealthCompo.currentHealth ;
                //float maxHp = penguin.HealthCompo.maxHealth;

                //float hpPercent = curHp / maxHp;

                //PenguinTakeDamage(list.SlotIdx, hpPercent);
            }
            else break;

            CheckType(list);
        }

        LegionCountTextSetting();
        ImportHpValue();
    }

    //private void PenguinTakeDamage(int idx, float hp) => slotList[idx].HpValue(hp);

    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if (currentDictionary.ContainsKey(idx))
        {
            return;
        }

        data = Instantiate(data);

        data.LegionName = legion.LegionList[legion.CurrentLegion].Name;
        data.SlotIdx = idx;

        slotList[idx].HpValue(1);

        currentLegionList.Add(data);
        currentDictionary.Add(idx, data);

        int questIdx = TutorialManager.Instance.CurTutoQuestIdx;

        if (questIdx == 0 && data.PenguinType == PenguinTypeEnum.Basic)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }
        if (questIdx == 1 && data.PenguinType == PenguinTypeEnum.Archer)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }

        if (questIdx == 5)
        {
            TutorialManager.Instance.CurTutorialProgressQuest(QuestGoalIdx.First);
        }

        CheckType(data);

        LegionCountTextSetting();
    }

    public void DeadPenguin(string legionName, int slotIdx, bool retire = false)
    {
        var saveList = savedLegionList.ToList();

        foreach (var saveData in saveList)
        {
            if (saveData.LegionName == legionName
                && saveData.SlotIdx == slotIdx)
            {
                savedLegionList.Remove(saveData);

                if (retire) currentLegionList.Remove(saveData);

                slotList[saveData.SlotIdx].ExitSlot(null);

                if (saveData.JobType == PenguinJobType.General) currentGeneral--;
                else currentPenguinCnt--;
            }
        }//end foreach
        
        LegionCountTextSetting();
    }

    public void RemovePenguinInCurrentLegion(int idx)
    {
        if (currentDictionary.TryGetValue(idx, out EntityInfoDataSO curData))
        {
            var penguin = PenguinManager.Instance.GetPenguinByInfoData(curData);

            float hpPercent = 1;

            if (penguin != null)
            {
                float curHp = penguin.HealthCompo.currentHealth;
                float maxHp = penguin.HealthCompo.maxHealth;

                hpPercent = curHp / maxHp;
            }

            if (hpPercent >= 1) //피가 풀피면
            {
                legion.AddPenguin(PenguinManager.Instance.GetDefaultInfoDataByType(curData.PenguinType));

                currentLegionList.Remove(curData);
                currentDictionary.Remove(idx);

                currentRemovePenguinList.Add(curData);

                slotList[idx].ExitSlot(null);

                if (curData.JobType == PenguinJobType.General) currentGeneral--;
                else
                {
                    currentPenguinCnt--;
                    currentRemovePenguinCnt++;
                }
            }
            else
            {
                UIManager.Instance.ShowWarningUI("펭귄의 체력이 닳아있습니다!");

                legion.ShowPenguinSituation(curData, hpPercent);

                return;
            }
        }

        LegionCountTextSetting();
    }

    public void ChangeLegionNameInSaveData(string beforeName, string afterName)
    {
        foreach (var list in savedLegionList.Where(list => list.LegionName == beforeName))
        {
            list.LegionName = afterName;
        }
    }
}