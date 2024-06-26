using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityActionData : MonoBehaviour
{
    [HideInInspector] public Vector3 HitPoint;
    [HideInInspector] public Vector3 HitNormal;
    [HideInInspector] public HitType HitType;

    //³ª ¶§¸°³ð
    public TargetObject HitTarget;

    private int _hitCount = 0;
    public int HitCount
    {
        get => _hitCount;
        set
        {
            _hitCount = value;
            OnHitCountUpdated?.Invoke(_hitCount);
        }
    }

    public OnValueUpdated<int> OnHitCountUpdated = null;
}
