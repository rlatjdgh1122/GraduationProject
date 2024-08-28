using System.Linq;

public class LegionInventory : LegionUI
{
    /// <summary>
    /// 저장 취소하기
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in currentRemovePenguinList)
        {
            legion.RemovePenguin(data);
        }

        currentLegionList.ProcessIfContained(savedLegionList, false, data => legion.AddPenguin(data));
    }

    /// <summary>
    /// 저장하기
    /// </summary>
    public void SaveLegion()
    {
        currentLegionList.ProcessIfContained(savedLegionList, false, data => savedLegionList.Add(data));

        currentRemovePenguinList.ProcessIfContained(savedLegionList, true, data => savedLegionList.Remove(data));

        ResetLegion();
    }

    /// <summary>
    /// 현재 군단 데이터 및 슬롯 초기화
    /// </summary>
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

    /// <summary>
    /// 군단 바꾸기
    /// </summary>
    /// <param name="name">바뀔 군단 이름</param>
    public void ChangeLegion(string name)
    {
        ResetLegion();

        if (savedLegionList == null) return;

        //저장된 군단 데이터 중 군단 이름이 매개변수 string과 같다면
        foreach (var data in savedLegionList.Where(list => list.LegionName == name))
        {
            slotList[data.SlotIdx].EnterSlot(data);
            currentLegionList.Add(data);

            if (!currentDictionary.ContainsKey(data.SlotIdx))
            {
                currentDictionary.Add(data.SlotIdx, data);

                CheckType(data);
            }
        }
    }

    /// <summary>
    /// 군단에 데이터 등록하기
    /// </summary>
    /// <param name="idx">현재 군단의 슬롯 위치</param>
    /// <param name="data">등록할 데이터</param>
    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if (currentDictionary.ContainsKey(idx)) return;

        data.LegionName = legion.LegionList[legion.CurrentLegion].Name;
        data.SlotIdx = idx;

        currentLegionList.Add(data);
        currentDictionary.Add(idx, data);

        CheckType(data);
    }

    /// <summary>
    /// 펭귄을 저장된 군단에서 뺴주기
    /// </summary>
    /// <param name="legionName">죽은 펭귄이 속한 군단 이름</param>
    /// <param name="slotIdx">속한 군단의 슬롯 위치</param>
    /// <param name="retire">유저 스스로 뺀건지 죽어서 빠진건지</param>
    public void RemovePenguin(string legionName, int slotIdx, bool retire = false)
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
        }

        LegionCountTextSetting();
    }

    /// <summary>
    /// 현재 군단에서 데이터 빼기
    /// </summary>
    /// <param name="idx">현재 군단의 슬롯 위치</param>
    public void RemovePenguinInCurrentLegion(int idx)
    {
        if (currentDictionary.TryGetValue(idx, out EntityInfoDataSO data))
        {
            legion.AddPenguin(data);

            currentLegionList.Remove(data);
            currentDictionary.Remove(idx);

            currentRemovePenguinList.Add(data);

            slotList[idx].ExitSlot(null);

            if (data.JobType == PenguinJobType.General) currentGeneral--;
            else
            {
                currentPenguinCnt--;
                currentRemovePenguinCnt++;
            }
        }
    }

    /// <summary>
    /// 군단 이름 바꾸기
    /// </summary>
    /// <param name="beforeName">바꾸기 전의 군단 이름</param>
    /// <param name="afterName">바꾼 후 군단 이름</param>
    public void ChangeLegionNameInSaveData(string beforeName, string afterName)
    {
        foreach (var list in savedLegionList.Where(list => list.LegionName == beforeName))
        {
            list.LegionName = afterName;
        }
    }
}