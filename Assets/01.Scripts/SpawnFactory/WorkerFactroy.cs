using System.Collections.Generic;
using UnityEngine;

public class WorkerFactroy : EntityFactory<MinerPenguin>
{
    public Transform spawnPoint;

    private List<MinerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    private List<MinerPenguin> _spawnedPenguins = new List<MinerPenguin>(); // ������ WorkerPenguin ������ ����Ʈ

    public void SetWorkerHandler() //�̰� WorkerManager���ٰ� ���� �÷��ִ°Ű� (�������� ��)
    {
        WorkerManager.Instance.SetWorker();
    }

    public void SpawnPenguinHandler() //�̰� ���� Worker�� ������ �����ϴ°� (�ڿ��� ������ ��ư ������ ��)
    {
        if (_workerList.Count > _spawnedPenguins.Count) // ���� �������� ���� WorkerPenguin�� �ִٸ�
        {
            MinerPenguin workerToSpawn = _workerList[_spawnedPenguins.Count]; // �������� ���� ���� WorkerPenguin�� ������
            MinerPenguin spawnPenguin = SpawnObject(workerToSpawn, spawnPoint.position) as MinerPenguin;
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

    protected override PoolableMono Create(MinerPenguin type)
    {
        string originalString = type.ToString();
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        MinerPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as MinerPenguin;
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
