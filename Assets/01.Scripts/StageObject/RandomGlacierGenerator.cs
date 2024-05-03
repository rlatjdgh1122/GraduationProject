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
    // ���Ͽ� �������� �ٸ� ���̴�. ���ϵ��� �𿩼� �������� ����� ����

    [SerializeField]
    private GameObject _glacierPrefab; // ���� ������

    [SerializeField]
    private Transform _secondGroundParent; // ���� 2�� �� �� �� ��

    private float baseAngle = 60f;
    private int makedHexagonCount = -1; // ������� �������� ����

    private Queue<GroundMove> _curHexagon_Grounds = new Queue<GroundMove>(); // ���߿� Queue�� stack���� �� ����
    private Queue<GroundMove> _allGrounds = new Queue<GroundMove>(); // ���߿� Queue�� stack���� �� ����

    private Queue<float> _rotateValues = new Queue<float>(); // ���� ȸ�� ����

    private readonly float glacierDiameter = 10.400405f;


    // �輺ȣ: ���� ��¼��� �ϱ�

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


        GroundMove curground = _curHexagon_Grounds.Dequeue();

        Vector3 roataeVec = new Vector3(0,  0f);
        curground.gameObject.SetActive(true);

        curground.SetGroundInfo(roataeVec);
        curground.transform.SetParent(transform);
        curground.transform.localPosition = new Vector3(0f, 0f, 50f * realMakedHexagonCount); // �ϴ��� ���� ������ ����ؼ� �ָ��� ��������

        float rotateValue = _rotateValues.Dequeue();
        transform.Rotate(Vector3.up * rotateValue);
        curground.transform.rotation = Quaternion.identity;

        curground.transform.SetParent(null);


        GameObject obj = new GameObject();
        obj.transform.forward = curground.transform.position;
        obj.transform.position = transform.forward * glacierDiameter * realMakedHexagonCount; // �ҹ���
         
        transform.rotation = Quaternion.Euler(0.0f, 30.0f, 0.0f);
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

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.K)) // ����� ��
        {
            makedHexagonCount++;
            AddGlacierToCurHexagon();
        }
        if (Input.GetKeyDown(KeyCode.J)) // ����� ��
        {
            GlacierSetPos();
        }
    }
}
