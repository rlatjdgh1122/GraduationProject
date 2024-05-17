using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArrangementManager : Singleton<ArrangementManager>
{
    private Transform SpawnPoint => GameManager.Instance.TentTrm;

    [Header("배치 거리"), Range(0.5f, 3f)]
    public float distance = 1;
    private int width = 5;
    private int length = 7;

    private List<Vector3> seatPosList = new();
    private List<EntityInfoDataSO> prevSaveDataList = new();
    private List<EntityInfoDataSO> addDataList = new();
    private List<EntityInfoDataSO> removeDataList = new();
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
    public void ApplySaveData(List<EntityInfoDataSO> dataList)
    {
        //이전 데이터와 현재 데이터를 비교
        CompareDataList(dataList);

        //사라진 정보에 따라 제거
        foreach (var item in removeDataList)
        {
            var legionName = item.LegionName;

            //스폰할때 만들어진 애만 가능
            var penguin = PenguinManager.Instance.GetPenguinByInfoData(item);
            RemovePenguin(legionName, penguin);
        }

        //추가된 정보에 따라 생성
        foreach (var item in addDataList)
        {
            SpawnPenguin(item);
        }

        PenguinManager.Instance.ApplySaveData(addDataList, removeDataList);
    }

    /// <summary>
    /// 이전 저장데이터랑 현재 데이터를 비교해서
    /// 이전 데이터에서 없던게 있다면 addDataList에 추가
    /// 이전 데이터에서 있던게 없다면 removeDataList 추가
    /// </summary>
    /// <param name="dataList"></param>
    private void CompareDataList(List<EntityInfoDataSO> dataList)
    {
        if (addDataList.Count > 0) addDataList.Clear();
        if (removeDataList.Count > 0) removeDataList.Clear();

        var highDataList = prevSaveDataList.Count >= dataList.Count ? prevSaveDataList : dataList;
        var lowDataList = prevSaveDataList.Count >= dataList.Count ? dataList : prevSaveDataList;

        bool isCountEqual = prevSaveDataList.Count == dataList.Count;
        bool isDataListIncreased = prevSaveDataList.Equals(highDataList);


        if (isCountEqual) //만약 이전 데이터와 현재 데이터의 개수가 같다면
        {
            //이런 느낌
            //1 1 2->이전
            //1 1 3->현재
            //remove-> 2
            //add-> 3
            //아 근데 이러면 3이 겹쳐서 안됨 새로만들어주고 넣어줘야할듯(Instantiate)

            for (int i = 0; i < dataList.Count; ++i)
            {
                //데이터가 서로 다르다면
                if (!prevSaveDataList[i].Equals(dataList[i]))
                {
                    //근데 펭귄 타입이 같으면 이미 있으니 안함
                    if (prevSaveDataList[i].PenguinType.Equals(dataList[i].PenguinType)) return;

                    removeDataList.Add(prevSaveDataList[i]);
                    addDataList.Add(dataList[i]);
                }
            }

        }//end if

        else
        {
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
                    if (isDataListIncreased) //이전 데이터가 더 많다면
                        removeDataList.Add(data1);
                    else
                        addDataList.Add(data1);
                }
            }
        }//end else

        //초기화
        prevSaveDataList = dataList.ToList();
    }

    private void SpawnPenguin(EntityInfoDataSO data)
    {
        var _slotIdx = data.SlotIdx;
        var _penguinType = data.PenguinType;

        Penguin spawnPenguin = PenguinManager.Instance.SpawnSoldier(_penguinType, SpawnPoint.position, seatPosList[_slotIdx]);

        PenguinManager.Instance.AddSoliderPenguin(spawnPenguin);
        PenguinManager.Instance.AddInfoDataMapping(data, spawnPenguin);
    }

    private void RemovePenguin(string _legionName, Penguin penguin)
    {
        PenguinManager.Instance.RemoveSoliderPenguin(penguin);
        ArmyManager.Instance.RemovePenguin(_legionName, penguin);
    }
}
