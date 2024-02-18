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

        if (WorkerCount >= count) //현재 일꾼의 수가 요청받은 일꾼보다 같거나 많다면
        {
            foreach (WorkerPenguin worker in WorkerList) //일꾼들 리스트를 반복돌리고
            {
                if (!worker.CanWork) //그중에 CanWork가 비활성화 된 애들만
                {
                    worker.StartWork(targetTrm); //활성화해주고
                    calledPenguinCount++; //값을 1 늘림
                }

                if (calledPenguinCount >= count) //값이 호출한 값과 같다면 반복문 중지
                    break;
            }
        }
    }
}
