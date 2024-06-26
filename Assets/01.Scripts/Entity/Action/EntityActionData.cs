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

    private int _hitCount = 0; //¸ÂÀº È½¼ö
    private int _attackCount = 0; // ¶§¸° È½¼ö
    private int _killCount = 0; //Àû Á×ÀÎ È½¼ö

    public int HitCount
    {
        get => _hitCount;
        set
        {
            _hitCount = value;
            OnHitCountUpdated?.Invoke(_hitCount);
        }
    }

    public int AttackCount
    {
        get => _attackCount;
        set
        {
            _attackCount = value;
            OnAttackCountUpdated?.Invoke(_attackCount);
        }
    }

    public int KillCount
    {
        get => _killCount;
        set
        {
            _killCount = value;
            OnKillCountUpdated?.Invoke(_killCount);
        }
    }

    public OnValueUpdated<int> OnHitCountUpdated = null;
    public OnValueUpdated<int> OnAttackCountUpdated = null;
    public OnValueUpdated<int> OnKillCountUpdated = null;

    public int AddHitCount(int value = 1)
    {
        HitCount += value;

        return HitCount;
    }

    public int AddAttackCount(int value = 1)
    {
        AttackCount += value;

        return AttackCount;
    }
    public int AddKillCount(int value = 1)
    {
        KillCount += value;

        return KillCount;
    }
}
