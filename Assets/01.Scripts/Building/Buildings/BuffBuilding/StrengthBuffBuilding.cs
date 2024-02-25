using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class StrengthBuffBuilding : BuffBuilding
{
    private Collider[] _colls;
    private Collider[] previousColls = default;

    private Dictionary<int , Penguin> _inRangePenguins = new Dictionary<int , Penguin>();

    protected override void Awake()
    {
        base.Awake();
        SetBuffValue(DefaultBuffValue);
    }

    protected override void Running()
    {
        Collider[] newColls = Physics.OverlapSphere(transform.position, innerDistance, _targetLayer);

        if (previousColls == default || !IsSameColliders(previousColls, newColls))
        {
            previousColls = _colls;
            _colls = newColls;

            if (previousColls != null)
            {
                previousColls = BuffRunning(_colls, previousColls);
            }
        }
    }

    protected override void BuffEvent()
    {
        int curPreviousCollsLength = previousColls.Length;
        for (int i = 0; i < _colls.Length; i++)
        {
            GameObject obj = _colls[i].gameObject;
            int instanceID = obj.GetInstanceID();

            if (_inRangePenguins.ContainsKey(instanceID))
            {
                continue;
            }

            if (!_inRangePenguins.TryGetValue(instanceID, out Penguin penguin))
            {
                _inRangePenguins.Add(instanceID, _colls[i].GetComponent<Penguin>());
            }

            _inRangePenguins[instanceID].AddStat(GetBuffValue(), StatType.Strength, StatMode.Increase);
            //_colls[i].GetComponent<Penguin>().AddStat(GetBuffValue(), StatType.Strength, StatMode.Increase);
            Debug.Log($"{_inRangePenguins[instanceID].name}: increase");
        }
    }

    protected override void CommenceBuffDecay()
    {
        foreach (var key in _inRangePenguins.Keys)
        {
            bool found = false;
            for (int i = 0; i < _colls.Length; i++)
            {
                GameObject _collObj = _colls[i].gameObject;
                int _collObjInstanceID = _collObj.GetInstanceID();
                if (_collObjInstanceID == key)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                _inRangePenguins[key].AddStat(GetBuffValue(), StatType.Strength, StatMode.Decrease);
                Debug.Log($"{_inRangePenguins[key].name}: decrease");
                _inRangePenguins.Remove(key);
                break;
            }
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
