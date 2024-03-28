using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DummyPenguinListItem
{
    public bool IsHaveOwner = false;
    public DummyPenguin dummyPenguin;
}

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;

    #region 더미 펭귄 관련
    private List<DummyPenguinListItem> _dummyPenguinList = new();
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

        Debug.Log("해당하는 타입의 오브젝트는 없습니다. SoldierListSO를 확인해주세요.");
        return null;
    }

    public void SetOwnerDummyPenguin(PenguinTypeEnum type, Penguin obj)
    {
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
                    dummyPenguin.SetOwner(obj);
                    info.IsHaveOwner = true;
                    break;
                }
            }

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
}


