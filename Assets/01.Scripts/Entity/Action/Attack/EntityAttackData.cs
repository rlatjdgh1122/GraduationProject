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
        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        owner = GetComponent<Entity>();
        DamageCasterCompo.SetOwner(owner.Stat);
    }

    public virtual void AoEAttack(bool Knb,float value)
    {

    }

    public virtual void MeleeAttack()
    {

    }

    public virtual void RangeAttack()
    {

    }

    public virtual void SpecialAttack(float aFewTimes)
    {

    }
}
