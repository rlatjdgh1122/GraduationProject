using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class RandomGlacierGenerator : MonoBehaviour
{
    // ���Ͽ� �������� �ٸ� ���̴�. ���ϵ��� �𿩼� �������� ����� ����

    [SerializeField]
    private GameObject _glacierPrefab; // ���� ������

    [SerializeField]
    private Transform _secondGroundParent; // ���� 2�� �� �� �� ��

    private float baseAngle = 60f;
    private int makedHexagonCount = -1; // ������� �������� ����

    private Queue<GroundMove> _curHexagon_Grounds = new Queue<GroundMove>(); // ���߿� Queue�� stack���� �� ����
    private Queue<GroundMove> _allGrounds = new Queue<GroundMove>(); // ���߿� Queue�� stack���� �� ����

    private float randomDistance => Random.Range(10, 30f); // �� ��ġ�� ���߿� ������ ã�Ƽ� �־�����

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
                .transform.GetChild(0).GetChild(0).GetComponent<GroundMove>(); // �ݵ�� �ٲ� ��
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
        // position�� randomDistance�� ������ �Ÿ��� �ִ� ��ġ��
        // rotation�� GetCurAngleBetweenGlacier���� ����
        GroundMove curground = _curHexagon_Grounds.Dequeue();

        Vector3 roataeVec = new Vector3(0,  0f);
        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(roataeVec);
        curground.transform.SetParent(transform);
        curground.transform.localPosition = new Vector3(0f, 0f, 10f);

        transform.Rotate(Vector3.up * GetCurAngleBetweenGlacier());
        curground.transform.SetParent(null);
    }


    private float GetCurAngleBetweenGlacier() // ���� ���� ���ϵ� ������ ����
    {
        if (makedHexagonCount == 0) { return 60; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 60 ��ȯ
        return 360 / _curHexagon_Grounds.Count;
    }

    
    private int GetCurHexagonGroundsGoalCount()  // ���� ���� �������� �ʿ��� ������ ����
    {
        if (makedHexagonCount == 0) { return 4; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 4 ��ȯ
        return 6 * (int)Mathf.Pow(2, makedHexagonCount + 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) // ����� ��
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }
        if (Input.GetKeyDown(KeyCode.J)) // ����� ��
        {
            GenarateGlacier();
        }
        // ���࿡ GetCurHexagonGroundsGoalCount(���� ���� �������� �ʿ��� ������ ����)��ŭ �� �Դٸ�
        // makedHexagonCount++ �ϰ�
        // curHexagon_GroundsGoalCount ��ŭ ���� ���� ����� �ְ�
        // �������� �ϳ� ����


        // ��������: ���� �����Ҷ� ���� ������ ������ �� �ΰ� or ���� �ϴ� ������� �����ϰ� ������ ������ ���� ���� ���ΰ�
    }
}
