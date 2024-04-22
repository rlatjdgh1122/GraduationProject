using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DashSkill : Skill
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;

    private Coroutine _dashCoroutine;
    public bool canDash = false;

    public UnityEvent OnDashEvent;

    public override void SetOwner(General owner)
    {
        base.SetOwner(owner);
    }

    public override void PlaySkill()
    {
        Dash(_dashDelay, _dashTime, _dashSpeed);
    }

    public void Dash(float delay, float time, float speed)
    {
        if (_dashCoroutine != null)
            StopCoroutine(DashCoroutine(0, 0, 0));

        _dashCoroutine = StartCoroutine(DashCoroutine(delay, time, speed));
    }

    private IEnumerator DashCoroutine(float delay, float time, float speed)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        OnDashEvent?.Invoke();
        _owner.NavAgent.enabled = false;

        while (Time.time < startTime + time)
        {
            _owner.CharacterCompo.Move(_owner.transform.forward * speed * Time.deltaTime);
            yield return null;
        }

        _owner.NavAgent.enabled = true;
    }
}