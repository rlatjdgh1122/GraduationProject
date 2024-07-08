using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    private int makedHexagonCount = -1;

    private Queue<Ground> _curHexagon_Grounds = new Queue<Ground>();
    private Queue<Ground> _allGrounds = new Queue<Ground>(); // 나중에 Queue나 stack으로 할 수도

    private Queue<float> _rotateValues = new Queue<float>(); // 랜덤 회전 값들

    private Queue<float> RotateValues // 0이면 자동으로 채워주게 했음 이걸 쓰셈
    {
        get
        {
            if (_rotateValues.Count == 0)
            {
                AddGlacierToCurHexagon();
            }
            return _rotateValues;
        }
    }

    private GroundConfigurer _groundConfigurer;
    private RaftConfigurer _raftConfigurer;

    [SerializeField]
    private TutorialGroundInfoDataSO _tutorialGroundInfoDataSO;
    [SerializeField]
    private ComingObjIncreaseRateDataSO _comingObjIncreaseRateDataSO;

    private int curWave => WaveManager.Instance.CurrentWaveCount;

    private readonly string raftName = "Raft";

    private bool isTutorialWave => curWave < 10;

    [SerializeField]
    private int angle;

    private Ground SpawnGlaciers()
    {
        Ground ground = _allGrounds.Dequeue();
        return ground;
    }

    private void Awake()
    {
        _groundConfigurer = transform.Find("GroundConfigurer").GetComponent<GroundConfigurer>();
        _raftConfigurer = transform.Find("RaftConfigurer").GetComponent<RaftConfigurer>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += ResetRotateHashSet;
        SignalHub.OnBattlePhaseStartPriorityEvent += GenerateGlacier;
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
    }

    private void AddGlacierToCurHexagon()
    {
        _rotateValues.Clear();

        float[] rotateValueArray = new float[GetCurHexagonGroundsGoalCount(makedHexagonCount)];

        for (int i = 0; i < GetCurHexagonGroundsGoalCount(makedHexagonCount); i++)
        {
            rotateValueArray[i] = GetCurAngleBetweenGlacier(makedHexagonCount) * i;
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

    private void SetGround(int i)
    {
        //transform.localRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        Ground curGround = _curHexagon_Grounds.Dequeue();
        curGround.gameObject.SetActive(true);

        float rotateValue = RotateValues.Dequeue() + angle;
        Vector3 direction = Quaternion.Euler(0, rotateValue, 0) * -transform.forward;

        //Vector3 groundPos = new Vector3(transform.localPosition.x, transform.localPosition.y, );
        Vector3 groundPos = transform.localPosition + direction * _spawnDistance * (makedHexagonCount + 1);
        curGround.SetComingObjectInfo(_groundConfigurer.SetComingObjectElements(curGround.transform),
                                      transform,
                                      groundPos);

        curGround.ActivateGround();

        if(i  == 0) // 첫번째 빙하만 도착했을 때 raft생성하도록
        {
            curGround.GroundMove.GeneratBrokenGroundEvent += GenerateRaft;
        }
    }

    private void GenerateGlacier()
    {
        int groundCount = GetGroundCount();

        if (_curHexagon_Grounds.Count == 0)
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }

        for (int i = 0; i < groundCount; i++)
        {
            if (_curHexagon_Grounds.Count == 0)
            {
                Debug.LogError("빙하 오게 할때 현재 육각형까지 남은 수가 넘으면 안됨 생성 가능한 만큼만 하겠음");
                return;
            }

            SetGround(i);
        }
    }


    private void GenerateRaft()
    {
        int raftCount = GetRaftCount();

        for (int i = 0; i < raftCount; i++)
        {
            Raft raft = PoolManager.Instance.Pop(raftName) as Raft;

            float rotateValue = GetRandRotateValue();

            Vector3 direction = Quaternion.Euler(0, rotateValue, 0) * transform.forward;
            Vector3 raftPos = transform.localPosition + direction * _spawnDistance * (makedHexagonCount + 1);

            //raft.transform.position = randomRaftPos;

            raft.transform.rotation = Quaternion.identity;
            raft.SetMoveTarget(transform.parent.parent.parent);
            raft.SetComingObjectInfo(_raftConfigurer.SetComingObjectElements(raft.transform),
                                      transform,
                                      raftPos);
        }
    }

    private HashSet<float> _prevRotateValues = new();

    private float GetRandRotateValue()
    {
        float[] rotateValues = RotateValues.ToArray();
        float rotateValue = 0;

        // 현재 가능한 모든 회전 값이 이전 값 목록에 포함되어 있는지 확인
        if (_prevRotateValues.Count >= rotateValues.Length)
        {
            int nextHexagonGroundsGoalCount = GetCurHexagonGroundsGoalCount(makedHexagonCount + 1);

            rotateValues = new float[nextHexagonGroundsGoalCount];

            for (int i = 0; i < nextHexagonGroundsGoalCount; i++)
            {
                float curAngleBetweenGlacier = GetCurAngleBetweenGlacier(makedHexagonCount + 1);
                rotateValues[i] = curAngleBetweenGlacier * i; // 다음 RotateValues로
            }
        }

        // 그전에 썼던거 다시 안 쓰도록
        do
        {
            rotateValue = rotateValues[UnityEngine.Random.Range(0, rotateValues.Length)];
        } while (_prevRotateValues.Contains(rotateValue));

        Debug.Log(rotateValue);

        _prevRotateValues.Add(rotateValue);
        return rotateValue;
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

    private float GetCurAngleBetweenGlacier(int _makedHexagonCount) // 현재 나올 빙하들 사이의 각도
    {
        if (_makedHexagonCount == 0) { return 60; } // 근데 처음에는 3개 깔린 상태로 시작하니까 60 반환
        return 360 / GetCurHexagonGroundsGoalCount(_makedHexagonCount);
    }
    
    private int GetCurHexagonGroundsGoalCount(int _makedHexagonCount)  // 지금 만들 육각형에 필요한 빙하의 개수
    {
        // 나중에 랜덤으로 여러 빙하 오게 할때 현재 육각형까지 남은 수가 넘으면 안됨 <- 이거 해야됨

        if (_makedHexagonCount == 0) { return 4; } // 근데 처음에는 3개 깔린 상태로 시작하니까 4 반환
        return 6 * (int)Mathf.Pow(2, _makedHexagonCount);
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
            int groundCountValue = Mathf.CeilToInt(curWave * _comingObjIncreaseRateDataSO.GroundIncreaseRate);
            int maxGroundCount = Mathf.Clamp(groundCountValue, 1, _curHexagon_Grounds.Count);
            groundCount = UnityEngine.Random.Range(1, maxGroundCount);
        }

        return groundCount;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= ResetRotateHashSet;
        SignalHub.OnBattlePhaseStartPriorityEvent -= GenerateGlacier;
    }

    private void ResetRotateHashSet()
    {
        _prevRotateValues.Clear();
    }
}
