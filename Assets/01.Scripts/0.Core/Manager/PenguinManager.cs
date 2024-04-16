using System.Collections.Generic;
using System.Xml.XPath;
using Unity.VisualScripting;
using UnityEngine;

public class PenguinManager
{
    public class DummyPenguinListItem
    {
        //���� ����� ������ �ִ°�?
        public bool IsHaveOwner = false;
        public DummyPenguin dummyPenguin;
    }

    #region �̱���
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
                    Debug.Log("PenguinManager�� null�Դϴ�.");
                }
            }
            return _instance;
        }
    }
    #endregion

    //Type���� Ŭ���� �ƴ� �͵��� ��������
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeToDefaultPenguinDic = new();
    private Dictionary<PenguinTypeEnum, BaseStat> soldierTypeToDefaultStatDic = new();
    private Dictionary<PenguinTypeEnum, EntityInfoDataSO> soldierTypeToDefaultInfoDataDic = new();

    #region ����
    private Dictionary<Penguin, DummyPenguin> penguinToDummyDic = new();
    private Dictionary<DummyPenguin, Penguin> dummyToPenguinDic = new();

    private Dictionary<EntityInfoDataSO, Penguin> infoDataToPenguinDic = new();
    private Dictionary<Penguin, EntityInfoDataSO> penguinToInfoDataDic = new();

    public BaseStat GetCurrentStat = null;
    public EntityInfoDataSO GetCurrentInfoData = null;
    #endregion

    #region ��� ����Ʈ
    private List<DummyPenguinListItem> _dummyPenguinList = new();
    public List<DummyPenguin> DummyPenguinList = new();

    /// <summary>
    /// ���ܿ� �Ҽӵ������� ���� ���
    /// </summary>
    public List<DummyPenguin> BelongDummyPenguinList = new();
    /// <summary>
    /// ���ܿ� �Ҽӵ� ���� ���
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
    //���� �Ŵ������� ���
    public void Setting(SoldierRegisterSO soldierTypeListSO)
    {
        foreach (var Soldier in soldierTypeListSO.soldierTypes)
        {
            var type = Soldier.Type;
            //������������ ��� �������� �������
            soldierTypeToDefaultPenguinDic.Add(type, Soldier.Obj);

            //Ŭ���� ���� ���� ������ �������
            soldierTypeToDefaultStatDic.Add(type, Soldier.Stat);
            //Ŭ���� ���� ���� ���������� �������
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
    /// ������ �� ���
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
    /// ������ �� ���
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

        Debug.Log("�ش��ϴ� Ÿ���� ������Ʈ�� �����ϴ�. SoldierRegisterSO�� Ȯ�����ּ���.");
        return null;
    }
    public BaseStat GetDefaultStatByType(PenguinTypeEnum type)
    {
        if (soldierTypeToDefaultStatDic.TryGetValue(type, out var value)) return value;

        Debug.Log("�ش��ϴ� Ÿ���� ������ �����ϴ�. SoldierRegisterSO�� Ȯ�����ּ���.");
        return null;
    }

    public EntityInfoDataSO GetDefaultInfoDataByType(PenguinTypeEnum type)
    {
        if (soldierTypeToDefaultInfoDataDic.TryGetValue(type, out var value)) return value;

        Debug.Log("�ش��ϴ� Ÿ���� ������ �����ϴ�. SoldierRegisterSO�� Ȯ�����ּ���.");
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
            Debug.Log("GetDummyByPenguin���� ����");
            Debug.Log($"{penguin.name}�� ��ϵ� ���� ����� �����ϴ�.");
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
            //���ܿ� �Ҽӵ��� �ʾҴٸ� Ŭ���� �ƴ� ����Ʈ ������ ������
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

//��ϰ� ��������� ����
private void ApplyDummyPenguin(EntityInfoDataSO data)
{
    var dataType = data.PenguinType;
    var penguin = GetPenguinByInfoData(data);

    //���ݱ��� ������ ������ϵ鿡��
    //���ʸ� ������ ���� ���� �ֵ��� ��� ���ʸ� �־���
    foreach (var info in _dummyPenguinList)
    {
        var dummyPenguinType = info.dummyPenguin.NotCloneInfo.PenguinType;
        var dummyPenguin = info.dummyPenguin;

        //���ʸ� ������ ���� �ʴٸ�
        if (!info.IsHaveOwner)
        {
            //��� Ÿ���� ���ٸ�
            if (dataType == dummyPenguinType)
            {
                info.IsHaveOwner = true;

                //����̶� ��������̶� ����
                penguinToDummyDic.Add(penguin, dummyPenguin);
                dummyToPenguinDic.Add(dummyPenguin, penguin);

                break;
            }
        }

    }
    UpdateOwnershipDataList();
}

//��ϰ� ��������� ��ųʸ����� ����
private void ReleaseDummyPenguin(EntityInfoDataSO data)
{
    var dataType = data.PenguinType;
    var penguin = GetPenguinByInfoData(data);

    //���ݱ��� ������ ������ϵ鿡��
    //���ʸ� ������ �ִ� ������ϵ��� ��� ���ʸ� �����ְ�
    //��ųʸ����� ������

    foreach (var info in _dummyPenguinList)
    {
        var dummyPenguinType = info.dummyPenguin.NotCloneInfo.PenguinType;
        var dummyPenguin = info.dummyPenguin;

        //���ʸ� ������ �ִٸ�
        if (info.IsHaveOwner)
        {
            //��� Ÿ���� ���ٸ�
            if (dataType == dummyPenguinType)
            {
                info.IsHaveOwner = false;

                //����̶� ��������̶� ����
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
/// �ر� �����ϴ� �Լ�
/// </summary>
/// <typeparam name="T"> Penguin ��ӹ��� �ֵ�</typeparam>
/// <param name="SpawnPoint"> ���� ��ġ</param>
/// <param name="seatPos"> ��ġ ��ġ *�ǵ����̸� ������� ����*</param>
/// <returns> �ϰ� ���� ���</returns>
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
/// ��� ���� �����ִ� �Լ�
/// </summary>
/// <typeparam name="T1">InfoData�� ��������</typeparam>
/// <typeparam name="T2">Stat�� ��������</typeparam>
/// <param name="dummy"></param>
public void ShowInfoUI<T1, T2>(DummyPenguin dummy) where T1 : EntityInfoDataSO where T2 : BaseStat
{
    T1 infoData = GetInfoDataByDummyPenguin<T1>(dummy);

    //���ܿ� �Ҽӵ��� �ʾҴٸ� ����Ʈ ������ �Ѱ���
    if (infoData == null) infoData = dummy.NotCloneInfo as T1;

    T2 statData = GetStatByInfoData<T2>(infoData);

    GetCurrentInfoData = infoData;
    GetCurrentStat = statData;

    UIManager.Instance.HidePanel("PenguinInfoUI");
    PenguinManager.Instance.CameraCompo.SetCameraTartget(dummy.transform.position, 30f, 5f);

    // �屺 ������ ��� ������ ����
    //if (stat is PenguinStat && infoData is EntityInfoDataSO)
    UIManager.Instance.ShowPanel("PenguinInfoUI");
    /*else
        UIManager.Instance.ShowPanel("GeneralInfoUI");*/

}
}


