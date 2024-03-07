using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class DefenseBuilding : BaseBuilding
{
    FieldOfView _fov;
    protected FieldOfView FOV => _fov;

    HashSet<Transform> _visibleTargets => _fov.FindVisibleTargets();

    private Material _rangeMat;

    private MeshRenderer _archerAttackRangeMesh;

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();

        _archerAttackRangeMesh = transform.Find("AttackRange/Slinder").GetComponent<MeshRenderer>();
        _rangeMat = _archerAttackRangeMesh.material;

        _archerAttackRangeMesh.gameObject.SetActive(false);
    }

    protected override void Running()
    {
        if (IsInstalling)
        {
            Transform[] newColls = _visibleTargets.ToArray();
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

        _archerAttackRangeMesh.gameObject.SetActive(false);
    }

}
