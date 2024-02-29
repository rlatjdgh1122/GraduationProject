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
            // ����� ������ �����Ǿ��ִ� �ֵ��� ���� ���� ������ ���ư��� �ֵ�
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

            foreach (Worker worker in List) //�ϲ۵� ����Ʈ�� �ݺ�������
            {
                var penguin = _workerFactory.SpawnPenguinHandler(worker);
                penguin.StartWork(workableObject); //Ȱ��ȭ���ְ�
                OnSpawnMinerrPenguin(penguin);

                calledPenguinCount++; //���� 1 �ø�

                if (calledPenguinCount >= count) //���� ȣ���� ���� ���ٸ� �ݺ��� ����
                    break;
            }

        }
    }
    public void ReturnWorkers(WorkableObject workableObject)
    {
        // ���ο� ����Ʈ�� SpawnedPenguinList�� �����մϴ�.
        List<Worker> list = new List<Worker>(SpawnedPenguinList);

        // SpawnedPenguinList�� �������� �ʰ� ���ο� ����Ʈ�� �ݺ��մϴ�.
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
