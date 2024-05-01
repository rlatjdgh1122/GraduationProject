using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class RandomGlacierGenerator : MonoBehaviour
{
    // 빙하와 육각형은 다른 것이다. 빙하들이 모여서 육각형을 만드는 거임

    [SerializeField]
    private GameObject _glacierPrefab; // 빙하 프리펩

    [SerializeField]
    private Transform _secondGroundParent; // 빙하 2개 올 때 쓸 것

    private float baseAngle = 60f;
    private int makedHexagonCount = -1; // 만들어진 육각형의 개수

    private Queue<GroundMove> _curHexagon_Grounds = new Queue<GroundMove>(); // 나중에 Queue나 stack으로 할 수도
    private Queue<GroundMove> _allGrounds = new Queue<GroundMove>(); // 나중에 Queue나 stack으로 할 수도

    private float randomDistance => Random.Range(10, 30f); // 이 수치는 나중에 적당히 찾아서 넣어주자

    private GroundMove SpawnGlaciers()
    {
        GroundMove ground = _allGrounds.Dequeue();
        return ground;
    }

    private void Start()
    {
        for(int i = 0; i < 50; i++)
        {
            GroundMove ground = Instantiate(_glacierPrefab, transform.position, Quaternion.identity)
                .transform.GetChild(0).GetChild(0).GetComponent<GroundMove>(); // 반드시 바꿀 것
            _allGrounds.Enqueue(ground);
            ground.gameObject.SetActive(false);
        }
        Debug.Log(_allGrounds.Count);
    }

    private void AddGlacierToCurHexagon()
    {
        for (int i = 0; i < GetCurHexagonGroundsGoalCount(); i++)
        {
            GroundMove ground = SpawnGlaciers();
            _curHexagon_Grounds.Enqueue(ground);
        }
    }

    private void GenarateGlacier()
    {
        // position는 randomDistance로 랜덤한 거리에 있는 위치로
        // rotation은 GetCurAngleBetweenGlacier으로 조절
        GroundMove curground = _curHexagon_Grounds.Dequeue();

        Vector3 roataeVec = new Vector3(0,  0f);
        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(roataeVec);
        curground.transform.SetParent(transform);
        curground.transform.localPosition = new Vector3(0f, 0f, 10f);

        transform.Rotate(Vector3.up * GetCurAngleBetweenGlacier());
        curground.transform.SetParent(null);
    }


    private float GetCurAngleBetweenGlacier() // 현재 나올 빙하들 사이의 각도
    {
        if (makedHexagonCount == 0) { return 60; } // 근데 처음에는 3개 깔린 상태로 시작하니까 60 반환
        return 360 / _curHexagon_Grounds.Count;
    }

    
    private int GetCurHexagonGroundsGoalCount()  // 지금 만들 육각형에 필요한 빙하의 개수
    {
        if (makedHexagonCount == 0) { return 4; } // 근데 처음에는 3개 깔린 상태로 시작하니까 4 반환
        return 6 * (int)Mathf.Pow(2, makedHexagonCount + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // 디버그 용
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }
        if (Input.GetKeyDown(KeyCode.J)) // 디버그 용
        {
            GenarateGlacier();
        }
        // 만약에 GetCurHexagonGroundsGoalCount(지금 만들 육각형에 필요한 빙하의 갯수)만큼 다 왔다면
        // makedHexagonCount++ 하고
        // curHexagon_GroundsGoalCount 만큼 땅을 새로 만들어 주고
        // 랜덤으로 하나 보냄


        // 생각할점: 땅을 생성할때 랜덤 순서를 정해줄 것 인가 or 땅을 일단 순서대로 생성하고 나갈때 랜덤한 놈이 나갈 것인가
    }
}
