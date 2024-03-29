using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class DummyPenguinListItem
{
    //실제 펭귄을 가지고 있는가?
    public bool IsHaveOwner = false;
    public DummyPenguin dummyPenguin;
}

public class SpawnManager : Singleton<SpawnManager>
{
    [SerializeField] private SoldierListSO soldierTypeListSO = null;

    #region 더미 펭귄 관련
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

                    //펭귄이랑 더미펭귄이랑 연결
                    penguinToDummyDictionary.Add(obj, dummyPenguin);
                    break;
                }
            }

        }

    }

    #region 더미 펭귄과 실제 펭귄 스왑 관련
    /// <summary>
    /// 전투가 끝나면 더미펭귄으로 바뀜
    /// </summary>
    /// <param name="obj"></param>
    public void ChangedToDummyPenguin(Penguin obj)
    {
        if (penguinToDummyDictionary.TryGetValue(obj, out var value))
        {
            var trm = obj.transform;
            //실제 펭귄을 꺼주고
            obj.gameObject.SetActive(false);

            //가짜 펭귄은 위치를 설정해주고 켜줌
            value.gameObject.SetActive(true);
            value.IsGoToHouse = false;
            value.SetPostion(trm);
            value.StateInit();
            value.ChangeNavqualityToHigh();
        }
    }

    /// <summary>
    /// 나의 더미 펭귄을 가져옴
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public DummyPenguin GetDummyPenguin(Penguin obj)
    {
        if (penguinToDummyDictionary.TryGetValue(obj, out var value))
            return value;

        Debug.Log($"{obj.name}은 더미 펭귄을 가지고 있지 않습니다.");
        return null;
    }


    #endregion
}


