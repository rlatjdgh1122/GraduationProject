using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGroundGenerator : MonoBehaviour
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

    private Ground SpawnGlaciers()
    {
        Ground ground = _allGrounds.Dequeue();
        return ground;
    }

    private void Start()
    {
        for(int i = 0; i < 50; i++)
        {
            Ground ground = Instantiate(_glacierPrefab, transform.position, Quaternion.identity)
                .GetComponent<Ground>();
            _allGrounds.Enqueue(ground);

            ground.SetMoveTarget(transform.parent);

            ground.gameObject.SetActive(false); 
        }

        SignalHub.OnBattlePhaseStartPriorityEvent += GenerateGlacier;
    }

    private void AddGlacierToCurHexagon()
    {
        _rotateValues.Clear();

        for (int i = 1; i <= GetCurHexagonGroundsGoalCount(); i++)
        {
            _rotateValues.Enqueue(GetCurAngleBetweenGlacier() * i);
            Ground ground = SpawnGlaciers();
            ground.ActivateGround();
            _curHexagon_Grounds.Enqueue(ground);
        }

        var array = _rotateValues.ToArray();                // 나올 수 있는 각도들을 Queue에 넣어두고 랜덤으로 셔플
        System.Random rnd = new System.Random();            // 나올 수 있는 각도들을 Queue에 넣어두고 랜덤으로 셔플
        array = array.OrderBy(x => rnd.Next()).ToArray();   // 나올 수 있는 각도들을 Queue에 넣어두고 랜덤으로 셔플
        _rotateValues = new Queue<float>(array);            // 나올 수 있는 각도들을 Queue에 넣어두고 랜덤으로 셔플
    }

    private void GlacierSetPos()
    {
        // position는 randomDistance로 랜덤한 거리에 있는 위치로
        // rotation은 GetCurAngleBetweenGlacier으로 조절
        int realMakedHexagonCount = makedHexagonCount + 1;

        Ground curground = _curHexagon_Grounds.Dequeue();

        float rotateValue = _rotateValues.Dequeue();
        transform.Rotate(Vector3.up * rotateValue);

        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(transform, new Vector3(transform.localPosition.x, 0f, _spawnDistance * realMakedHexagonCount));

        CoroutineUtil.CallWaitForOneFrame(() => // SetGroundInfo 하고 나서 해야 하니 1프레임 기다리고 한다.
        {
            transform.rotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);
        });
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

    private void GenerateGlacier()
    {
        if (_curHexagon_Grounds.Count == 0)
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }

        GlacierSetPos();
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartPriorityEvent -= GenerateGlacier;
    }
}
