using System;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
using Unity.VisualScripting;

[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue; //�⺻ ����
    [ReadOnly] public float _fewTimes; //�⺻ ���� ���� ������� (�ν����� ��)
    [ReadOnly] public float _finalValue; //�⺻ ���� ���� ������� (�ν����� ��)

    public List<int> increases; //���� % (������)
    public List<int> decreases; //���� % (�տ���)

    public int GetValue() //�տ������� ��
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
