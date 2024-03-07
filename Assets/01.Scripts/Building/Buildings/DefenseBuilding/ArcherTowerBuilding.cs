using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTowerBuilding : BaseBuilding
{
    FieldOfView _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    ArcherTowerPenguin[] _archerPenguins;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();

        _archerPenguins = transform.GetComponentsInChildren<ArcherTowerPenguin>();

        for(int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].gameObject.SetActive(false);
        }
    }

    protected override void Running()
    {

    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].gameObject.SetActive(true);
        }
    }
}
