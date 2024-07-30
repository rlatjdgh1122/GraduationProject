using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParabolaAttackableEntity : RangeAttackableEntity
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void RangeAttack()
    {
        TargetObject curtarget = null;

        if(owner.CurrentTarget == null ||
           owner.CurrentTarget.IsDead)
        {
            return;
        }

        Collider[] colliders = GetColliders();

        if (colliders.Length > 0)
        {
            curtarget = owner.CurrentTarget.GetComponent<TargetObject>();
            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));
        }

        ParabolaArrow arrow = GenerateArrow();

        ExecuteAttack(curtarget, arrow);
    }

    public override void UltimateRangeAttack()
    {
        Collider[] colliders = GetColliders();

        if(colliders.Length <= 0)
        {
            SkillRangeAttack();
            return;
        }

        foreach (Collider collider in colliders)
        {
            TargetObject curtarget = collider.GetComponent<TargetObject>();
            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));

            ParabolaArrow arrow = GenerateArrow();
            ExecuteAttack(curtarget, arrow);
        }
    }

    public override void SkillRangeAttack()
    {
        TargetObject curtarget = null;

        Collider[] colliders = GetColliders();

        if (colliders.Length > 0)
        {
            curtarget = GetColliders().FirstOrDefault().GetComponent<TargetObject>();
            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));
        }

        ParabolaArrow arrow = GenerateArrow();

        ExecuteAttack(curtarget, arrow);
    }

    private Collider[] GetColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position,
                                              15f,
                                              DamageCasterCompo.TargetLayer);

        return colliders;
    }

    private ParabolaArrow GenerateArrow()
    {
        ParabolaArrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation) as ParabolaArrow;
        //Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as Arrow;
        arrow.transform.position = _firePos.position;
        arrow.transform.rotation = Quaternion.Euler(_firePos.transform.forward);

        arrow.Setting(owner, DamageCasterCompo.TargetLayer);

        return arrow;
    }

    private void ExecuteAttack(TargetObject curtarget, ParabolaArrow arrow)
    {
        if (curtarget != null)
        {
            arrow.ExecuteAttack(_firePos.position, curtarget.transform.position, 10f, false);
        }
        else
        {
            Vector3 targetPos = Random.insideUnitSphere * Random.Range(1f, 5f) + transform.position;
            arrow.ExecuteAttack(_firePos.position, targetPos, 10f, false);
        }
    }

}
