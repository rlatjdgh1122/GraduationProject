using System.Collections;
using UnityEngine;

public class ThrowSkill : Skill
{
    [SerializeField] private float _throwDelay;

    private Coroutine _throwCoroutine;

    public override void SetOwner(General owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Throw();
    }

    public void Throw()
    {
        if (_throwCoroutine != null)
            StopCoroutine(ThrowCoroutine(0));

        _throwCoroutine = StartCoroutine(ThrowCoroutine(_throwDelay)); 
    }

    private IEnumerator ThrowCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        _owner.CurrentTarget.HealthCompo.KnockBack(4, -transform.forward);
    }
}
