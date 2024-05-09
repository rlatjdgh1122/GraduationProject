using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RangeAttackableEntity : EntityAttackData
{
    [Header("RangeAttack Info")]
    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _firePos;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack(Vector3 targetPos)
    {
        TargetObject curtarget = null;

        if (owner.CurrentTarget != null)
        {
            curtarget = owner.CurrentTarget;
        }
        else
        {
            if (owner is Enemy)
            {
                curtarget = (owner as Enemy).NexusTarget;
            }
        }


        _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));

        Arrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
        //Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as Arrow;
        //arrow.transform.position = _firePos.position;
        //arrow.transform.rotation = Quaternion.Euler(_firePos.transform.forward);
        arrow.Setting(owner, DamageCasterCompo.TargetLayer);
        arrow.Fire(_firePos.forward);
    }
}
