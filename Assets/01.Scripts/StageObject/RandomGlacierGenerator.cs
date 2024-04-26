using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGlacierGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _glacierPrefab; // 풀링하기 귀찮아서 일단 이걸로 함

    private float baseAngle = 60f;

    private int curHexagon_GlacierCount
    {
        get
        {
            return _curHexagon_Grounds.Count;
        }
    }

    private List<GameObject> _curHexagon_Grounds = new List<GameObject>();

    private void SetRandomGlaciers()
    {
        int curMaxGlacierCount = GetCurMaxGlacierCount();

        int randomIdx = Random.Range(0, _curHexagon_Grounds.Count);
        GameObject glacier = _curHexagon_Grounds[randomIdx]; // 이번에 갈 놈
        
        _curHexagon_Grounds.Remove(glacier);
    }

    private void GenerateGlacier()
    {
        // 생성 하고 위치 다 설정

        for (int i = 0; i < GetCurMaxGlacierCount() - curHexagon_GlacierCount; i++)
        {
            GameObject glacier = Instantiate(_glacierPrefab, transform); // 나중에 풀링으로 바꾸쇼
            // 위치 바꿔줘야 하고
            _curHexagon_Grounds.Add(glacier);
        }
    }


    private float GetCurAngleBetweenGlacier() // 현재 나올 빙하들 사이의 각도
    {
        return baseAngle / curHexagon_GlacierCount + 1;
    }

    private int GetCurMaxGlacierCount() // 현재 만들어야 할 육각형까지 필요한 빙하의 개수
    {
        return 6 * curHexagon_GlacierCount + 1;
    }
}
