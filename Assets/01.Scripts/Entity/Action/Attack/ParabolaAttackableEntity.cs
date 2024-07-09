using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaAttackableEntity : RangeAttackableEntity
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack(Vector3 targetPos)
    {
        TargetObject curtarget = null;

        if (owner.IsTargetInAttackRange)
        {
            curtarget = owner.CurrentTarget;

            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));

            SingijeonArrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation);
            //Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as Arrow;
            //arrow.transform.position = _firePos.position;
            //arrow.transform.rotation = Quaternion.Euler(_firePos.transform.forward);
            arrow.Setting(owner, DamageCasterCompo.TargetLayer);
            arrow.Fire(_firePos.forward);
        }
    }
}
