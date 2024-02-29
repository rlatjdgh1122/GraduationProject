using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField]
    private Worker _workerPrefab;

    [SerializeField] private List<Worker> _workerList = new List<Worker>();

    [SerializeField] private List<Worker> _spawnedPenguinList = new List<Worker>();

    private WorkerFactroy _workerFactory;

    #region property
    public int WorkerCount => _workerList.Count;
    public int SpawnedPenguinCount => _spawnedPenguinList.Count;
    public List<Worker> WorkerList => _workerList;
    public List<Worker> SpawnedPenguinList => _spawnedPenguinList;
    #endregion

    public override void Awake()
    {
        _workerFactory = GameObject.Find("Manager/WorkerManager").GetComponent<WorkerFactroy>();

        MinerPenguin[] workers = FindObjectsOfType<MinerPenguin>();

        if (workers != null)
            _workerList.AddRange(workers);
    }

    public void SetWorker()
    {
        _workerList.Add(_workerPrefab);
    }


    public void SendWorkers(int count, WorkableObject workableObject)
    {
        int calledPenguinCount = 0;
        var List = WorkerList;

        if (WorkerCount + SpawnedPenguinCount >= count)
        {
            // 여기는 기존에 스폰되어있는 애들이 일이 끝나 집으로 돌아가는 애들
            foreach (Worker worker in SpawnedPenguinList)
            {
                if (worker.EndWork == true)
                {
                    worker.StartWork(workableObject);

                    calledPenguinCount++;
                    if (calledPenguinCount >= count)
                        break;
                }
            }

            foreach (Worker worker in List) //일꾼들 리스트를 반복돌리고
            {
                var penguin = _workerFactory.SpawnPenguinHandler(worker);
                penguin.StartWork(workableObject); //활성화해주고
                OnSpawnMinerrPenguin(penguin);

                calledPenguinCount++; //값을 1 늘림

                if (calledPenguinCount >= count) //값이 호출한 값과 같다면 반복문 중지
                    break;
            }

        }
    }
    public void ReturnWorkers(WorkableObject workableObject)
    {
        // 새로운 리스트에 SpawnedPenguinList를 복사합니다.
        List<Worker> list = new List<Worker>(SpawnedPenguinList);

        // SpawnedPenguinList를 수정하지 않고 새로운 리스트를 반복합니다.
        foreach (Worker worker in list)
        {
            if (worker.CanWork && worker.Target == workableObject)
            {
                worker.FinishWork();
                OnDeSpawnMinerrPenguin(worker);
            }
        }
    }


    public void OnSpawnMinerrPenguin(Worker miner)
    {
        _workerList.RemoveAt(_workerList.Count - 1);
        _spawnedPenguinList.Add(miner);
    }
    public void OnDeSpawnMinerrPenguin(Worker miner)
    {
        _spawnedPenguinList.Remove(miner);
        _workerList.Add(miner);
    }
}
