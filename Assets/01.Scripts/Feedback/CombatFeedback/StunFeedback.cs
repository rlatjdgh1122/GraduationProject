using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StunFeedback : CombatFeedback
{
    public override float Value { get => base.Value; set => base.Value = value; }

    private Coroutine StunCoruotine = null;

    public override bool StartFeedback()
    {
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
        var navSpeed = _navMeshAgent.speed;
        var animSpeed = _animator.speed;

        _controller.enabled = false;
        _navMeshAgent.speed = 0f;
        _animator.speed = 0f;

        yield return new WaitForSeconds(duration);

        _animator.speed = navSpeed;
        _controller.enabled = true;
        _navMeshAgent.speed = animSpeed;
    }
}
