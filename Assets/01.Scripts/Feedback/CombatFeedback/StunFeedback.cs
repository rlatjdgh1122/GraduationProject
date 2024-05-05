using System.Collections;
using UnityEngine;

public class StunFeedback : CombatFeedback
{

    private Coroutine StunCoruotine = null;

    public override bool StartFeedback()
    {
        if (owner.IsDead) return false;

        if (StunCoruotine != null)
            StopCoroutine(StunCoruotine);

        StunCoruotine = StartCoroutine(StunCoroutine(Value));
        return true;
    }
    public override bool FinishFeedback()
    {
        return true;
    }


    private IEnumerator StunCoroutine(float duration)
    {
        //var navSpeed = _navMeshAgent.speed;
        //var animSpeed = _animator.speed;
        //_navMeshAgent.speed = 0f;
        //_animator.speed = 0f;

        _animator.enabled = false;
        _navMeshAgent.enabled = false;

        yield return new WaitForSeconds(duration);

        if (!owner.IsDead)
        {
            _animator.enabled = true;
            _navMeshAgent.enabled = true;

            owner.MoveToCurrentTarget();
        }


        //_animator.speed = navSpeed;
        //_navMeshAgent.speed = animSpeed;
    }
}
