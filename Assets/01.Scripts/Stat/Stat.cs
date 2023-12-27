using System;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue; //기본 스탯
    [SerializeField] private float _fewTimes; //기본 스탯 기준 몇배인지 (UI용)
    public List<int> increases; //증가 % (곱연산)
    public List<int> decreases; //감소 % (합연산)
    public int GetValue() //합연산으로 들어감
    {
        var plusValue = StatCalculator.MultiOperValue(_baseValue, increases);
        var minusValue = StatCalculator.SumOperValue(plusValue, decreases);

        var result = StatCalculator.GetValue(plusValue, minusValue);

        _fewTimes = StatCalculator.OperTimes(result, _baseValue);
        return result;
    }

    public void AddModifier(int value)
    {
        if (value != 0)
            increases.Add(value);
    }

    public void RemoveModifier(int value)
    {
        if (value != 0)
            increases.Remove(value);
    }

    public void SetDefaultValue(int value)
    {
        _baseValue = value;
    }
}
