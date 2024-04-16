using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DashSkill : Skill
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;

    public UnityEvent OnDashEvent;

    private General general => _owner as General;

    private Coroutine _dashCoroutine;

    public bool canDash = false;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public void DashHandler()
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
        general.NavAgent.enabled = false;

        while (Time.time < startTime + time)
        {
            general.CharacterCompo.Move(general.transform.forward * speed * Time.deltaTime);
            yield return null;
        }

        general.NavAgent.enabled = true;
    }
}