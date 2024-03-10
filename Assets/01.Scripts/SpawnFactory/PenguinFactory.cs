using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    private Vector3 _firstSpawnPos;

    protected List<Penguin> _curPTspawnPenguins = new List<Penguin>(); // 현재 준비시간에 생성한 펭귄 리스트

    private void Awake()
    {
        // 생성 위치들을 씬에서 받아와 설정해준다.

        _firstSpawnPos = transform.Find("FirstSpawnPos").transform.position;
    }

    public void SpawnPenguinHandler(Penguin penguin)
    {
        spawnZIdx++; // 생성 위치를 위한 idx
        Debug.Log(spawnZIdx);
        if (spawnZIdx >= 5)
        { 
            spawnZIdx = 0;
            spawnXIdx++;
        }
        Vector3 spawnVec = new Vector3(_firstSpawnPos.x + (spawnXIdx * 1.5f),
                                       _firstSpawnPos.y,
                                       _firstSpawnPos.z + (spawnZIdx * 1.5f));
        Penguin spawnPenguin =  SpawnObject(penguin, spawnVec) as Penguin;  //매개변수로 받아온 Penguin을 생성한다
        _curPTspawnPenguins.Add(spawnPenguin); // 리스트에 추가
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += ResetPTInfo;
        WaveManager.Instance.OnBattlePhaseStartEvent += SetCurPTspawnPenguins;
    }

    public void OnDisable()
    {
        WaveManager.Instance.OnIceArrivedEvent -= ResetPTInfo;
        WaveManager.Instance.OnBattlePhaseStartEvent -= SetCurPTspawnPenguins;
    }

    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
        _curPTspawnPenguins.Clear();
    }

    private void SetCurPTspawnPenguins() // 이번 준비시간에 생성한 펭귄들을 WaveManager로 넘겨준다.
    {
        WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override PoolableMono Create(Penguin type) // 실제로 펭귄이 생성되는 함수
    {
        string originalString = type.ToString();
        // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" ")); 

        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
