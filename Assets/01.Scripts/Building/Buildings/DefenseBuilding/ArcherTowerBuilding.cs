using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTowerBuilding : BaseBuilding
{
    FieldOfView _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    ArcherPenguin[] _archerPenguins;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();
    }

    protected override void Running()
    {

    }
}
