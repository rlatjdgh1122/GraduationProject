using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    protected List<DummyPenguin> _curPTspawnPenguins = new List<DummyPenguin>(); // 현재 준비시간에 생성한 펭귄 리스트

    public void SpawnPenguinHandler(DummyPenguin dummyPenguin)
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

        DummyPenguin spawnPenguin = SpawnObject(dummyPenguin, spawnVec) as DummyPenguin;  //매개변수로 받아온 Penguin을 생성한다

        _curPTspawnPenguins.Add(spawnPenguin); // 리스트에 추가
    }

    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent += SetCurPTspawnPenguins;
    }

    public void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent -= SetCurPTspawnPenguins;
    }

    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
        _curPTspawnPenguins.Clear();
    }

    private void SetCurPTspawnPenguins() // 이번 준비시간에 생성한 펭귄들을 WaveManager로 넘겨준다.
    {
        //WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override PoolableMono Create(DummyPenguin _type)
    {
        string originalString = _type.ToString();
        // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        DummyPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as DummyPenguin;
        //spawnPenguin.SetCanInitTent(false);
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
