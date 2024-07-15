using Unity.VisualScripting;
using UnityEngine;

public class EntityAttackData : MonoBehaviour
{
    public DamageCaster DamageCasterCompo => owner.DamageCasterCompo;

    protected Entity owner;

    protected virtual void Awake()
    {
        owner = GetComponent<Entity>();
    }

    //range필요없음
    public virtual void AoEAttack(float knbValue, float stunValue, float range = 0)
    {

    }

    public virtual void AoEPrickAttack(float knbValue, float stunValue, float range = 0)
    {

    }

    public virtual void MeleeAttack(float knbValue, float stunValue)
    {

    }

    public virtual void MeleeSphereAttack()
    {

    }

    public virtual void BombAttack()
    {

    }

    public virtual void RangeAttack()
    {

    }

    public virtual void MagicAttack(Vector3 targetPos)
    {

    }

    public virtual void TruckAttack(Vector3 mousePos)
    {

    }

    public virtual void SpecialAttack(float aFewTimes)
    {

    }

    public virtual void DashAttack()
    {

    }

    public virtual void UltimateRangeAttack()
    {

    }

    public virtual void SkillRangeAttack()
    {

    }
}
