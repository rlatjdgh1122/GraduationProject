using System;
using System.Collections.Generic;
using UnityEngine;
using StatOperator;
[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;
    public List<int> increases; //���� % (������)
    public List<int> decreases; //���� % (�տ���)
    public int GetValue() //�տ������� ��
    {
        int plusValue = StatCalculator.MultiOperValue(_baseValue, increases);
        int minusValue = StatCalculator.SumOperValue(_baseValue, decreases);
        return plusValue - minusValue;
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
