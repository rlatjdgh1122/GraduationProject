using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public enum WorkerType
{
    Builder,
    ResourceAcquirer
}

public class WorkerManager : Singleton<WorkerManager>
{
    private readonly string WoodCutter = "WoodcutterPenguin";
    private readonly string Miner = "MinerPenguin";
    private readonly string Builder = "BuilderPenguin";

    [SerializeField]
    private int _maxWorkerCount;

    private List<Worker> _spawnedWorkerList = new();

    private WorkerFactroy _workerFactory;

    #region property

    //사용가능한 일꾼들
    public int AvailiableWorkerCount => MaxWorkerCount - _spawnedWorkerList.Count;
    public int MaxWorkerCount
    {
        get { return _maxWorkerCount; }
        set
        {
            _maxWorkerCount = value;
            OnUIUpdate?.Invoke(_maxWorkerCount);
        }
    }
    #endregion

    public delegate void OnUIUpdateHandler(int count);
    public event OnUIUpdateHandler OnUIUpdate;

    public override void Awake()
    {
        _workerFactory = GameObject.Find("Manager/WorkerManager").GetComponent<WorkerFactroy>();
    }

    public void SendWorkers(int count, WorkableObject workableObject)
    {
        if (workableObject.resourceType == ResourceType.Stone)
        {
            for (int i = 0; i < count; ++i)
            {
                var penguin = _workerFactory.SpawnPenguinHandler<MinerPenguin>(Miner);
                _spawnedWorkerList.Add(penguin);
                penguin.StartWork(workableObject);
            }

            if(WaveManager.Instance.CurrentWaveCount == 3)
            {
                ResourceManager.Instance.GetStone = false;
                ResourceManager.Instance.OnlyGetOneStone = true;
            }
        }
        else if (workableObject.resourceType == ResourceType.Wood)
        {
            for (int i = 0; i < count; ++i)
            {
                var penguin = _workerFactory.SpawnPenguinHandler<WoodCutterPenguin>(WoodCutter);
                _spawnedWorkerList.Add(penguin);
                penguin.StartWork(workableObject);
            }

            if (WaveManager.Instance.CurrentWaveCount == 4)
                ResourceManager.Instance.OnlyGetOneWood = true;
        }
    }

    public void ReturnWorker(WorkableObject workableObject)
    {
        List<Worker> list = _spawnedWorkerList.ToList();

        foreach (Worker worker in list)
        {
            if (worker.CanWork && worker.CurrentTarget == workableObject)
            {
                worker.FinishWork();
                //_spawnedWorkerList.Remove(worker);
            }
        }

        if (WaveManager.Instance.CurrentWaveCount == 3)
        {
            ResourceManager.Instance.GetStone = true;
        }
    }

    public void SendBuilders(int count, WorkableObject workableObject)
    {
        for (int i = 0; i < count; ++i)
        {
            var penguin = _workerFactory.SpawnPenguinHandler<BuilderPenguin>(Builder);
            _spawnedWorkerList.Add(penguin);
            penguin.StartWork(workableObject);
        }
    }

    public void ReturnBuilders(WorkableObject workableObject)
    {
        List<Worker> list = _spawnedWorkerList.ToList();

        foreach (Worker worker in list)
        {
            if (worker.CanWork && worker.CurrentTarget == workableObject)
            {
                worker.FinishWork();
                //_spawnedWorkerList.Remove(worker);
            }
        }
    }

    public void PopWorker(Worker worker)
    {
        _spawnedWorkerList.Remove(worker);
    }
}
