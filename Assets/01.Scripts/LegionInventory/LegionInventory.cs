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
    /// 저장하지 않기
    /// </summary>
    public void UndoLegion()
    {
        foreach (var data in _currentLegionList) //현재 군단에서
        {
            if (!_savedLegionList.Contains(data)) //저장 군단에 포함되어 있지 않는 데이터(새로 추가한 데이터)라면
            {
                legion.AddPenguin(data.InfoData); //다시 넣어주기
            }
        }
        foreach (var data in _currentRemovePenguinList) //현재 군단에서 삭제된 펭귄 중
        {
            legion.RemovePenguin(data.InfoData); //다시 빼주기
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
                _savedLegionList?.Add(data); //저장 군단에 넣어주기
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
        saveCnt = 0;
        currentPenguinCnt = 0;
        currentRemovePenguinCnt = 0;
        currentGeneral = 0;
    }

    /// <summary>
    /// 군단 바꾸기
    /// </summary>
    /// <param name="name">군단 이름</param>
    public void ChangeLegion(string name)
    {
        ResetLegion(); //리셋

        if (_savedLegionList == null) return;

        foreach (var list in _savedLegionList.Where(list => list.LegionName == name)) //저장 군단에서 바뀔 군단의 이름과 같다면 
        {
            slotList[list.IndexNumber].EnterSlot(list.InfoData); //그 위치의 슬롯을 업뎃
            _currentLegionList.Add(list); //현재 군단에 넣어줘

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
    /// 현재 군단에 추가하기
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="data"></param>
    public void LegionRegistration(int idx, EntityInfoDataSO data)
    {
        if (_currentDictionary.ContainsKey(idx))
        {
            return;
        }

        if (data.PenguinType == PenguinTypeEnum.Basic && // 1번 퀘스트
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
    /// 펭귄이 죽었으면
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
    /// 현재 군단에서 펭귄 지우기
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
    /// 군단이 바뀌면 저장된 펭귄의 군단 이름도 바꿔줌
    /// </summary>
    /// <param name="beforeName">그 전 이름</param>
    /// <param name="afterName">바뀔 이름</param>
    public void ChangeLegionNameInSaveData(string beforeName, string afterName)
    {
        foreach (var list in _savedLegionList.Where(list => list.LegionName == beforeName)) //저장된 군단에서 전 군단 이름의 데이터 찾기
        {
            list.LegionName = afterName; //바뀐 군단 이름으로 저장하기
        }
    }

    /// <summary>
    /// 만약 현재 군단에서 바뀐게 있다면
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