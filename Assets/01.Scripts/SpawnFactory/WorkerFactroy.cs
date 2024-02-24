using System.Collections.Generic;
using UnityEngine;

public class WorkerFactroy : EntityFactory<WorkerPenguin>
{
    public Transform spawnPoint;

    private List<WorkerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    private List<WorkerPenguin> _spawnedPenguins = new List<WorkerPenguin>(); // 积己等 WorkerPenguin 眠利侩 府胶飘

    public void SetWorkerHandler()
    {
        WorkerManager.Instance.SetWorker();
    }

    public void SpawnPenguinHandler(WorkerPenguin worker)
    {
        WorkerPenguin penguin = SpawnObject(worker, spawnPoint.position) as WorkerPenguin;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetWorkerHandler();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (WorkerPenguin worker in _workerList)
            {
                SpawnPenguinHandler(worker);
            }
        }
    }

    protected override PoolableMono Create(WorkerPenguin type)
    {
        string originalString = type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        WorkerPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as WorkerPenguin;
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
