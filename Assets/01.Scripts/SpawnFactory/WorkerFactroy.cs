using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerFactroy : EntityFactory<Worker>
{
    public Transform spawnPoint;

    //private List<Worker> _workerList => WorkerManager.Instance.WorkerList;
    // ������ WorkerPenguin ������ ����Ʈ

    //�̰� WorkerManager���ٰ� ���� �÷��ִ°Ű� (�������� ��)
    public void SetWorkerHandler()
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(SuccesHudText);

        //WorkerManager.Instance.SetWorker();
    }

    //�̰� ���� Worker�� ������ �����ϴ°� (�ڿ��� ������ ��ư ������ ��)
    public T SpawnPenguinHandler<T>(T miner) where T : Worker
    {
        T spawnPenguin = SpawnObject(miner, spawnPoint.position) as T;

        return spawnPenguin;
    }

    protected override PoolableMono Create(Worker type)
    {
        string originalString = type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        Worker spawnPenguin = PoolManager.Instance.Pop(resultString) as Worker;
        spawnPenguin.WorkerStateChange();
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
