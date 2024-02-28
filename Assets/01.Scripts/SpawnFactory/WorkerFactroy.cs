using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerFactroy : EntityFactory<MinerPenguin>
{
    public Transform spawnPoint;

    private List<MinerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    public List<MinerPenguin> spawnedPenguins = new List<MinerPenguin>();
    // 생성된 WorkerPenguin 추적용 리스트

    //이게 WorkerManager에다가 수만 늘려주는거고 (구매했을 때)
    public void SetWorkerHandler()
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(SuccesHudText);

        WorkerManager.Instance.SetWorker();
    }

    public MinerPenguin SpawnPenguinHandler() //이게 이제 Worker를 실제로 생성하는거 (자원에 보내기 버튼 눌렀을 때)
    {
        if (_workerList.Count >= spawnedPenguins.Count) // 아직 생성되지 않은 WorkerPenguin이 있다면
        {
            // 생성되지 않은 다음 WorkerPenguin을 가져옴
            MinerPenguin workerToSpawn = _workerList[spawnedPenguins.Count];
            MinerPenguin spawnPenguin = SpawnObject(workerToSpawn, spawnPoint.position) as MinerPenguin;

            // 생성된 WorkerPenguin을 추적 리스트에 추가
            spawnedPenguins.Add(spawnPenguin);

            return spawnPenguin;
        }
        else
        {
            Debug.LogError("일꾼이 다 일하고잇잖슴;;");
            return null;
        }
    }
    public void DeSpawnPenguinHandler(MinerPenguin worker)
    {
        spawnedPenguins.Remove(worker);
    }

    protected override PoolableMono Create(MinerPenguin type)
    {
        string originalString = type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        MinerPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as MinerPenguin;
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
