using System.Collections.Generic;
using UnityEngine;

public class DummyPenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    private List<DummyPenguin> _dummyPenguinList = new();

    private void Awake()
    {
        PenguinManager.Instance.GetComponent_DummyFactory(this);

        var penguin = Resources.LoadAll<DummyPenguin>("PenguinPrefab/Dummy");
        _dummyPenguinList = new(penguin);
    }

    private void OnEnable()
    {
        SignalHub.OnGroundArrivedEvent += ResetPTInfo;
    }

    public void OnDisable()
    {
        SignalHub.OnGroundArrivedEvent -= ResetPTInfo;
    }

    /// <summary>
    /// 더미펭귄을 리턴한다
    /// </summary>
    public DummyPenguin FindDummyPenguin<T>(T info) where T : EntityInfoDataSO
    {
        return _dummyPenguinList.Find(penguin => penguin.NotCloneInfo.PenguinType == info.PenguinType);
    }

    /// <summary>
    /// 더미 펭귄 생성
    /// </summary>
    /// <param name="dummyPenguin"> 더미 펭귄</param>   
    public T SpawnDummyPenguinHandler<T>(T dummyPenguin) where T : DummyPenguin
    {
        if (spawnXIdx >= 5)
        {
            spawnXIdx = 0;
            spawnZIdx++;
        }

        Vector3 spawnVec = new Vector3(6 + (spawnXIdx * 1.5f),
                                       1.22f,
                                       -1.5f - (spawnZIdx * 1.5f));

        spawnXIdx++; // 생성 위치를 위한 idx

        T spawnPenguin = SpawnObject(dummyPenguin, spawnVec) as T;

        PenguinManager.Instance.AddDummyPenguin(spawnPenguin); // 리스트에 추가

        return spawnPenguin;
    }

    public void SpawnDummyPenguinByInfoData<T>(T info) where T : EntityInfoDataSO
    {
        var penguin = FindDummyPenguin(info);

        SpawnDummyPenguinHandler(penguin);
    }
    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
    }
    protected override PoolableMono Create(DummyPenguin _type)
    {
        string originalString = _type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        DummyPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as DummyPenguin;
        return spawnPenguin;
    }
}
