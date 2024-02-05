using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
public class StatCalculatorTest : MonoBehaviour
{
    public int stat;
    public List<int> increases = new();
    public List<int> decreases = new();
    [ReadOnly] public float fewtimes; // 기존보다 몇배 증가했는가
    [ReadOnly] public float result; // 최종 결과 값

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