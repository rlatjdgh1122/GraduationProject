using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OPERATION
{
    �տ���, // �տ���
    ������ // ������
}

public class StatCalculator : MonoBehaviour
{
    public OPERATION operation;
    public int stat;
    public List<int> percent = new();
    public float fewtimes; // �������� ��� �����ߴ°�
    public float result; // ���� ��� ��

    public float Percent(int per)
        => stat + (stat * (per / 100f)); // �Ǽ� ���������� ����

    [ContextMenu("Result")]
    public void Result()
    {
        float sum = 0; // �Ǽ��� ����
        if (operation == OPERATION.�տ���)
        {
            foreach (var item in percent)
            {
                Debug.Log(Percent(item));
                sum += Percent(item) - stat; // �տ��� ����
            }
            fewtimes = 1 + sum / stat; // ���� ���� ��� �� �� �����ߴ��� ���
            result = stat + sum;
        }
        else if (operation == OPERATION.������)
        {
            sum = 1; // ���� ������ ���� �ʱⰪ�� 1�� ����
            foreach (var item in percent)
            {
                sum *= Percent(item) / 100f; // �Ǽ� ���������� ����
            }
            fewtimes = sum; // ���� ���� ��� �� �� �����ߴ��� ���
            result = stat * sum;
        }
    }

    private void Reset()
    {
        stat = 0;
        result = 0;
        fewtimes = 0;
        percent.Clear();
    }
}