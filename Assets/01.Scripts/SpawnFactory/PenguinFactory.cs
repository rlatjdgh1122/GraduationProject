using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int campFireidx = 0;

    private Transform[] _campFireSpawnPoints; // ����� �������� �� ���� ��ġ
    private Transform[] _legionSpawnPoints; // ������ ������ ���� �ʱ� ��ġ

    protected List<Penguin> _curPTspawnPenguins = new List<Penguin>(); // ���� �غ�ð��� ������ ��� ����Ʈ

    private void Awake()
    {
        // ���� ��ġ���� ������ �޾ƿ� �������ش�.

        GameObject campFireSpawnObj = GameObject.FindGameObjectWithTag("CampFireSpawnPos");
        GameObject legionSpawnObj = GameObject.FindGameObjectWithTag("LegionSpawnPos");

        Transform[] campFireSpawnPos = campFireSpawnObj.GetComponentsInChildren<Transform>();
        Transform[] legionSpawnPos = legionSpawnObj.GetComponentsInChildren<Transform>();

        _campFireSpawnPoints = new Transform[campFireSpawnPos.Length];
        _legionSpawnPoints = new Transform[legionSpawnPos.Length];

        for (int i = 0; i < _legionSpawnPoints.Length; i++)
        {
            _legionSpawnPoints[i] = legionSpawnPos[i];
        }

        for (int i = 0; i < _campFireSpawnPoints.Length; i++)
        {
            _campFireSpawnPoints[i] = campFireSpawnPos[i];
        }
    }

    public void SpawnPenguinHandler(Penguin penguin) 
    {
        campFireidx++; // ���� ��ġ�� ���� idx
        Penguin spawnPenguin =  SpawnPenguin(penguin, _campFireSpawnPoints[campFireidx]) as Penguin;  //�Ű������� �޾ƿ� Penguin�� �����Ѵ�
        _curPTspawnPenguins.Add(spawnPenguin); // ����Ʈ�� �߰�
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnIceArrivedEvent += ResetPTInfo;
        WaveManager.Instance.OnPhaseStartEvent += SetCurPTspawnPenguins;
    }

    public void OnDisable()
    {
        WaveManager.Instance.OnIceArrivedEvent -= ResetPTInfo;
        WaveManager.Instance.OnPhaseStartEvent -= SetCurPTspawnPenguins;
    }

    private void ResetPTInfo()
    {
        campFireidx = 0;
        _curPTspawnPenguins.Clear();
    }

    private void SetCurPTspawnPenguins() // �̹� �غ�ð��� ������ ��ϵ��� WaveManager�� �Ѱ��ش�.
    {
        WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override Entity Create(Penguin type, Transform spawnTrm) // ������ ����� �����Ǵ� �Լ�
    {
        string originalString = type.ToString();
        // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" ")); 

        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        return spawnPenguin;
    }
}
