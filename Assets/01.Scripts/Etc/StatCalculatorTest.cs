using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
public enum OPERATION
{
    합연산, // 합연산
    곱연산 // 곱연산
}

public class StatCalculatorTest : MonoBehaviour
{
    public OPERATION operation;
    public int stat;
    public List<int> percent = new();
    public float fewtimes; // 기존보다 몇배 증가했는가
    public float result; // 최종 결과 값

    public float Percent(int per)
        => stat + (stat * (per / 100f)); // 실수 나눗셈으로 변경

    [ContextMenu("Result")]
    public void Result()
    {
        if (operation == OPERATION.합연산)
        {
            result = StatCalculator.SumOperValue(stat, percent);
            fewtimes = StatCalculator.SumOperTimes(stat, percent);
        }
        else if (operation == OPERATION.곱연산)
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