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

        Collider[] colliders = GetColliders();

        if (colliders.Length > 0)
        {
            curtarget = GetColliders().FirstOrDefault().GetComponent<TargetObject>();
            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));
        }

        SingijeonArrow arrow = GenerateArrow();

        ExecuteAttack(curtarget, arrow);
    }

    public override void UltimateRangeAttack()
    {
        Collider[] colliders = GetColliders();

        foreach (Collider collider in colliders)
        {
            TargetObject curtarget = collider.GetComponent<TargetObject>();
            _firePos.LookAt(new Vector3(curtarget.transform.position.x,
            curtarget.transform.position.y + 0.5f, curtarget.transform.position.z));

            SingijeonArrow arrow = GenerateArrow();
            ExecuteAttack(curtarget, arrow);
        }
    }

    private Collider[] GetColliders()
    {
        Collider[] colliders = Physics.OverlapSphere(owner.transform.position,
                                              15f,
                                              DamageCasterCompo.TargetLayer);

        return colliders;
    }

    private SingijeonArrow GenerateArrow()
    {
        SingijeonArrow arrow = Instantiate(_arrowPrefab, _firePos.transform.position, _firePos.rotation) as SingijeonArrow;
        //Arrow arrow = PoolManager.Instance.Pop(_arrowPrefab.name) as Arrow;
        arrow.transform.position = _firePos.position;
        arrow.transform.rotation = Quaternion.Euler(_firePos.transform.forward);

        arrow.Setting(owner, DamageCasterCompo.TargetLayer);

        return arrow;
    }

    private void ExecuteAttack(TargetObject curtarget, SingijeonArrow arrow)
    {
        if (curtarget != null)
        {
            arrow.ExecuteAttack(_firePos.position, curtarget.transform.position, 10f, false);
        }
        else
        {
            Debug.Log("·£´ý");
            arrow.ExecuteAttack(_firePos.position, Random.insideUnitSphere * Random.Range(1f, 5f), 10f, false);
        }
    }

}
