using System.Collections.Generic;
using UnityEngine;

public class DummyPenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    private void OnEnable()
    {
        SignalHub.OnGroundArrivedEvent += ResetPTInfo;
    }

    public void OnDisable()
    {
        SignalHub.OnGroundArrivedEvent -= ResetPTInfo;
    }

    /// <summary>
    /// 더미 펭귄 생성
    /// </summary>
    /// <param name="dummyPenguin"> 더미 펭귄</param>   
    public void SpawnDummyPenguinHandler(DummyPenguin dummyPenguin)
    {
        if (spawnXIdx >= 5)
        {
            spawnXIdx = 0;
            spawnZIdx++;
        }

        Vector3 spawnVec = new Vector3(6 + (spawnXIdx * 1.5f),
                                       0.0f,
                                       -1.5f - (spawnZIdx * 1.5f));

        spawnXIdx++; // 생성 위치를 위한 idx

        DummyPenguin spawnPenguin = SpawnObject(dummyPenguin, spawnVec) as DummyPenguin;

        PenguinManager.Instance.AddDummyPenguin(spawnPenguin); // 리스트에 추가
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
