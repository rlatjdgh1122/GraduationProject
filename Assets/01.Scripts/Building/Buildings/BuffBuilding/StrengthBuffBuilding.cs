using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBuffBuilding : BuffBuilding
{
    protected Collider[] _colls;
    private int previousCollsLength = 0;

    protected override void Awake()
    {
        base.Awake();
        SetBuffValue(DefaultBuffValue);
    }

    protected override void Running()
    {
        _colls = Physics.OverlapSphere(transform.position, innerDistance, _targetLayer);
        previousCollsLength = BuffRunning(_colls, previousCollsLength);
    }

    protected override void BuffEvent()
    {
        for (int i = _colls.Length - 1; i >= previousCollsLength; i--)
        {
            _colls[i].GetComponent<Penguin>().AddStat(GetBuffValue(), StatType.Strength, StatMode.Increase);
        }
    }

    protected override void SetBuffValue(int value)
    {
        this.buffValue = value;
    }

    protected override int GetBuffValue()
    {
        return this.buffValue;
    }
}
