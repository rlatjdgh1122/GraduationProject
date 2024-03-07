using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArcherTowerBuilding : BaseBuilding
{
    FieldOfView _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    ArcherTowerPenguin[] _archerPenguins;

    private MeshRenderer _archerAttackRangeMesh;
    private Material _rangeMat;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();

        _archerPenguins = transform.GetComponentsInChildren<ArcherTowerPenguin>();
        _archerAttackRangeMesh = transform.Find("AttackRange/Slinder").GetComponent<MeshRenderer>();
        _rangeMat = _archerAttackRangeMesh.material;

        _archerAttackRangeMesh.gameObject.SetActive(false);

        SetUpPenguinsCompo(_fov.ViewRadius, true);
    }

    protected override void Running()
    {

    }

    private void SetUpPenguinsCompo(float attackDistance, bool isFirst = false)
    {
        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].attackDistance = attackDistance;

            if (isFirst)
            {
                _archerPenguins[i].gameObject.SetActive(false);
            }
        }
    }

    public override void SetSelect()
    {
        base.SetSelect();

        _archerAttackRangeMesh.gameObject.SetActive(true);
        _archerAttackRangeMesh.material = _rangeMat;
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        for (int i = 0; i < _archerPenguins.Length; i++)
        {
            _archerPenguins[i].gameObject.SetActive(true);
        }

        _archerAttackRangeMesh.gameObject.SetActive(false);
    }
}
