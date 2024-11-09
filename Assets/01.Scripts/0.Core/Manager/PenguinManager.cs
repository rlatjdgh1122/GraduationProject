using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void Clear()
    {
        soldierTypeToDefaultPenguinDic.TryClear();
        soldierTypeToDefaultStatDic.TryClear();
        soldierTypeToDefaultInfoDataDic.TryClear();

        penguinToDummyDic.TryClear();
        dummyToPenguinDic.TryClear();

        infoDataToPenguinDic.TryClear();
        penguinToInfoDataDic.TryClear();

        soliderTypeToGeneralStatDic.TryClear();
        BelongDummyPenguinList.TryClear();
        NotBelongDummyPenguinList.TryClear();

        SoldierPenguinList.TryClear();
        GeneralDummyPenguinList.TryClear();
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

    public void DummyToPenguinMapping(DummyPenguin d, Penguin p)
    {
        penguinToDummyDic.Add(p, d);
        dummyToPenguinDic.Add(d, p);

        NotBelongDummyPenguinList.Remove(d);
        BelongDummyPenguinList.Add(d);
    }

    public void RemoveDummy(DummyPenguin d)
    {
        BelongDummyPenguinList.Remove(d);
        NotBelongDummyPenguinList.Add(d);
    }

    private Dictionary<EntityInfoDataSO, Penguin> infoDataToPenguinDic = new();
    private Dictionary<Penguin, EntityInfoDataSO> penguinToInfoDataDic = new();

    public BaseStat GetCurrentStat = null;
    public EntityInfoDataSO GetCurrentInfoData = null;
    #endregion

    //�屺 ������ ������
    private Dictionary<PenguinTypeEnum, GeneralStat> soliderTypeToGeneralStatDic = new();

    #region ��� ����Ʈ

    public List<DummyPenguin> DummyPenguinList = new();

    /// <summary>
    /// ���ܿ� �Ҽӵ������� ���� ���
    /// </summary>
    public List<DummyPenguin> BelongDummyPenguinList = new();
    /// <summary>
    /// ���ܿ� �Ҽӵ� ���� ���
    /// </summary>
    public List<DummyPenguin> NotBelongDummyPenguinList = new();
    public List<DummyPenguin> GeneralDummyPenguinList = new();

    public List<Penguin> SoldierPenguinList = new();
    public int DummyPenguinCount => DummyPenguinList.Count;
    public int SoldierPenguinCount => SoldierPenguinList.Count;
    #endregion

    public DummyPenguinFactory DummyFactoryCompo { get; set; }
    public CameraSystem CameraCompo { get; set; }
    public DummyPenguinCamera DummyPenguinCameraCompo { get; set; }

    //PenguinManger��
    private List<DummyPenguinListItem> _itemDummyPenguinList = new();

    #region  GetComponent

    public void GetComponent_DummyFactory(DummyPenguinFactory compo)
    {
        DummyFactoryCompo = compo;
    }
    public void GetComponent_CameraSystem(CameraSystem compo)
    {
        CameraCompo = compo;
    }

    public void GetComponent_DummyCameraSystem(DummyPenguinCamera compo)
    {
        DummyPenguinCameraCompo = compo;
    }
    #endregion
    //���� �Ŵ������� ���
    public void Setting(SoldierRegisterSO soldierTypeListSO)
    {
        foreach (var Soldier in soldierTypeListSO.Data)
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
        _itemDummyPenguinList.Add(new DummyPenguinListItem
        {
            //IsHaveOwner = false, 
            IsHaveOwner = true, //�ϴ��� �̷���
            dummyPenguin = obj
        });

        DummyPenguinList.Add(obj);
        if (obj is GeneralDummyPengiun)
        {
            NotBelongDummyPenguinList.Add(obj);
        }
        else
        {
            BelongDummyPenguinList.Add(obj);
        }
    }

    public void AddGeneralStat(PenguinTypeEnum type, GeneralStat stat)
    {
        soliderTypeToGeneralStatDic.Add(type, stat);
    }


    public void AddSoliderPenguin(Penguin obj)
    {
        SoldierPenguinList.Add(obj);
    }

    /// <summary>
    /// ������ �� ���
    /// </summary>
    /// <param name="obj"></param>
    public void RetireDummyPenguin(DummyPenguin obj)
    {
        DummyPenguinList.Remove(obj);

        var penguin = dummyToPenguinDic[obj];

        SoldierPenguinList.Remove(penguin);
        BelongDummyPenguinList.Remove(obj);

        PoolManager.Instance.Push(obj);
    }

    /// <summary>
    /// ����� �� ���
    /// </summary>
    /// <param name="obj"></param>
    public void DeadSoliderPenguin(Penguin obj)
    {
        SoldierPenguinList.Remove(obj);

        var dummy = penguinToDummyDic[obj];

        DummyPenguinList.Remove(dummy);
        BelongDummyPenguinList.Remove(dummy);

        dummy.gameObject.SetActive(false);
        //PoolManager.Instance.Push(dummy);
    }

    public void ResurrectedSoldierPenguin(Penguin obj)
    {
        SoldierPenguinList.Add(obj);

        var dummy = penguinToDummyDic[obj];

        DummyPenguinList.Add(dummy);
        BelongDummyPenguinList.Add(dummy);

        dummy.gameObject.SetActive(true);
        dummy.StateInit();
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

        if (!infoDataToPenguinDic.ContainsKey(dataType))
        {
            infoDataToPenguinDic.Add(dataType, penguin);
            penguinToInfoDataDic.Add(penguin, dataType);
        }

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

        return resultDummy;
    }

    public DummyPenguin SpawnDummyPenguinByInfoData<T>(T info) where T : EntityInfoDataSO
    {
        return DummyFactoryCompo.SpawnDummyPenguinByInfoData(info);
    }

    public void InsertGeneralDummyPenguin(DummyPenguin penguin)
    {
        if (!GeneralDummyPenguinList.Contains(penguin))
            GeneralDummyPenguinList.Add(penguin);
    }

    public DummyPenguin FindGeneralDummyPenguin(PenguinTypeEnum type)
    {
        return GeneralDummyPenguinList.Find(dummyPenguinInfo => dummyPenguinInfo.NotCloneInfo.PenguinType == type);
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

    public GeneralStat GetGeneralStatToSoliderType(PenguinTypeEnum type)
   => soliderTypeToGeneralStatDic[type];

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
    public void ShowPenguinInfoUI(DummyPenguin dummy)
    {
        var defaultInfo = dummy.CloneInfo;
        var infoData = GetInfoDataByDummyPenguin<PenguinInfoDataSO>(dummy);

        if (infoData == null)
            infoData = defaultInfo;
        else
            infoData.PenguinPersonality = defaultInfo.PenguinPersonality;

        //���ܿ� �Ҽӵ��� �ʾҴٸ� default������ �Ѱ���

        var statData = GetStatByInfoData<PenguinStat>(infoData);

        GetCurrentInfoData = infoData;
        GetCurrentStat = statData;

        UIManager.Instance.HidePanel("PenguinInfoUI");
        DummyPenguinCameraCompo.SetCamera(dummy.transform);

        // �屺 ������ ��� ������ ����
        UIManager.Instance.ShowPanel("PenguinInfoUI");
    }

    public void ShowGeneralInfoUI(GeneralDummyPengiun dummy)
    {
        var getInfoData = GetInfoDataByDummyPenguin<GeneralInfoDataSO>(dummy);
        var data = getInfoData == null ? dummy.CloneInfo : getInfoData;
        var statData = GetStatByInfoData<GeneralStat>(data);

        if (getInfoData)
            statData.InfoData.LegionName = getInfoData.LegionName;
        else
            statData.InfoData.LegionName = "�Ҽӵ� ���� ����";

        GetCurrentInfoData = data;
        GetCurrentStat = statData;

        UIManager.Instance.HidePanel("GeneralInfoUI");
        DummyPenguinCameraCompo.SetCamera(dummy.transform);

        // �屺 ������ ��� ������ ����
        UIManager.Instance.ShowPanel("GeneralInfoUI");
    }
}


