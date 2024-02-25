using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField]
    private WorkerPenguin _workerPrefab;
    [SerializeField]
    private List<WorkerPenguin> _workerList = new List<WorkerPenguin>();

    private WorkerFactroy _workerFactory;

    #region property
    public int WorkerCount => _workerList.Count;
    public List<WorkerPenguin> WorkerList => _workerList;
    #endregion

    public override void Awake()
    {
        _workerFactory = GameObject.Find("Manager/WorkerManager").GetComponent<WorkerFactroy>();

        WorkerPenguin[] workers = FindObjectsOfType<WorkerPenguin>();

        if (workers != null)
            _workerList.AddRange(workers);
    }

    public void SetWorker()
    {
        _workerList.Add(_workerPrefab);
    }

    public void SendWorkers(int count, ResourceObject target)
    {
        int calledPenguinCount = 0;

        if (WorkerCount >= count) //���� �ϲ��� ���� ��û���� �ϲۺ��� ���ų� ���ٸ�
        {
            foreach (WorkerPenguin worker in WorkerList) //�ϲ۵� ����Ʈ�� �ݺ�������
            {
                if (!worker.CanWork) //���߿� CanWork�� ��Ȱ��ȭ �� �ֵ鸸
                {
                    worker.StartWork(target); //Ȱ��ȭ���ְ�
                    _workerFactory.SpawnPenguinHandler();
                    calledPenguinCount++; //���� 1 �ø�
                }

                if (calledPenguinCount >= count) //���� ȣ���� ���� ���ٸ� �ݺ��� ����
                    break;
            }      
        }
    }

    public void ReturnWorkers(ResourceObject resource)
    {
        foreach (WorkerPenguin worker in WorkerList)
        {
            if (worker.CanWork && worker.Target == resource)
            {
                worker.FinishWork();
            }
        }
    }
}
