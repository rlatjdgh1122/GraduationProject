using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomGroundGenerator : MonoBehaviour
{
    // ���Ͽ� �������� �ٸ� ���̴�. ���ϵ��� �𿩼� �������� ����� ����

    [SerializeField]
    private GameObject _glacierPrefab; // ���� ������
    [SerializeField]
    private float _spawnDistance;

    private int makedHexagonCount = -1; // ������� �������� ����

    private Queue<Ground> _curHexagon_Grounds = new Queue<Ground>(); // ���߿� Queue�� stack���� �� ����
    private Queue<Ground> _allGrounds = new Queue<Ground>(); // ���߿� Queue�� stack���� �� ����

    private Queue<float> _rotateValues = new Queue<float>(); // ���� ȸ�� ����

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

        var array = _rotateValues.ToArray();                // ���� �� �ִ� �������� Queue�� �־�ΰ� �������� ����
        System.Random rnd = new System.Random();            // ���� �� �ִ� �������� Queue�� �־�ΰ� �������� ����
        array = array.OrderBy(x => rnd.Next()).ToArray();   // ���� �� �ִ� �������� Queue�� �־�ΰ� �������� ����
        _rotateValues = new Queue<float>(array);            // ���� �� �ִ� �������� Queue�� �־�ΰ� �������� ����
    }

    private void GlacierSetPos()
    {
        // position�� randomDistance�� ������ �Ÿ��� �ִ� ��ġ��
        // rotation�� GetCurAngleBetweenGlacier���� ����
        int realMakedHexagonCount = makedHexagonCount + 1;

        Ground curground = _curHexagon_Grounds.Dequeue();

        float rotateValue = _rotateValues.Dequeue();
        transform.Rotate(Vector3.up * rotateValue);

        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(transform, new Vector3(transform.localPosition.x, 0f, _spawnDistance * realMakedHexagonCount));

        CoroutineUtil.CallWaitForOneFrame(() => // SetGroundInfo �ϰ� ���� �ؾ� �ϴ� 1������ ��ٸ��� �Ѵ�.
        {
            transform.rotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);
        });
    }

    private float GetCurAngleBetweenGlacier() // ���� ���� ���ϵ� ������ ����
    {
        if (makedHexagonCount == 0) { return 60; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 60 ��ȯ
        return 360 / GetCurHexagonGroundsGoalCount();
    }

    
    private int GetCurHexagonGroundsGoalCount()  // ���� ���� �������� �ʿ��� ������ ����
    {
        if (makedHexagonCount == 0) { return 4; } // �ٵ� ó������ 3�� �� ���·� �����ϴϱ� 4 ��ȯ
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
