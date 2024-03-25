using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerFactroy : EntityFactory<Worker>
{
    public Transform spawnPoint;

    //private List<Worker> _workerList => WorkerManager.Instance.WorkerList;
    // 생성된 WorkerPenguin 추적용 리스트

    //이게 WorkerManager에다가 수만 늘려주는거고 (구매했을 때)
    public void SetWorkerHandler()
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(SuccesHudText);

        //WorkerManager.Instance.SetWorker();
    }

    //이게 이제 Worker를 실제로 생성하는거 (자원에 보내기 버튼 눌렀을 때)
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
