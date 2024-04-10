using System.Collections.Generic;
using UnityEngine;

public class PenguinManager
{
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
    public class DummyPenguinListItem
    {
        //���� ����� ������ �ִ°�?
        public bool IsHaveOwner = false;
        public DummyPenguin dummyPenguin;
    }

    //Type���� ��� ���
    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();

    #region ��� ����Ʈ
    private List<DummyPenguinListItem> _dummyPenguinList = new();
    public List<DummyPenguin> DummyPenguinList = new();
    /// <summary>
    /// ���ܿ� �Ҽӵ������� ���� ���
    /// </summary>
    public List<DummyPenguin> NotBelongDummyPenguinList = new();
    public List<Penguin> SoldierPenguinList = new();
    public int DummyPenguinCount => NotBelongDummyPenguinList.Count;
    public int SoldierPenguinCount => SoldierPenguinList.Count;
    #endregion


    #region ����
    private Dictionary<Penguin, DummyPenguin> penguinToDummyDic = new();
    private Dictionary<DummyPenguin, Penguin> dummyToPenguinDic = new();

    private Dictionary<EntityInfoDataSO, DummyPenguin> infoDataToDummyDic = new();

    private Dictionary<LegionInventoryData, Penguin> legionDataToPenguinDic = new();
    private Dictionary<Penguin, LegionInventoryData> penguinToLegionDataDic = new();
    #endregion

    //���� �Ŵ������� ���
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
    /// �ر� �����ϴ� �Լ�
    /// </summary>
    /// <typeparam name="T"> Penguin ��ӹ��� �ֵ�</typeparam>
    /// <param name="SpawnPoint"> ���� ��ġ</param>
    /// <param name="seatPos"> ��ġ ��ġ *�ǵ����̸� ������� ����*</param>
    /// <returns> �ϰ� ���� ���</returns>
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

        Debug.Log("�ش��ϴ� Ÿ���� ������Ʈ�� �����ϴ�. SoldierListSO�� Ȯ�����ּ���.");
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
            Debug.Log($"GetPenguinByDummyPenguin���� ����");
            Debug.Log($"{dummyPenguin.name}�� ��ϵ� ����� �����ϴ�.");
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
            Debug.Log($"GetPenguinByDummyPenguin���� ����");
            Debug.Log($"{data}�� ��ϵ� ����� �����ϴ�.");
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
            Debug.Log("GetDummyByPenguin���� ����");
            Debug.Log($"{penguin.name}�� ��ϵ� ���� ����� �����ϴ�.");
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
            Debug.Log("GetStatByInfoData���� ����");
            Debug.Log("UI������ ��ϵ� ���� ����� �����ϴ�.");
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
        //���⼭ �Ҽӵ� ������� ����Ʈ�� �Ҽӵ������� �ֵ� ������

        foreach (var info in _dummyPenguinList)
        {
            var PenguinType = info.dummyPenguin.PenguinUIInfo.PenguinType;
            var dummyPenguin = info.dummyPenguin;

            //���ʸ� ������ ���� �ʴٸ�
            if (!info.IsHaveOwner)
            {
                //��� Ÿ���� ���ٸ�
                if (PenguinType == type)
                {
                    dummyPenguin.SetOwner(penguin);
                    info.IsHaveOwner = true;

                    //����̶� ��������̶� ����
                    penguinToDummyDic.Add(penguin, dummyPenguin);
                    DummyToPenguinDic.TryAdd(dummyPenguin, penguin);
                    break;
                }
            }

        }

    }
}


