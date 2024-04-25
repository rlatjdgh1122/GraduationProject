using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGlacierGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _glacierPrefab; // Ǯ���ϱ� �����Ƽ� �ϴ� �̰ɷ� ��

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
        GameObject glacier = _curHexagon_Grounds[randomIdx]; // �̹��� �� ��
        
        _curHexagon_Grounds.Remove(glacier);
    }

    private void GenerateGlacier()
    {
        // ���� �ϰ� ��ġ �� ����

        for (int i = 0; i < GetCurMaxGlacierCount() - curHexagon_GlacierCount; i++)
        {
            GameObject glacier = Instantiate(_glacierPrefab, transform); // ���߿� Ǯ������ �ٲټ�
            // ��ġ �ٲ���� �ϰ�
            _curHexagon_Grounds.Add(glacier);
        }
    }


    private float GetCurAngleBetweenGlacier() // ���� ���� ���ϵ� ������ ����
    {
        return baseAngle / curHexagon_GlacierCount + 1;
    }

    private int GetCurMaxGlacierCount() // ���� ������ �� ���������� �ʿ��� ������ ����
    {
        return 6 * curHexagon_GlacierCount + 1;
    }
}
