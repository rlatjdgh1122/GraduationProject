using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int campFireidx = 0;

    private Transform[] _campFireSpawnPoints; // 펭귄을 생성했을 때 놓을 위치
    private Transform[] _legionSpawnPoints; // 군단을 생성해 놓을 초기 위치

    protected List<Penguin> _curPTspawnPenguins = new List<Penguin>(); // 현재 준비시간에 생성한 펭귄 리스트

    private void Awake()
    {
        // 생성 위치들을 씬에서 받아와 설정해준다.

        GameObject campFireSpawnObj = GameObject.FindGameObjectWithTag("CampFireSpawnPos");
        GameObject legionSpawnObj = GameObject.FindGameObjectWithTag("LegionSpawnPos");

        Transform[] campFireSpawnPos = campFireSpawnObj.GetComponentsInChildren<Transform>();
        Transform[] legionSpawnPos = legionSpawnObj.GetComponentsInChildren<Transform>();

        _campFireSpawnPoints = new Transform[campFireSpawnPos.Length];
        _legionSpawnPoints = new Transform[legionSpawnPos.Length];

        for (int i = 0; i < _legionSpawnPoints.Length; i++)
        {
            _legionSpawnPoints[i] = legionSpawnPos[i];
        }

        for (int i = 0; i < _campFireSpawnPoints.Length; i++)
        {
            _campFireSpawnPoints[i] = campFireSpawnPos[i];
        }
    }

    public void SpawnPenguinHandler(Penguin penguin) 
    {
        campFireidx++; // 생성 위치를 위한 idx
        Penguin spawnPenguin =  SpawnPenguin(penguin, _campFireSpawnPoints[campFireidx]) as Penguin;  //매개변수로 받아온 Penguin을 생성한다
        _curPTspawnPenguins.Add(spawnPenguin); // 리스트에 추가
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += ResetPTInfo;
        WaveManager.Instance.OnPhaseStartEvent += SetCurPTspawnPenguins;
    }

    public void OnDisable()
    {
        WaveManager.Instance.OnIceArrivedEvent -= ResetPTInfo;
        WaveManager.Instance.OnPhaseStartEvent -= SetCurPTspawnPenguins;
    }

    private void ResetPTInfo()
    {
        campFireidx = 0;
        _curPTspawnPenguins.Clear();
    }

    private void SetCurPTspawnPenguins() // 이번 준비시간에 생성한 펭귄들을 WaveManager로 넘겨준다.
    {
        WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override Entity Create(Penguin type, Transform spawnTrm) // 실제로 펭귄이 생성되는 함수
    {
        string originalString = type.ToString();
        // 프리펩에 있는 스크립트의 이름을 가져오므로 필요한 부분만 가져온다. (ex: MeleePenguin (MeleePenguin) 에서 (MeleePenguin)제거.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" ")); 

        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        return spawnPenguin;
    }
}
