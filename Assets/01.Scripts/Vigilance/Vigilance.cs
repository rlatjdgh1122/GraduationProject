using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vigilance : MonoBehaviour
{
    [SerializeField] List<Ability> _abilities = new();

    protected TargetObject _target = null;

    public virtual void SetTarget(TargetObject target)
    {
        _target = target;
    }

    public virtual void OnVigilance()
    {
        _target.AddStat(_abilities);
    }

    public virtual void InitVigilance()
    {
        _target.RemoveStat(_abilities);
    }
}