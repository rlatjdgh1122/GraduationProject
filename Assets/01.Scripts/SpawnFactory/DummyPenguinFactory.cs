using System.Collections.Generic;
using UnityEngine;

public class DummyPenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    protected List<DummyPenguin> _dummyPenguins = new List<DummyPenguin>(); // 현재 준비시간에 생성한 펭귄 리스트


    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent += SetupComplete;
    }

    public void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent -= SetupComplete;
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

        _dummyPenguins.Add(spawnPenguin); // 리스트에 추가
    }

    /// <summary>
    /// 생성된 더미 펭귄을 배틀 라운드가 되면 스폰매니저로 배송
    /// </summary>
    private void SetupComplete()
    {
        SpawnManager.Instance.GetDummyPenguinList(_dummyPenguins);
    }

    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
        _dummyPenguins.Clear();
    }
    protected override PoolableMono Create(DummyPenguin _type)
    {
        string originalString = _type.ToString();
        // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        DummyPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as DummyPenguin;
        return spawnPenguin;
    }

    /*  protected override PoolableMono Create(Penguin type) // 실제로 펭귄이 생성되는 함수
      {
          string originalString = type.ToString();
          // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
          string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

          Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
          spawnPenguin.SetCanInitTent(false);
          return spawnPenguin;
      }*/
}
