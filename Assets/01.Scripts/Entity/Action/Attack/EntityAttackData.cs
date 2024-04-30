using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntityAttackData : MonoBehaviour
{
    public DamageCaster DamageCasterCompo { get; protected set; }

    protected Entity owner;

    protected virtual void Awake()
    {
        
        DamageCasterCompo = transform.GetComponentInChildren<DamageCaster>();
        owner = GetComponent<Entity>();
        DamageCasterCompo.SetOwner(owner);
    }

    public virtual void AoEAttack(float knbValue, float stunValue)
    {

    }

    public virtual void MeleeAttack(float knbValue, float stunValue)
    {

    }

    public virtual void MeleeSphereAttack()
    {

    }

    public virtual void RangeAttack(Vector3 targetPos)
    {

    }

    public virtual void MagicAttack(Vector3 targetPos)
    {

    }

    public virtual void SpecialAttack(float aFewTimes)
    {

    }

    public virtual void DashAttack()
    {

    }
}
