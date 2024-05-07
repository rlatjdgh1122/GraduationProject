using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField]
    private MinerPenguin _minerPrefab;
    [SerializeField]
    private WoodCutterPenguin _woodCutterPrefab;

    [SerializeField]
    private BuilderPenguin _builderPrefabs;

    [SerializeField]
    private int _maxWorkerCount;

    private List<MinerPenguin> _minerList = new List<MinerPenguin>();
    private List<WoodCutterPenguin> _woodCutterList = new List<WoodCutterPenguin>();

    private List<BuilderPenguin> _builderList = new List<BuilderPenguin>();

    private List<MinerPenguin> _spawnedMinerList = new List<MinerPenguin>();
    private List<WoodCutterPenguin> _spawnedWoodCutterList = new List<WoodCutterPenguin>();

    private List<BuilderPenguin> _spawnedBuilderList = new List<BuilderPenguin>();

    private WorkerFactroy _workerFactory;

    #region property
    public int WorkerCount => _minerList.Count;
    public int AvailiableMinerCount => _minerList.Count - _spawnedMinerList.Count;
    public int AvailiableWoodCutterCount => _woodCutterList.Count - _spawnedWoodCutterList.Count;
    public int MaxWorkerCount
    {
        get { return _maxWorkerCount; }
        set 
        {  
            _maxWorkerCount = value;
            OnUIUpdate?.Invoke(_maxWorkerCount);
        }
    }

    public List<MinerPenguin> SpawnedMinerList => _spawnedMinerList;
    public List<WoodCutterPenguin> SpawnedWoodCutterList => _spawnedWoodCutterList;
    #endregion

    public delegate void OnUIUpdateHandler(int count);
    public event OnUIUpdateHandler OnUIUpdate;

    public override void Awake()
    {
        _workerFactory = GameObject.Find("Manager/WorkerManager").GetComponent<WorkerFactroy>();

        SetWorkers();
    }

    public void SetWorkers()
    {
        for (int i = 0; i < _maxWorkerCount; i++)
        {
            _minerList.Add(_minerPrefab);
            _builderList.Add(_builderPrefabs);
            _woodCutterList.Add(_woodCutterPrefab);
        }
    }

    public void SendWorkers(int count, WorkableObject workableObject)
    {
        int calledPenguin = 0;

        if (workableObject.resourceType == ResourceType.Stone)
        {
            foreach (MinerPenguin miner in _minerList)
            {
                if (miner.EndWork)
                {
                    var penguin = _workerFactory.SpawnPenguinHandler(miner);
                    _spawnedMinerList.Add(penguin);
                    penguin.StartWork(workableObject);
                }

                calledPenguin++;
                if (calledPenguin >= count)
                    break;
            }
        }
        else if (workableObject.resourceType == ResourceType.Wood)
        {
            foreach (WoodCutterPenguin wood in _woodCutterList)
            {
                if (wood.EndWork)
                {
                    var penguin = _workerFactory.SpawnPenguinHandler(wood);
                    _spawnedWoodCutterList.Add(penguin);
                    penguin.StartWork(workableObject);
                }

                calledPenguin++;
                if (calledPenguin >= count)
                    break;
            }
        }
    }

    public void ReturnWorker(WorkableObject workableObject)
    {
        if (workableObject.resourceType == ResourceType.Stone)
        {
            List<MinerPenguin> list = new List<MinerPenguin>(_spawnedMinerList);

            foreach (MinerPenguin worker in list)
            {
                if (worker.CanWork && worker.CurrentTarget == workableObject)
                {
                    worker.FinishWork();
                    _spawnedMinerList.Remove(worker);
                }
            }
        }
        else if (workableObject.resourceType == ResourceType.Wood)
        {
            List<WoodCutterPenguin> list = new List<WoodCutterPenguin>(_spawnedWoodCutterList);

            foreach (WoodCutterPenguin worker in list)
            {
                if (worker.CanWork && worker.CurrentTarget == workableObject)
                {
                    worker.FinishWork();
                    _spawnedWoodCutterList.Remove(worker);
                }
            }
        }
    }

    public void SendBuilders(int count, WorkableObject workableObject)
    {
        int calledPenguin = 0;

        foreach (BuilderPenguin builder in _builderList)
        {
            if (builder.EndWork)
            {
                var penguin = _workerFactory.SpawnPenguinHandler(builder);
                _spawnedBuilderList.Add(penguin);
                penguin.StartWork(workableObject);
            }

            calledPenguin++;
            if (calledPenguin >= count)
                break;
        }

    }

    public void ReturnBuilders(WorkableObject workableObject)
    {
        List<BuilderPenguin> list = new List<BuilderPenguin>(_spawnedBuilderList);

        foreach (BuilderPenguin builder in list)
        {
            if (builder.CanWork && builder.CurrentTarget == workableObject)
            {
                builder.FinishWork();
                _spawnedBuilderList.Remove(builder);
            }
        }
    }
}
