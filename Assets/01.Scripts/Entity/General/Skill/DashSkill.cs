using System.Collections;
using UnityEngine;

public class DashSkill : Skill
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;

    private General owner => _owner as General;

    private Coroutine _dashCoroutine;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);
    }

    public void DashHandler()
    {
        owner.canDash = false;
        Vector3 direction = (owner.CurrentTarget.transform.position - owner.transform.position).normalized;
        owner.transform.rotation = Quaternion.LookRotation(direction);
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

        while (Time.time < startTime + time)
        {
            owner.CharacterCompo.Move(owner.transform.forward * speed * Time.deltaTime);
            yield return null;
        }
    }
}