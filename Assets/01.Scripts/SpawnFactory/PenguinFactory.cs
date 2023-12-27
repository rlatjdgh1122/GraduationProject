using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int campFireidx = 0;

    private Transform[] _campFireSpawnPoints;
    private Transform[] _legionSpawnPoints;

    protected List<Penguin> curPTspawnPenguins = new List<Penguin>();

    private void Awake()
    {
        int a, b, c= 0, d= 0;
        (a, b) = (c, d);


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

    public void SpawnPenguinHandler(Penguin penguin, int ? legionidx = null)
    {
        campFireidx++;
        if (legionidx != null)
        {
            SpawnPenguin(penguin, _campFireSpawnPoints[campFireidx], _legionSpawnPoints[(int)legionidx].position);
            curPTspawnPenguins.Add(penguin);
        }
        else
        {
            SpawnPenguin(penguin, _campFireSpawnPoints[campFireidx]);
        }
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnPhaseStartEvent += ResetPTInfo;


        WaveManager.Instance.OnPhaseStartEvent += ResetPTInfo;
    }

    public void OnDisable()
    {
        WaveManager.Instance.OnPhaseStartEvent -= ResetPTInfo;
    }

    private void ResetPTInfo()
    {
        campFireidx = 0;
        curPTspawnPenguins.Clear();
    }

    private void SpawnPenguinsMoveToTent()
    {
        for(int i = 0; i < curPTspawnPenguins.Count; i++) 
        {
            if () // 생성된 펭귄이 군단에 들어가있지 않으면 텐트로 돌아가게.
            {
                curPTspawnPenguins[i].
            }
            // 군단에 들어가 있다면 알아서 군단위치로 가게
        }
    }    

    protected override Entity Create(Penguin type, Transform spawnTrm, Vector3? setVec = null)
    {
        string originalString = type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        return spawnPenguin;
    }
}
