using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RandomGlacierGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _glacierPrefab; // 풀링하기 귀찮아서 일단 이걸로 함

    private float baseAngle = 60f;
    private int makedHexagonCount = 0; // 만들어진 육각형의 개수
    private int curGroundCount = 3; // 처음에 3개 주고 시작하니까

    private int curHexagon_GlacierCount
    {
        get
        {
            return _curHexagon_Grounds.Count;
        }
    }

    private List<GroundMove> _curHexagon_Grounds = new List<GroundMove>(); // 나중에 Queue나 stack으로 할 수도

    private void SetRandomGlaciers()
    {
        
    }

    private void GenerateGlacier()
    {
        // 생성 하고 위치 다 설정
        for (int i = 0; i < GetCurHexagonGroundsGoalCount(); i++)
        {
            // 일단 풀링하기 귀찮아서 Instantiate으로 하겟음
            // 생각해보니까 이거 생성 말고 처음부터 있게 해야됨.
            // 부모 네브메시 해줘야 해서
            GroundMove ground = Instantiate(_glacierPrefab, transform.position, Quaternion.identity).GetComponent<GroundMove>();
            _curHexagon_Grounds.Add(ground);
        } 
    }


    private float GetCurAngleBetweenGlacier() // 현재 나올 빙하들 사이의 각도
    {
        return baseAngle / (curHexagon_GlacierCount + 1);
    }

    
    private int GetCurHexagonGroundsGoalCount()  //지금 만들 육각형에 필요한 빙하의 개수
    {
        if (makedHexagonCount == 0) { return 4; } //근데 처음에는 3개 깔린 상태로 시작하니까 4 반환
        return 6 * (int)Mathf.Pow(2, makedHexagonCount + 1);
    }

    private void Update()
    {
        // 만약에 GetCurHexagonGroundsGoalCount(지금 만들 육각형에 필요한 빙하의 갯수)만큼 다 왔다면
        // makedHexagonCount++ 하고
        // curHexagon_GroundsGoalCount 만큼 땅을 새로 만들어 주고
        // 랜덤으로 하나 보냄


        //생각할점: 땅을 생성할때 랜덤 순서를 정해줄 것 인가 or 땅을 일단 순서대로 생성하고 나갈때 랜덤한 놈이 나갈 것인가
    }
}
