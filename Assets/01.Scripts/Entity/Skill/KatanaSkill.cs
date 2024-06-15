using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class KatanaSkill : Skill
{
    [SerializeField] private float _dashDelay;

    private Coroutine _dashCoroutine;
    public bool canDash = false;

    public UnityEvent OnSlashEvent;
    public UnityEvent OnDashEvent;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Dash(_dashDelay);
    }

    public void Dash(float delay)
    {
        OnSlashEvent?.Invoke();

        if (_dashCoroutine != null)
            StopCoroutine(DashCoroutine(0));

        _dashCoroutine = StartCoroutine(DashCoroutine(delay));
    }

    private IEnumerator DashCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        OnDashEvent?.Invoke();
    }
}