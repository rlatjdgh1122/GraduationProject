using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class DummyPenguinListItem
{
    //���� ����� ������ �ִ°�?
    public bool IsHaveOwner = false;
    public DummyPenguin dummyPenguin;
}

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;

    #region ���� ��� ����
    private List<DummyPenguinListItem> _dummyPenguinList = new();
    private Dictionary<Penguin, DummyPenguin> penguinToDummyDictionary = new();
    public int DummyPenguinCount => _dummyPenguinList.Count;
    #endregion

    private Dictionary<PenguinTypeEnum, Penguin> soldierTypeDictionary = new();
    public override void Awake()
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
        var prefab = ReturnPenguinByType(type);

        obj = PoolManager.Instance.Pop(prefab.name) as Penguin;
        obj.gameObject.SetActive(false);
        obj.transform.position = SpawnPoint;
        obj.SeatPos = seatPos;
        return obj;
    }
    private Penguin ReturnPenguinByType(PenguinTypeEnum type)
    {
        if (soldierTypeDictionary.TryGetValue(type, out var value)) return value;

        Debug.Log("�ش��ϴ� Ÿ���� ������Ʈ�� �����ϴ�. SoldierListSO�� Ȯ�����ּ���.");
        return null;
    }

    public void SetOwnerDummyPenguin(PenguinTypeEnum type, Penguin obj)
    {
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
                    dummyPenguin.SetOwner(obj);
                    info.IsHaveOwner = true;

                    //����̶� ��������̶� ����
                    penguinToDummyDictionary.Add(obj, dummyPenguin);
                    break;
                }
            }

        }

    }

    #region ���� ��ϰ� ���� ��� ���� ����
    /// <summary>
    /// ������ ������ ����������� �ٲ�
    /// </summary>
    /// <param name="obj"></param>
    public void ChangedToDummyPenguin(Penguin obj)
    {
        if (penguinToDummyDictionary.TryGetValue(obj, out var value))
        {
            var trm = obj.transform;
            //���� ����� ���ְ�
            obj.gameObject.SetActive(false);

            //��¥ ����� ��ġ�� �������ְ� ����
            value.gameObject.SetActive(true);
            value.IsGoToHouse = false;
            value.SetPostion(trm);
            value.StateInit();
            value.ChangeNavqualityToHigh();
        }
    }

    /// <summary>
    /// ���� ���� ����� ������
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public DummyPenguin GetDummyPenguin(Penguin obj)
    {
        if (penguinToDummyDictionary.TryGetValue(obj, out var value))
            return value;

        Debug.Log($"{obj.name}�� ���� ����� ������ ���� �ʽ��ϴ�.");
        return null;
    }


    #endregion
}


