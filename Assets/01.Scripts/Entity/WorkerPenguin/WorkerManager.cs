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

        if (WorkerCount >= count) //현재 일꾼의 수가 요청받은 일꾼보다 같거나 많다면
        {
            foreach (MinerPenguin worker in WorkerList) //일꾼들 리스트를 반복돌리고
            {
                if (!worker.CanWork) //그중에 CanWork가 비활성화 된 애들만
                {
                    var penguin = _workerFactory.SpawnPenguinHandler();
                    penguin.StartWork(workableObject); //활성화해주고

                    calledPenguinCount++; //값을 1 늘림
                }

                if (calledPenguinCount >= count) //값이 호출한 값과 같다면 반복문 중지
                    break;
            }
        }
    }

    public void ReturnWorkers(WorkableObject workableObject)
    {
        Debug.Log("돌아가자");
        foreach (MinerPenguin worker in WorkerList)
        {

            if (worker.CanWork
                && worker.Target.Equals(workableObject))
            {
                Debug.Log("고우고우");
                _workerFactory.DeSpawnPenguinHandler(worker);
                worker.FinishWork();
            }
        }
    }
}
