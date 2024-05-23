using DG.Tweening;
using UnityEngine;

public class ThrowSkill : Skill
{
    [SerializeField] private float _throwDelay;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        if (CanUseSkill)
        {
            Throw();
            CanUseSkill = false;
        }
        else
        {
            Grab();
            CanUseSkill = true;
        } 
    }

    public void Grab() 
    {
        _owner.CurrentTarget.transform.DORotate(new Vector3(0, 0, 90), 1.1f);
        _owner.CurrentTarget.transform.DOMoveY(2.8f, 1.2f).SetEase(Ease.OutQuint);
    }

    public void Throw()
    {
        if (!_owner.IsTargetInThrowRange) return;

        _owner.CurrentTarget.HealthCompo.Knockback(4, -transform.forward);
    }
}