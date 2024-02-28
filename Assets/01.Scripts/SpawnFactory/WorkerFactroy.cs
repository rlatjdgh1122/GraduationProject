using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorkerFactroy : EntityFactory<MinerPenguin>
{
    public Transform spawnPoint;

    private List<MinerPenguin> _workerList => WorkerManager.Instance.WorkerList;
    private List<MinerPenguin> _spawnedPenguins = new List<MinerPenguin>();
    // ������ WorkerPenguin ������ ����Ʈ

    public void SetWorkerHandler() //�̰� WorkerManager���ٰ� ���� �÷��ִ°Ű� (�������� ��)
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.SpawnHudText(SuccesHudText);

        WorkerManager.Instance.SetWorker();
    }

    public MinerPenguin SpawnPenguinHandler() //�̰� ���� Worker�� ������ �����ϴ°� (�ڿ��� ������ ��ư ������ ��)
    {
        if (_workerList.Count >= _spawnedPenguins.Count) // ���� �������� ���� WorkerPenguin�� �ִٸ�
        {
            // �������� ���� ���� WorkerPenguin�� ������
            MinerPenguin workerToSpawn = _workerList[_spawnedPenguins.Count];
            MinerPenguin spawnPenguin = SpawnObject(workerToSpawn, spawnPoint.position) as MinerPenguin;

            _spawnedPenguins.Add(spawnPenguin); // ������ WorkerPenguin�� ���� ����Ʈ�� �߰�

            return spawnPenguin;
        }
        else
        {
            Debug.LogError("�ϲ��� �� ���ϰ����ݽ�;;");
            return null;
        } 
    }
    public void DeSpawnPenguinHandler(MinerPenguin worker)
    {
        _spawnedPenguins.Remove(worker);
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
