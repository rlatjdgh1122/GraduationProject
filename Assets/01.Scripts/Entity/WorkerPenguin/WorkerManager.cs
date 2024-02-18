using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : Singleton<WorkerManager>
{
    [SerializeField]
    private List<WorkerPenguin> _workerList = new List<WorkerPenguin>();

    public int WorkerCount => _workerList.Count;

    #region property
    List<WorkerPenguin> WorkerList => _workerList;
    #endregion

    public override void Awake()
    {
        WorkerPenguin[] workers = FindObjectsOfType<WorkerPenguin>();

        if (workers != null)
            _workerList.AddRange(workers);
    }

    public void SendWorkers(int count, Transform targetTrm)
    {
        int calledPenguinCount = 0;

        if (WorkerCount >= count) //���� �ϲ��� ���� ��û���� �ϲۺ��� ���ų� ���ٸ�
        {
            foreach (WorkerPenguin worker in WorkerList) //�ϲ۵� ����Ʈ�� �ݺ�������
            {
                if (!worker.CanWork) //���߿� CanWork�� ��Ȱ��ȭ �� �ֵ鸸
                {
                    worker.StartWork(targetTrm); //Ȱ��ȭ���ְ�
                    calledPenguinCount++; //���� 1 �ø�
                }

                if (calledPenguinCount >= count) //���� ȣ���� ���� ���ٸ� �ݺ��� ����
                    break;
            }
        }
    }
}
