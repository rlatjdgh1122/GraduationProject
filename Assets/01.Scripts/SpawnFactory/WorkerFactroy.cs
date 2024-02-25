using System.Collections.Generic;
using UnityEngine;

public class WorkerFactroy : EntityFactory<WorkerPenguin>
{
    public Transform spawnPoint;

    private List<WorkerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    private List<WorkerPenguin> _spawnedPenguins = new List<WorkerPenguin>(); // ������ WorkerPenguin ������ ����Ʈ

    public void SetWorkerHandler() //�̰� WorkerManager���ٰ� ���� �÷��ִ°Ű� (�������� ��)
    {
        WorkerManager.Instance.SetWorker();
    }

    public void SpawnPenguinHandler() //�̰� ���� Worker�� ������ �����ϴ°� (�ڿ��� ������ ��ư ������ ��)
    {
        if (_workerList.Count > _spawnedPenguins.Count) // ���� �������� ���� WorkerPenguin�� �ִٸ�
        {
            WorkerPenguin workerToSpawn = _workerList[_spawnedPenguins.Count]; // �������� ���� ���� WorkerPenguin�� ������
            WorkerPenguin spawnPenguin = SpawnObject(workerToSpawn, spawnPoint.position) as WorkerPenguin;
            _spawnedPenguins.Add(spawnPenguin); // ������ WorkerPenguin�� ���� ����Ʈ�� �߰�
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
