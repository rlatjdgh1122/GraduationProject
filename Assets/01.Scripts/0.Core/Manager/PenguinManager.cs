using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

public class PenguinManager
{
    public class DummyPenguinListItem
    {
        //실제 펭귄을 가지고 있는가?
        public bool IsHaveOwner = false;
        public DummyPenguin dummyPenguin;
    }

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

    //Type으로 클론이 아닌 것들을 저장해줌
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeToDefaultPenguinDic = new();
    private Dictionary<PenguinTypeEnum, BaseStat> soldierTypeToDefaultStatDic = new();
    private Dictionary<PenguinTypeEnum, EntityInfoDataSO> soldierTypeToDefaultInfoDataDic = new();

    #region 변수
    private Dictionary<Penguin, DummyPenguin> penguinToDummyDic = new();
    private Dictionary<DummyPenguin, Penguin> dummyToPenguinDic = new();

    private Dictionary<EntityInfoDataSO, Penguin> infoDataToPenguinDic = new();
    private Dictionary<Penguin, EntityInfoDataSO> penguinToInfoDataDic = new();

    public BaseStat GetCurrentStat = null;
    public EntityInfoDataSO GetCurrentInfoData = null;
    #endregion

    #region 펭귄 리스트
    private List<DummyPenguinListItem> _dummyPenguinList = new();
    public List<DummyPenguin> DummyPenguinList = new();

    /// <summary>
    /// 군단에 소속되지안흔 더미 펭귄
    /// </summary>
    public List<DummyPenguin> BelongDummyPenguinList = new();
    /// <summary>
    /// 군단에 소속된 더미 펭귄
    /// </summary>
    public List<DummyPenguin> NotBelongDummyPenguinList = new();

    public List<Penguin> SoldierPenguinList = new();
    public int DummyPenguinCount => DummyPenguinList.Count;
    public int SoldierPenguinCount => SoldierPenguinList.Count;
    #endregion

    public CameraSystem CameraCompo { get; set; }

    #region  GetComponent
    public void GetComponent_CameraSystem(CameraSystem compo)
    {
        CameraCompo = compo;
    }
    #endregion
    //게임 매니저에서 등록
    public void Setting(SoldierRegisterSO soldierTypeListSO)
    {
        foreach (var Soldier in soldierTypeListSO.soldierTypes)
        {
            var type = Soldier.Type;
            //생성되지않은 펭귄 프리팹을 등록해줌
            soldierTypeToDefaultPenguinDic.Add(type, Soldier.Obj);

            //클론이 되지 않은 스탯을 등록해줌
            soldierTypeToDefaultStatDic.Add(type, Soldier.Stat);
            //클론이 되지 않은 인포정보를 등록해줌
            soldierTypeToDefaultInfoDataDic.Add(type, Soldier.InfoData);
        }
    }

    #region Add Or Remove
    public void AddDummyPenguin(DummyPenguin obj)
    {
        _dummyPenguinList.Add(new DummyPenguinListItem
        {
            IsHaveOwner = false,
            dummyPenguin = obj
        });
        DummyPenguinList.Add(obj);
    }

    /// <summary>
    /// 퇴출할 때 사용
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveDummyPenguin(DummyPenguin obj)
    {
        DummyPenguinList.Remove(obj);
    }
    public void AddSoliderPenguin(Penguin obj)
    {
        SoldierPenguinList.Add(obj);
    }
    /// <summary>
    /// 퇴출할 때 사용
    /// </summary>
    /// <param name="obj"></param>
    public void RemoveSoliderPenguin(Penguin obj)
    {
        SoldierPenguinList.Remove(obj);
    }
    public void AddInfoDataMapping(EntityInfoDataSO data, Penguin penguin)
    {
        EntityInfoDataSO dataType = null;

        if (data.JobType == PenguinJobType.General)
        {
            dataType = data as GeneralInfoDataSO;
        }
        else
        {
            dataType = data as PenguinInfoDataSO;
        }

        infoDataToPenguinDic.Add(dataType, penguin);
        penguinToInfoDataDic.Add(penguin, dataType);
    }

    #endregion

    #region default(NotClone) Return
    public Penguin GetDefaultPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeToDefaultPenguinDic.TryGetValue(type, out var value)) return value;

        Debug.Log("해당하는 타입의 오브젝트는 없습니다. SoldierRegisterSO를 확인해주세요.");
        return null;
    }
    public BaseStat GetDefaultStatByType(PenguinTypeEnum type)
    {
        if (soldierTypeToDefaultStatDic.TryGetValue(type, out var value)) return value;

        Debug.Log("해당하는 타입의 스탯은 없습니다. SoldierRegisterSO를 확인해주세요.");
        return null;
    }

    public EntityInfoDataSO GetDefaultInfoDataByType(PenguinTypeEnum type)
    {
        if (soldierTypeToDefaultInfoDataDic.TryGetValue(type, out var value)) return value;

        Debug.Log("해당하는 타입의 정보는 없습니다. SoldierRegisterSO를 확인해주세요.");
        return null;
    }

    #endregion

    #region DummyPenguin Return
    public DummyPenguin GetDummyByInfoData(EntityInfoDataSO infoData)
    {
        DummyPenguin resultDummy = null;
        if (infoDataToPenguinDic.TryGetValue(infoData, out var dummy))
        {
            resultDummy = GetDummyByPenguin(dummy);
        }

        return resultDummy;
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
    #endregion

    #region Penguin Return
    public Penguin GetPenguinByInfoData(EntityInfoDataSO data)
    {
        Penguin resultPenguin = null;
        if (infoDataToPenguinDic.TryGetValue(data, out var penguin))
        {
            resultPenguin = penguin;
        }

        return resultPenguin;
    }
    public Penguin GetPenguinByDummyPenguin(DummyPenguin dummyPenguin)
    {
        Penguin resultPenguin = null;
        if (dummyToPenguinDic.TryGetValue(dummyPenguin, out var penguin))
        {
            resultPenguin = penguin;
        }

        return resultPenguin;
    }

    #endregion

    #region InfoData Return

    public T GetStatByInfoData<T>(EntityInfoDataSO data) where T : BaseStat
    {
        T resultStat = null;

        if (infoDataToPenguinDic.TryGetValue(data, out var penguin))
        {
            resultStat = penguin.ReturnGenericStat<T>();
            GetCurrentStat = resultStat;
        }
        else
        {
            //군단에 소속되지 않았다면 클론이 아닌 디폴트 스탯을 가져옴
            if (soldierTypeToDefaultStatDic.TryGetValue(data.PenguinType, out var notCloneStat))
                resultStat = notCloneStat as T;
        }

        return resultStat;
    }

    public T GetInfoDataByDummyPenguin<T>(DummyPenguin dummy) where T : EntityInfoDataSO
    {
        T result = null;
        Penguin penguin = GetPenguinByDummyPenguin(dummy);

        if (penguin == null) return result;

        if (penguinToInfoDataDic.TryGetValue(penguin, out var infoDataSO))
        {
            result = (T)infoDataSO;
        }

        return result;
    }
    public EntityInfoDataSO GetInfoDataByPenguin(Penguin penguin)
    {
        EntityInfoDataSO result = null;
        if (penguin == null) return result;

        if (penguinToInfoDataDic.TryGetValue(penguin, out var infoDataSO))
        {

            result = infoDataSO;
        }

        return result;
    }
    #endregion

    #region ApplyData

    public void ApplySaveData(List<EntityInfoDataSO> addDataList, List<EntityInfoDataSO> removeDataList)
{
    foreach (var data in addDataList)
    {
        ApplyDummyPenguin(data);
    }

    foreach (var data in removeDataList)
    {
        ReleaseDummyPenguin(data);
    }
}

//펭귄과 더미펭귄을 맵핑
private void ApplyDummyPenguin(EntityInfoDataSO data)
{
    var dataType = data.PenguinType;
    var penguin = GetPenguinByInfoData(data);

    //지금까지 생성된 더미펭귄들에서
    //오너를 가지고 있지 않은 애들을 골라 오너를 넣어줌
    foreach (var info in _dummyPenguinList)
    {
        var dummyPenguinType = info.dummyPenguin.NotCloneInfo.PenguinType;
        var dummyPenguin = info.dummyPenguin;

        //오너를 가지고 있지 않다면
        if (!info.IsHaveOwner)
        {
            //펭귄 타입이 같다면
            if (dataType == dummyPenguinType)
            {
                info.IsHaveOwner = true;

                //펭귄이랑 더미펭귄이랑 연결
                penguinToDummyDic.Add(penguin, dummyPenguin);
                dummyToPenguinDic.Add(dummyPenguin, penguin);

                break;
            }
        }

    }
    UpdateOwnershipDataList();
}

//펭귄과 더미펭귄을 딕셔너리에서 제외
private void ReleaseDummyPenguin(EntityInfoDataSO data)
{
    var dataType = data.PenguinType;
    var penguin = GetPenguinByInfoData(data);

    //지금까지 생성된 더미펭귄들에서
    //오너를 가지고 있는 더미펭귄들을 골라 오너를 지워주고
    //딕셔너리에서 지워줌

    foreach (var info in _dummyPenguinList)
    {
        var dummyPenguinType = info.dummyPenguin.NotCloneInfo.PenguinType;
        var dummyPenguin = info.dummyPenguin;

        //오너를 가지고 있다면
        if (info.IsHaveOwner)
        {
            //펭귄 타입이 같다면
            if (dataType == dummyPenguinType)
            {
                info.IsHaveOwner = false;

                //펭귄이랑 더미펭귄이랑 연결
                penguinToDummyDic.Remove(penguin);
                dummyToPenguinDic.Remove(dummyPenguin);

                break;
            }
        }

    }
}

private void UpdateOwnershipDataList()
{
    if (BelongDummyPenguinList.Count > 0) BelongDummyPenguinList.Clear();
    if (NotBelongDummyPenguinList.Count > 0) NotBelongDummyPenguinList.Clear();


    foreach (var item in _dummyPenguinList)
    {
        if (item.IsHaveOwner)
        {
            BelongDummyPenguinList.Add(item.dummyPenguin);
        }
        else
        {
            NotBelongDummyPenguinList.Add(item.dummyPenguin);
        }
    }
}
#endregion

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
    var prefab = GetDefaultPenguinByType(type);

    obj = PoolManager.Instance.Pop(prefab.name) as Penguin;
    obj.gameObject.SetActive(false);
    obj.transform.position = SpawnPoint;
    obj.SeatPos = seatPos;
    return obj;
}

/// <summary>
/// 펭귄 정보 보여주는 함수
/// </summary>
/// <typeparam name="T1">InfoData를 넣으세요</typeparam>
/// <typeparam name="T2">Stat을 넣으세요</typeparam>
/// <param name="dummy"></param>
public void ShowInfoUI<T1, T2>(DummyPenguin dummy) where T1 : EntityInfoDataSO where T2 : BaseStat
{
    T1 infoData = GetInfoDataByDummyPenguin<T1>(dummy);

    //군단에 소속되지 않았다면 디폴트 정보를 넘겨줌
    if (infoData == null) infoData = dummy.NotCloneInfo as T1;

    T2 statData = GetStatByInfoData<T2>(infoData);

    GetCurrentInfoData = infoData;
    GetCurrentStat = statData;

    UIManager.Instance.HidePanel("PenguinInfoUI");
    PenguinManager.Instance.CameraCompo.SetCameraTartget(dummy.transform.position, 30f, 5f);

    // 장군 정보와 펭귄 정보는 따로
    //if (stat is PenguinStat && infoData is EntityInfoDataSO)
    UIManager.Instance.ShowPanel("PenguinInfoUI");
    /*else
        UIManager.Instance.ShowPanel("GeneralInfoUI");*/

}
}


