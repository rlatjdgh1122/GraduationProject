using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicAttackableEntity : EntityAttackData
{
    [Header("MagicAttack Info")]
    [SerializeField] private Meteor _meteorPrefab;
    [SerializeField] private Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void MagicAttack(Vector3 targetPos)
    {
        _firePos.LookAt(new Vector3(owner.CurrentTarget.transform.position.x,
            owner.CurrentTarget.transform.position.y + 0.5f, owner.CurrentTarget.transform.position.z));

        Meteor meteor = Instantiate(_meteorPrefab, _firePos.transform.position, _firePos.rotation);
        //Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as Arrow;
        //arrow.transform.position = _firePos.position;
        //arrow.transform.rotation = Quaternion.Euler(_firePos.transform.forward);
        meteor.Setting(owner, DamageCasterCompo.TargetLayer);
        meteor.Fire(_firePos.forward);
    }
}
