using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestGroup : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private TargetObject _target;

    public void SetTarget()
    {
        _entity.CurrentTarget = _target;
        _target.CurrentTarget = _entity;
    }
}
