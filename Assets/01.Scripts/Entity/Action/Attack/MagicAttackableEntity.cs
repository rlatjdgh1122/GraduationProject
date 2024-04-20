using System;
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
            owner.CurrentTarget.transform.position.y, owner.CurrentTarget.transform.position.z));

        //Meteor meteor = PoolManager.Instance.Pop(_meteorPrefab.name) as Meteor;
        //meteor.transform.position = _firePos.transform.localPosition;
        //meteor.transform.rotation = Quaternion.Euler(-90, _firePos.rotation.eulerAngles.y, 0);
        Meteor meteor = Instantiate(_meteorPrefab, _firePos.transform.position, Quaternion.Euler(-90, _firePos.rotation.eulerAngles.y, 0));

        meteor.Setting(owner, DamageCasterCompo.TargetLayer);
        meteor.Fire(owner.CurrentTarget.transform.position - _firePos.transform.position);
    }
}
