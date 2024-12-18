using System;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;

[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue; //기본 스탯
    [ReadOnly] public float _fewTimes; //기본 스탯 기준 몇배인지 (인스펙터 용)
    [ReadOnly] public float _finalValue; //기본 스탯 기준 몇배인지 (인스펙터 용)

    public List<int> increases; //증가 % (곱연산)
    public List<int> decreases; //감소 % (합연산)

    /// <summary>
    /// 기존 벨류값 기준 몇 퍼센트가 되었는지
    /// </summary>
    /// <returns> 퍼센트</returns>
    public int GetStatPercent()
    {
        int result = 0;
        foreach (var item in increases)
        {
            result += item;
        }

        foreach (var item in decreases)
        {
            result -= item;
        }

        return result;
    }

    public int GetValue()
    {
        return Modify();
    }

    private int Modify()
    {
        var plusValue = StatCalculator.MultiOperValue(_baseValue, increases);
        var minusValue = StatCalculator.SumOperValue(plusValue, decreases);

        var result = StatCalculator.GetValue(plusValue, minusValue);

        _fewTimes = StatCalculator.OperTimes(result, _baseValue);
        _finalValue = result;

        return result;
    }

    public void AddSum(int value, int mul1, int mul2)
    {
        _baseValue = value + (mul1 * mul2);
    }

    public void AddIncrease(int value)
    {
        if (value != 0)
            increases.Add(value);

        Modify();
    }

    public void AddDecrease(int value)
    {
        if (value != 0)
            decreases.Add(value);

        Modify();
    }

    public void RemoveIncrease(int value)
    {   
        if (value != 0)
            increases.Remove(value);

        Modify();
    }

    public void RemoveDecrease(int value)
    {
        if (value != 0)
            decreases.Remove(value);

        Modify();
    }

    public void StatDecReset()
    {
        if (decreases.Count > 0)
        {
            decreases.Clear();
        }
    }

    public void StatIncReset()
    {
        if (increases.Count > 0)
        {
            increases.Clear();
        }
    }

    public void StatAllReset()
    {
        StatDecReset();
        StatIncReset();
    }

    public void SetDefaultValue(int value)
    {
        _baseValue = value;
    }
}
