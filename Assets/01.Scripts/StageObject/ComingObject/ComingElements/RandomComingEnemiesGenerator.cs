using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomComingEnemiesGenerator : MonoBehaviour  
{
    // ���Ͽ� �������� �ٸ� ���̴�. ���ϵ��� �𿩼� �������� ����� ����

    [SerializeField]
    private GameObject _glacierPrefab; // ���� ������
    [SerializeField]
    private float _spawnDistance;

    private int makedHexagonCount = -1; // ������� �������� ����

    private Queue<Ground> _curHexagon_Grounds = new Queue<Ground>(); // ���߿� Queue�� stack���� �� ����
    private Queue<Ground> _allGrounds = new Queue<Ground>(); // ���߿� Queue�� stack���� �� ����

    private Queue<float> _rotateValues = new Queue<float>(); // ���� ȸ�� ����

    private GroundConfigurer _groundConfigurer;
    private RaftConfigurer _raftConfigurer;

    [SerializeField]
    private TutorialGroundInfoDataSO _tutorialGroundInfoDataSO;

    private int curWave => WaveManager.Instance.CurrentWaveCount;

    private readonly string raftName = "Raft";

    private Ground SpawnGlaciers()
    {
        Ground ground = _allGrounds.Dequeue();
        return ground;
    }

    private void Awake()
    {
        _groundConfigurer = transform.Find("GroundConfigurer").GetComponent<GroundConfigurer>();
        _raftConfigurer = transform.Find("RaftConfigurer").GetComponent<RaftConfigurer>();
            
        SignalHub.OnGroundArrivedEvent += TutorialGenerateRaft;
    }

    private void Start()
    {
        for(int i = 0; i < 50; i++)
        {
            Ground ground = Instantiate(_glacierPrefab, transform.position, Quaternion.identity)
                .GetComponent<Ground>();

            _allGrounds.Enqueue(ground);

            ground.SetMoveTarget(transform.parent.parent.parent);

            ground.gameObject.SetActive(false); 
        }

        SignalHub.OnBattlePhaseStartPriorityEvent += GenerateGlacier;
    }

    private void AddGlacierToCurHexagon()
    {
        _rotateValues.Clear();

        float[] rotateValueArray = new float[GetCurHexagonGroundsGoalCount()];

        for (int i = 0; i < GetCurHexagonGroundsGoalCount(); i++)
        {
            rotateValueArray[i] = GetCurAngleBetweenGlacier() * i;
            Ground ground = SpawnGlaciers();
            _curHexagon_Grounds.Enqueue(ground);
        }

        System.Random rnd = new System.Random();            // ���� �� �ִ� ������ �迭�� �������� ����
        rotateValueArray = rotateValueArray.OrderBy(x => rnd.Next()).ToArray();

        for(int i = 0; i < rotateValueArray.Length; i++)
        {
            _rotateValues.Enqueue(rotateValueArray[i]);
        }
    }

    private void GlacierSetPos()
    {
        // ���߿� �������� ���� ���� ���� �Ҷ� ���� ���������� ���� ���� ������ �ȵ�
        Ground curground = _curHexagon_Grounds.Dequeue();
        curground.ActivateGround();

        float rotateValue = _rotateValues.Dequeue();
        transform.Rotate(Vector3.up * rotateValue);

        curground.gameObject.SetActive(true);

        Vector3 groundPos = new Vector3(transform.localPosition.x, 0f, _spawnDistance * (makedHexagonCount + 1));

        curground.SetComingObjectInfo(transform,
                                      groundPos,
                                      _groundConfigurer.SetComingObjectElements(curground.transform));

        transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f); // ������ �����ϸ� �Ƹ� �̰� �����ϰ�. ��ٷ���.
    }

    private float GetCurAngleBetweenGlacier() // ���� ���� ���ϵ� ������ ����
    {
        if (makedHexagonCount == 0) { return 60; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 60 ��ȯ
        return 360 / GetCurHexagonGroundsGoalCount();
    }

    
    private int GetCurHexagonGroundsGoalCount()  // ���� ���� �������� �ʿ��� ������ ����
    {
        if (makedHexagonCount == 0) { return 4; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 4 ��ȯ
        return 6 * (int)Mathf.Pow(2, makedHexagonCount);
    }

    private void GenerateGlacier()
    {
        if (_curHexagon_Grounds.Count == 0)
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }

        if(curWave < 10) // Ʃ�丮���̸� ���������
        {
            TutorialGlacierSetPos();
        }
        else // �ƴϸ� ��������
        {
            GlacierSetPos();
            //GenerateRaft(); // �ϴ� �� �� �Ұ�
        }
    }

    private void TutorialGenerateRaft() // ���߿� �� �ϳ��� ����
    {
        for (int i = 0; i < _tutorialGroundInfoDataSO.TutorialComingEnemies[curWave - 1].ComingRaftCount; i++)
        {
            Raft raft = PoolManager.Instance.Pop(raftName) as Raft;
            Vector3 randomRaftPos = UnityEngine.Random.insideUnitCircle.normalized * 80;
            float raftZ = randomRaftPos.y;
            randomRaftPos.z = raftZ;
            randomRaftPos.y = 0.7f;

            raft.transform.position = randomRaftPos;

            raft.transform.rotation = Quaternion.identity;
            raft.SetMoveTarget(transform.parent.parent.parent);
            raft.SetComingObjectInfo(transform,
                                     randomRaftPos,
                                     _raftConfigurer.SetComingObjectElements(raft.transform));
        }
    }

    private void TutorialGlacierSetPos()
    {
        for (int i = 0; i < _tutorialGroundInfoDataSO.TutorialComingEnemies[curWave - 1].ComingGroundsCount; i++)
        {
            GlacierSetPos();
        }
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartPriorityEvent -= GenerateGlacier;
        SignalHub.OnGroundArrivedEvent -= TutorialGenerateRaft;
    }
}
