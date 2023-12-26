using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int campFireidx = -1;

    private Transform[] _campFireSpawnPoints;
    private Transform[] _legionSpawnPoints;

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
        }
        else
        {
            SpawnPenguin(penguin, _campFireSpawnPoints[campFireidx]);
        }
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnPhaseStartEvent += ResetCampFireIdx;
    }

    public void OnDisable()
    {
        WaveManager.Instance.OnPhaseStartEvent -= ResetCampFireIdx;
    }

    private void ResetCampFireIdx()
    {
        campFireidx = 0;
    }

    protected override Entity Create(Penguin type, Transform spawnTrm, Vector3? setVec = null)
    {
        string originalString = type.ToString();
        string resultString = originalString.Replace($"({type})", "").Trim();
        Debug.Log(resultString);
        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        return spawnPenguin;
    }
}
