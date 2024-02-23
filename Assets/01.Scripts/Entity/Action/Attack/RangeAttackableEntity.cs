using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackableEntity : EntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack()
    {
        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        arrow.Setting(owner.Stat, DamageCasterCompo.TargetLayer);
        arrow.Fire(_firePos.forward);
    }
}
