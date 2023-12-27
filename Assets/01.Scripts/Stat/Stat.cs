using System;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue; //�⺻ ����
    [SerializeField] private float _fewTimes; //�⺻ ���� ���� ������� (UI��)
    public List<int> increases; //���� % (������)
    public List<int> decreases; //���� % (�տ���)
    public int GetValue() //�տ������� ��
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
