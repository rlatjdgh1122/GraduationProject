using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DashSkill : Skill
{
    [SerializeField] private float _dashDelay;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;

    private General owner => _owner as General;

    public override void SetOwner(Entity owner)
    {
        base.SetOwner(owner);

        OnSkillStart += DashHandler;
    }

    public void DashHandler()
    {
        Quaternion playerRotation = owner.AnimatorCompo.transform.rotation; //Visual의 회전값을가져온다
        Vector3 forwardDirection = playerRotation * Vector3.forward;
        Dash(forwardDirection, _dashDelay, _dashTime, _dashSpeed);
    }

    public void Dash(Vector3 dir, float delay, float time, float speed)
    {
        StopCoroutine(DashCoroutine(dir, delay, time, speed));
        StartCoroutine(DashCoroutine(dir, delay, time, speed));
    }

    private IEnumerator DashCoroutine(Vector3 dir, float delay, float time, float speed)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;

        owner.NavAgent.radius = 0.01f;

        while (Time.time < startTime + time)
        {
            owner.CharacterCompo.Move(dir * speed * Time.deltaTime);

            yield return null;
        }

        owner.NavAgent.radius = 0.45f;
    }
}
