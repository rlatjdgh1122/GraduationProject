using System.Collections.Generic;
using UnityEngine;

public class WorkerFactroy : EntityFactory<WorkerPenguin>
{
    public Transform spawnPoint;

    private List<WorkerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    private List<WorkerPenguin> _spawnedPenguins = new List<WorkerPenguin>(); // 생성된 WorkerPenguin 추적용 리스트

    public void SetWorkerHandler() //이게 WorkerManager에다가 수만 늘려주는거고 (구매했을 때)
    {
        WorkerManager.Instance.SetWorker();
    }

    public void SpawnPenguinHandler() //이게 이제 Worker를 실제로 생성하는거 (자원에 보내기 버튼 눌렀을 때)
    {
        if (_workerList.Count > _spawnedPenguins.Count) // 아직 생성되지 않은 WorkerPenguin이 있다면
        {
            WorkerPenguin workerToSpawn = _workerList[_spawnedPenguins.Count]; // 생성되지 않은 다음 WorkerPenguin을 가져옴
            WorkerPenguin spawnPenguin = SpawnObject(workerToSpawn, spawnPoint.position) as WorkerPenguin;
            _spawnedPenguins.Add(spawnPenguin); // 생성된 WorkerPenguin을 추적 리스트에 추가
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SetWorkerHandler();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            SpawnPenguinHandler();
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
