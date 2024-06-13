using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomComingEnemiesGenerator : MonoBehaviour
{
    // 빙하와 육각형은 다른 것이다. 빙하들이 모여서 육각형을 만드는 거임
    [SerializeField]
    private GameObject _glacierPrefab; // 빙하 프리펩
    [SerializeField]
    private float _spawnDistance;

    private int makedHexagonCount = -1; // 만들어진 육각형의 개수

    private Queue<Ground> _curHexagon_Grounds = new Queue<Ground>(); // 나중에 Queue나 stack으로 할 수도
    private Queue<Ground> _allGrounds = new Queue<Ground>(); // 나중에 Queue나 stack으로 할 수도

    private Queue<float> _rotateValues = new Queue<float>(); // 랜덤 회전 값들

    private GroundConfigurer _groundConfigurer;
    private RaftConfigurer _raftConfigurer;

    [SerializeField]
    private TutorialGroundInfoDataSO _tutorialGroundInfoDataSO;
    [SerializeField]
    private ComingObjIncreaseRateDataSO _comingObjIncreaseRateDataSO;

    private int curWave => WaveManager.Instance.CurrentWaveCount;

    private readonly string raftName = "Raft";

    private bool isTutorialWave => curWave < 10;

    private Ground SpawnGlaciers()
    {
        Ground ground = _allGrounds.Dequeue();
        return ground;
    }

    private void Awake()
    {
        _groundConfigurer = transform.Find("GroundConfigurer").GetComponent<GroundConfigurer>();
        _raftConfigurer = transform.Find("RaftConfigurer").GetComponent<RaftConfigurer>();

        SignalHub.OnGroundArrivedEvent += GenerateRaft;
    }

    private void Start()
    {
        for (int i = 0; i < 50; i++)
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

        System.Random rnd = new System.Random();            // 나올 수 있는 각도들 배열을 랜덤으로 셔플
        rotateValueArray = rotateValueArray.OrderBy(x => rnd.Next()).ToArray();

        for (int i = 0; i < rotateValueArray.Length; i++)
        {
            _rotateValues.Enqueue(rotateValueArray[i]);
        }
    }

    private void GlacierSetPos()
    {
        //transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

        // 나중에 랜덤으로 여러 빙하 오게 할때 현재 육각형까지 남은 수가 넘으면 안됨
        Ground curGround = _curHexagon_Grounds.Dequeue();
        curGround.gameObject.SetActive(true);

        float rotateValue = _rotateValues.Dequeue();
        // rotate가 제대로 되지 않음
        Debug.Log($"Before: {transform.rotation}");
        transform.Rotate(Vector3.up * rotateValue);
        Debug.Log($"RotateValue: {Vector3.up * rotateValue}");
        //Debug.Break();
        Debug.Log($"After: {transform.rotation}");

        Vector3 groundPos = new Vector3(transform.localPosition.x, transform.localPosition.y, _spawnDistance * (makedHexagonCount + 1));
        curGround.SetComingObjectInfo(_groundConfigurer.SetComingObjectElements(curGround.transform),
                                      transform,
                                      groundPos);
        curGround.ActivateGround();
    }

    private void GenerateGlacier()
    {
        if (_curHexagon_Grounds.Count == 0)
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }

        int groundCount = GetGroundCount();
        for (int i = 0; i < groundCount; i++)
        {
            GlacierSetPos();
        }

        //if (isTutorialWave) // 튜토리얼이면 정해져서 나오게
        //{
        //    for (int i = 0; i < _tutorialGroundInfoDataSO.TutorialComingEnemies[curWave - 1].ComingGroundsCount; i++)
        //    {
        //        GlacierSetPos();
        //    }
        //}
        //else // 아니면 랜덤으로 
        //{
        //    for(int i = 0; i < GetGroundCount(); i++)
        //    {
        //        GlacierSetPos();
        //    }
        //}
    }

    private void GenerateRaft()
    {
        int raftCount = GetRaftCount();
        for (int i = 0; i < raftCount; i++)
        {
            Raft raft = PoolManager.Instance.Pop(raftName) as Raft;
            Vector3 randomRaftPos = UnityEngine.Random.insideUnitCircle.normalized * 80;
            float raftZ = randomRaftPos.y;
            randomRaftPos.z = raftZ;
            randomRaftPos.y = 0.7f;

            //raft.transform.position = randomRaftPos;

            raft.transform.rotation = Quaternion.identity;
            raft.SetMoveTarget(transform.parent.parent.parent);
            raft.SetComingObjectInfo(_raftConfigurer.SetComingObjectElements(raft.transform),
                                      transform,
                                      randomRaftPos);
        }
    }

    private int GetRaftCount()
    {
        int raftCount = 0;
        if (isTutorialWave) // 튜토리얼이면 정해진대로
        {
            raftCount = _tutorialGroundInfoDataSO.TutorialComingEnemies[curWave - 1].ComingRaftCount;
        }
        else // 아니면 랜덤한 값 계산해서
        {
            raftCount = Mathf.Clamp(raftCount,0, Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.RaftIncreaseRate));
        }

        return raftCount;
    }

    private float GetCurAngleBetweenGlacier() // 현재 나올 빙하들 사이의 각도
    {
        if (makedHexagonCount == 0) { return 60; } // 근데 처음에는 3개 깔린 상태로 시작하니까 60 반환
        return 360 / GetCurHexagonGroundsGoalCount();
    }
    
    private int GetCurHexagonGroundsGoalCount()  // 지금 만들 육각형에 필요한 빙하의 개수
    {
        if (makedHexagonCount == 0) { return 4; } // 근데 처음에는 3개 깔린 상태로 시작하니까 4 반환
        return 6 * (int)Mathf.Pow(2, makedHexagonCount);
    }

    private int GetGroundCount()
    {
        int groundCount = 0;

        if (isTutorialWave) // 튜토리얼이면 정해진대로
        {
            groundCount = _tutorialGroundInfoDataSO.TutorialComingEnemies[curWave - 1].ComingGroundsCount;
        }
        else // 아니면 랜덤
        {
            int maxGroundCount = 0;
            maxGroundCount = Mathf.Clamp(maxGroundCount, 1, Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.GroundIncreaseRate));
            groundCount = UnityEngine.Random.Range(1, maxGroundCount);
        }

        return groundCount;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartPriorityEvent -= GenerateGlacier;
        SignalHub.OnGroundArrivedEvent -= GenerateRaft;
    }
}
