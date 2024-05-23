using DG.Tweening;
using UnityEngine;

public class ThrowSkill : Skill
{
    [SerializeField] private float _throwDelay;
    [SerializeField] private float _throwValue = 2f;

    private Transform _visualTrm = null;

    private Vector3 _normal = Vector3.zero;

    private Quaternion _cashingLocalRot = Quaternion.identity;

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
        _visualTrm = _owner.CurrentTarget.transform.Find("Visual");

        /* RaycastHit raycastHit;
         bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, 10, _owner.TargetLayer);
         if (raycastSuccess)
         {
             _normal = raycastHit.normal;
         }
 */
        _cashingLocalRot = _visualTrm.localRotation;

        _visualTrm.DORotate(new Vector3(90, 0, 0), 1.1f);
        _visualTrm.DOMoveY(2.8f, 1.2f).SetEase(Ease.OutQuint);
    }

    public void Throw()
    {
        if (!_owner.IsTargetInThrowRange) return;

        _visualTrm.localRotation = _cashingLocalRot;
        _visualTrm.localPosition = Vector3.zero;

        _owner.CurrentTarget.HealthCompo.Knockback(_throwValue, transform.forward);
    }
}