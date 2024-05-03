using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Queue<float> _rotateValues = new Queue<float>(); // 랜덤 회전 값들

    private readonly float glacierDiameter = 10.400405f;


    // 김성호: 비율 어쩌고로 하기

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
                .GetComponent<GroundMove>();
            _allGrounds.Enqueue(ground);

            ground.SetMoveDir(transform);

            ground.gameObject.SetActive(false); 
        }
    }

    private void AddGlacierToCurHexagon()
    {
        _rotateValues.Clear();

        for (int i = 1; i <= GetCurHexagonGroundsGoalCount(); i++)
        {
            _rotateValues.Enqueue(GetCurAngleBetweenGlacier() * i);
            GroundMove ground = SpawnGlaciers();
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


        GroundMove curground = _curHexagon_Grounds.Dequeue();

        Vector3 roataeVec = new Vector3(0,  0f);
        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(roataeVec);
        curground.transform.SetParent(transform);
        curground.transform.localPosition = new Vector3(0f, 0f, 50f * realMakedHexagonCount); // 일단은 만든 육각형 비례해서 멀리서 나오도록

        float rotateValue = _rotateValues.Dequeue();
        transform.Rotate(Vector3.up * rotateValue);
        curground.transform.rotation = Quaternion.identity;

        curground.transform.SetParent(null);


        GameObject obj = new GameObject();
        obj.transform.forward = curground.transform.position;
        obj.transform.position = transform.forward * glacierDiameter * realMakedHexagonCount; // 소발임
         
        transform.rotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);
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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K)) // 디버그 용
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }
        if (Input.GetKeyDown(KeyCode.J)) // 디버그 용
        {
            GlacierSetPos();
        }
    }
}
