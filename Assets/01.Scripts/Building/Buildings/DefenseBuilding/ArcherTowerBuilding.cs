using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerBuilding : BaseBuilding
{
    FieldOfView _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();
    }

    protected override void Running()
    {
        if(_visibleTargets.Count > 0)
        {
            Debug.Log("발견을했다????");
        }
    }
}
