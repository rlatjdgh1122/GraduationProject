using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class ArrangementManager : Singleton<ArrangementManager>
{
    [SerializeField] private Transform SpawnPoint;

    [Header("배치 거리"), Range(0.5f, 3f)]
    public float distance = 1;
    private int width = 5;
    private int length = 7;

    private List<Vector3> seatPosList = new();
    private List<LegionInventoryData> prevSaveDataList = new();
    private List<LegionInventoryData> addDataList = new();
    private List<LegionInventoryData> removeDataList = new();
    public override void Awake()
    {
        Setting();
    }
    private void Setting()
    {
        int CLNum = length / 2 + (length % 2 == 0 ? 0 : 1); //7 => 4
        int CWNum = width / 2 + (width % 2 == 0 ? 0 : 1); //5 => 3

        for (int i = (CLNum - 1); i > -CLNum; --i)
        {
            for (int j = -(CWNum - 1); j < CWNum; ++j)
            {
                seatPosList.Add(new Vector3(j * distance, 0, i * distance));
            }
        }
    }
    /// <summary>
    /// 저장되어있는 데이터로 실제 펭귄들을 적용
    /// </summary>
    /// <param name="dataList"></param>
    public void ApplySaveData(List<LegionInventoryData> dataList)
    {
        //이전 데이터와 현재 데이터를 비교
        CompareDataList(dataList);

        //추가된 정보에 따라 생성
        foreach (var item in addDataList)
        {
            SpawnPenguin(item);
        }

        //사라진 정보에 따라 제거
        foreach (var item in removeDataList)
        {
            var legionName = item.LegionName;
            var penguin = PenguinManager.Instance.GetPenguinByLegionData(item);

            RemovePenguin(legionName, penguin);
        }

        PenguinManager.Instance.SetOwnerDummyPenguin(info.PenguinType, obj);
    }

    /// <summary>
    /// 이전 저장데이터랑 현재 데이터를 비교해서
    /// 이전 데이터에서 없던게 있다면 addDataList에 추가
    /// 이전 데이터에서 있던게 없다면 removeDataList 추가
    /// </summary>
    /// <param name="dataList"></param>
    private void CompareDataList(List<LegionInventoryData> dataList)
    {
        if (addDataList.Count > 0) addDataList.Clear();
        if (removeDataList.Count > 0) removeDataList.Clear();

        var highDataList = prevSaveDataList.Count >= dataList.Count ? prevSaveDataList : dataList;
        var lowDataList = prevSaveDataList.Count >= dataList.Count ? dataList : prevSaveDataList;

        bool isDataListIncreased = prevSaveDataList.SequenceEqual(highDataList);

        // highDataList를 반복
        foreach (var data1 in highDataList)
        {
            bool found = false;
            // lowDataList를 반복
            foreach (var data2 in lowDataList)
            {
                if (data1.Equals(data2))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                if (isDataListIncreased)
                    addDataList.Add(data1);
                else
                    removeDataList.Add(data1);
            }
        }

        //초기화
        prevSaveDataList = dataList.ToList();
    }

    private void SpawnPenguin(LegionInventoryData data)
    {
        var _legionName = data.LegionName;
        var _slotIdx = data.InfoData.SlotIdx;
        var _jobType = data.InfoData.JobType;
        var _penguinType = data.InfoData.PenguinType;

        Penguin spawnPenguin =
            PenguinManager.Instance.SpawnSoldier(_penguinType, SpawnPoint.position, seatPosList[_slotIdx]);

        PenguinManager.Instance.AddSpawnMapping(data, spawnPenguin);

        if (_jobType == PenguinJobType.Solider)
        {
            ArmyManager.Instance.JoinArmyToSoldier(_legionName, spawnPenguin);
        }

        else if (_jobType == PenguinJobType.General)
        {
            ArmyManager.Instance.JoinArmyToGeneral(_legionName, spawnPenguin as General);
        }
    }

    private void RemovePenguin(string _legionName, Penguin penguin)
    {
        ArmyManager.Instance.Remove(_legionName, penguin);
    }
}
