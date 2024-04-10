using System.Collections.Generic;
using UnityEngine;

public class PenguinManager
{
    #region 싱글톤
    private static PenguinManager _instance;
    public static PenguinManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PenguinManager();

                if (_instance == null)
                {
                    Debug.Log("PenguinManager가 null입니다.");
                }
            }
            return _instance;
        }
    }
    #endregion
    public class DummyPenguinListItem
    {
        //실제 펭귄을 가지고 있는가?
        public bool IsHaveOwner = false;
        public DummyPenguin dummyPenguin;
    }

    //Type으로 펭귄 등록
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();

    #region 펭귄 리스트
    private List<DummyPenguinListItem> _dummyPenguinList = new();
    public List<DummyPenguin> DummyPenguinList = new();
    /// <summary>
    /// 군단에 소속되지안흔 더미 펭귄
    /// </summary>
    public List<DummyPenguin> NotBelongDummyPenguinList = new();
    public List<Penguin> SoldierPenguinList = new();
    public int DummyPenguinCount => NotBelongDummyPenguinList.Count;
    public int SoldierPenguinCount => SoldierPenguinList.Count;
    #endregion


    #region 변수
    private Dictionary<Penguin, DummyPenguin> penguinToDummyDic = new();
    private Dictionary<DummyPenguin, Penguin> dummyToPenguinDic = new();

    private Dictionary<EntityInfoDataSO, DummyPenguin> infoDataToDummyDic = new();

    private Dictionary<LegionInventoryData, Penguin> legionDataToPenguinDic = new();
    private Dictionary<Penguin, LegionInventoryData> penguinToLegionDataDic = new();
    #endregion

    //군단 매니저에서 등록
    public void Setting(SoldierListSO soldierTypeListSO)
    {
        foreach (var solider in soldierTypeListSO.soldierTypes)
        {
            var type = solider.type;
            soldierTypeDictionary.Add(type, solider.obj);
        }
    }
    public void AddDummyPenguin(DummyPenguin obj)
    {
        _dummyPenguinList.Add(new DummyPenguinListItem
        {
            IsHaveOwner = false,
            dummyPenguin = obj
        });
    }

    public void RemoveDummyPenguin(DummyPenguin obj)
    {

    }
    public void AddSoliderPenguin(Penguin obj)
    {
        SoldierPenguinList.Add(obj);
    }
    public void AddSpawnMapping(LegionInventoryData data, Penguin penguin)
    {
        legionDataToPenguinDic.Add(data, penguin);
        penguinToLegionDataDic.Add(penguin, data);
    }


    /// <summary>
    /// 팽귄 생성하는 함수
    /// </summary>
    /// <typeparam name="T"> Penguin 상속받은 애들</typeparam>
    /// <param name="SpawnPoint"> 스폰 위치</param>
    /// <param name="seatPos"> 배치 위치 *되도록이면 사용하지 말것*</param>
    /// <returns> 니가 만든 펭귄</returns>
    public Penguin SpawnSoldier(PenguinTypeEnum type, Vector3 SpawnPoint, Vector3 seatPos = default)
    {
        Penguin obj;
        var prefab = GetPenguinByType(type);

        obj = PoolManager.Instance.Pop(prefab.name) as Penguin;
        obj.gameObject.SetActive(false);
        obj.transform.position = SpawnPoint;
        obj.SeatPos = seatPos;
        return obj;
    }
    private Penguin GetPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeDictionary.TryGetValue(type, out var value)) return value;

        Debug.Log("해당하는 타입의 오브젝트는 없습니다. SoldierListSO를 확인해주세요.");
        return null;
    }
    public Penguin GetPenguinByDummyPenguin(DummyPenguin dummyPenguin)
    {
        Penguin resultPenguin = null;
        if (dummyToPenguinDic.TryGetValue(dummyPenguin, out var penguin))
        {
            resultPenguin = penguin;
        }
        else
        {
            Debug.Log($"GetPenguinByDummyPenguin에서 오류");
            Debug.Log($"{dummyPenguin.name}과 등록된 펭귄이 없습니다.");
        }

        return resultPenguin;
    }
    public Penguin GetPenguinByLegionData(LegionInventoryData data)
    {
        Penguin resultPenguin = null;
        if (legionDataToPenguinDic.TryGetValue(data, out var penguin))
        {
            resultPenguin = penguin;
        }
        else
        {
            Debug.Log($"GetPenguinByDummyPenguin에서 오류");
            Debug.Log($"{data}과 등록된 펭귄이 없습니다.");
        }

        return resultPenguin;
    }
    public DummyPenguin GetDummyByPenguin(Penguin penguin)
    {
        DummyPenguin resultDummy = null;
        if (penguinToDummyDic.TryGetValue(penguin, out var dummy))
        {
            resultDummy = dummy;
        }
        else
        {
            Debug.Log("GetDummyByPenguin에서 오류");
            Debug.Log($"{penguin.name}과 등록된 더미 펭귄이 없습니다.");
        }

        return resultDummy;
    }
    public T GetStatByInfoData<T>(EntityInfoDataSO data) where T : BaseStat
    {
        T resultStat = null;

        if (infoDataToDummyDic.TryGetValue(data, out var dummy))
        {
            Penguin penguin = GetPenguinByDummyPenguin(dummy);
            resultStat = penguin.ReturnGenericStat<T>();
        }
        else
        {
            Debug.Log("GetStatByInfoData에서 오류");
            Debug.Log("UI정보와 등록된 더미 펭귄이 없습니다.");
        }

        return resultStat;
    }
    public (EntityInfoDataSO, string, int) GetLegionDataByPenguin(Penguin penguin)
    {
        var dummy = GetDummyByPenguin(penguin);
        var UIData = dummy.PenguinUIInfo;

        return (UIData, UIData.LegionName, UIData.SlotIdx);
    }

    public void SetOwnerDummyPenguin(PenguinTypeEnum type, Penguin penguin)
    {
        //여기서 소속된 더미펭귄 리스트랑 소속되지않은 애들 나누기

        foreach (var info in _dummyPenguinList)
        {
            var PenguinType = info.dummyPenguin.PenguinUIInfo.PenguinType;
            var dummyPenguin = info.dummyPenguin;

            //오너를 가지고 있지 않다면
            if (!info.IsHaveOwner)
            {
                //펭귄 타입이 같다면
                if (PenguinType == type)
                {
                    dummyPenguin.SetOwner(penguin);
                    info.IsHaveOwner = true;

                    //펭귄이랑 더미펭귄이랑 연결
                    penguinToDummyDic.Add(penguin, dummyPenguin);
                    DummyToPenguinDic.TryAdd(dummyPenguin, penguin);
                    break;
                }
            }

        }

    }
}


