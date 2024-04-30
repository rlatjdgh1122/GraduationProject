using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class RandomGlacierGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _glacierPrefab; // Ǯ���ϱ� �����Ƽ� �ϴ� �̰ɷ� ��

    private float baseAngle = 60f;
    private int makedHexagonCount = 0; // ������� �������� ����
    private int curGroundCount = 3; // ó���� 3�� �ְ� �����ϴϱ�

    private int curHexagon_GlacierCount
    {
        get
        {
            return _curHexagon_Grounds.Count;
        }
    }

    private List<GroundMove> _curHexagon_Grounds = new List<GroundMove>(); // ���߿� Queue�� stack���� �� ����

    private void SetRandomGlaciers()
    {
        
    }

    private void GenerateGlacier()
    {
        // ���� �ϰ� ��ġ �� ����
        for (int i = 0; i < GetCurHexagonGroundsGoalCount(); i++)
        {
            // �ϴ� Ǯ���ϱ� �����Ƽ� Instantiate���� �ϰ���
            // �����غ��ϱ� �̰� ���� ���� ó������ �ְ� �ؾߵ�.
            // �θ� �׺�޽� ����� �ؼ�
            GroundMove ground = Instantiate(_glacierPrefab, transform.position, Quaternion.identity).GetComponent<GroundMove>();
            _curHexagon_Grounds.Add(ground);
        } 
    }


    private float GetCurAngleBetweenGlacier() // ���� ���� ���ϵ� ������ ����
    {
        return baseAngle / (curHexagon_GlacierCount + 1);
    }

    
    private int GetCurHexagonGroundsGoalCount()  //���� ���� �������� �ʿ��� ������ ����
    {
        if (makedHexagonCount == 0) { return 4; } //�ٵ� ó������ 3�� �� ���·� �����ϴϱ� 4 ��ȯ
        return 6 * (int)Mathf.Pow(2, makedHexagonCount + 1);
    }

    private void Update()
    {
        // ���࿡ GetCurHexagonGroundsGoalCount(���� ���� �������� �ʿ��� ������ ����)��ŭ �� �Դٸ�
        // makedHexagonCount++ �ϰ�
        // curHexagon_GroundsGoalCount ��ŭ ���� ���� ����� �ְ�
        // �������� �ϳ� ����


        //��������: ���� �����Ҷ� ���� ������ ������ �� �ΰ� or ���� �ϴ� ������� �����ϰ� ������ ������ ���� ���� ���ΰ�
    }
}
