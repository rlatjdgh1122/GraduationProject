using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField]
    private MinerPenguin _workerPrefab;
    [SerializeField]
    private List<MinerPenguin> _workerList = new List<MinerPenguin>();

    private WorkerFactroy _workerFactory;

    #region property
    public int WorkerCount => _workerList.Count;
    public List<MinerPenguin> WorkerList => _workerList;
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

        if (WorkerCount >= count) //���� �ϲ��� ���� ��û���� �ϲۺ��� ���ų� ���ٸ�
        {
            foreach (MinerPenguin worker in WorkerList) //�ϲ۵� ����Ʈ�� �ݺ�������
            {
                if (!worker.CanWork) //���߿� CanWork�� ��Ȱ��ȭ �� �ֵ鸸
                {
                    var penguin = _workerFactory.SpawnPenguinHandler();
                    penguin.StartWork(workableObject); //Ȱ��ȭ���ְ�

                    calledPenguinCount++; //���� 1 �ø�
                }

                if (calledPenguinCount >= count) //���� ȣ���� ���� ���ٸ� �ݺ��� ����
                    break;
            }
        }
    }

    public void ReturnWorkers(WorkableObject workableObject)
    {
        Debug.Log("���ư���");
        foreach (MinerPenguin worker in WorkerList)
        {

            if (worker.CanWork
                && worker.Target.Equals(workableObject))
            {
                Debug.Log("�����");
                _workerFactory.DeSpawnPenguinHandler(worker);
                worker.FinishWork();
            }
        }
    }
}
