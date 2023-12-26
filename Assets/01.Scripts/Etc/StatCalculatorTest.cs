using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
public enum OPERATION
{
    �տ���, // �տ���
    ������ // ������
}

public class StatCalculatorTest : MonoBehaviour
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
        if (operation == OPERATION.�տ���)
        {
            result = StatCalculator.SumOperValue(stat, percent);
            fewtimes = StatCalculator.SumOperTimes(stat, percent);
        }
        else if (operation == OPERATION.������)
        {
            result = StatCalculator.MultiOperValue(stat, percent);
            fewtimes = StatCalculator.MultiOperTimes(stat, percent);
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