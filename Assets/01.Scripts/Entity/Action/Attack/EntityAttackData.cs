using UnityEngine;

public class EntityAttackData : MonoBehaviour
{
    public DamageCaster DamageCasterCompo => owner.DamageCasterCompo;

    protected Entity owner;

    protected virtual void Awake()
    {
        owner = GetComponent<Entity>();
    }

    public virtual void AoEAttack(float knbValue, float stunValue, float range = 0)
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
