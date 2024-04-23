using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSkill : Skill
{
    [SerializeField] private float _throwDelay;
    [SerializeField] private float _throwSpeed;

    private Coroutine _throwCoroutine;

    public override void SetOwner(General owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Debug.Log("¶Ë");
        Throw();
    }

    public void Throw()
    {
        if (_throwCoroutine != null)
            StopCoroutine(ThrowCoroutine(0, 0));

        _throwCoroutine = StartCoroutine(ThrowCoroutine(_throwDelay, _throwSpeed)); 
    }

    private IEnumerator ThrowCoroutine(float delay, float speed)
    {
        yield return new WaitForSeconds(delay);

        _owner.CurrentTarget.HealthCompo.ApplyKnockback(3, transform.forward);
    }

    //public void Dash(float delay, float time, float speed)
    //{
    //    if (_dashCoroutine != null)
    //        StopCoroutine(DashCoroutine(0, 0, 0));

    //    _dashCoroutine = StartCoroutine(DashCoroutine(delay, time, speed));
    //}

    //private IEnumerator DashCoroutine(float delay, float time, float speed)
    //{
    //    yield return new WaitForSeconds(delay);

    //    float startTime = Time.time;

    //    OnDashEvent?.Invoke();
    //    general.NavAgent.enabled = false;

    //    while (Time.time < startTime + time)
    //    {
    //        general.CharacterCompo.Move(general.transform.forward * speed * Time.deltaTime);
    //        yield return null;
    //    }

    //    general.NavAgent.enabled = true;
    //}
}
