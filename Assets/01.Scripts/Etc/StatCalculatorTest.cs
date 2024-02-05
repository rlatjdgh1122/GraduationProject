using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
public class StatCalculatorTest : MonoBehaviour
{
    public int stat;
    public List<int> increases = new();
    public List<int> decreases = new();
    [ReadOnly] public float fewtimes; // �������� ��� �����ߴ°�
    [ReadOnly] public float result; // ���� ��� ��

    [ContextMenu("Result")]
    public void Result()
    {
        int plusValue = StatCalculator.MultiOperValue(stat, increases);
        int minusValue = StatCalculator.SumOperValue(plusValue, decreases);
        result = StatCalculator.GetValue(plusValue, minusValue);
        fewtimes = StatCalculator.OperTimes(result, stat);
    }

    private void Reset()
    {
        stat = 0;
        result = 0;
        fewtimes = 0;
        increases.Clear();
        decreases.Clear();
    }
}