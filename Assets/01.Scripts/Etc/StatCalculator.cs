using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OPERATION
{
    합연산, // 합연산
    곱연산 // 곱연산
}

public class StatCalculator : MonoBehaviour
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
        float sum = 0; // 실수로 변경
        if (operation == OPERATION.합연산)
        {
            foreach (var item in percent)
            {
                Debug.Log(Percent(item));
                sum += Percent(item) - stat; // 합연산 수정
            }
            fewtimes = 1 + sum / stat; // 기존 스탯 대비 몇 배 증가했는지 계산
            result = stat + sum;
        }
        else if (operation == OPERATION.곱연산)
        {
            sum = 1; // 곱셈 연산을 위해 초기값을 1로 설정
            foreach (var item in percent)
            {
                sum *= Percent(item) / 100f; // 실수 나눗셈으로 변경
            }
            fewtimes = sum; // 기존 스탯 대비 몇 배 증가했는지 계산
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