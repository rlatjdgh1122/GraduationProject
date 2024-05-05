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
        if (owner.IsDead) yield break;

        _animator.enabled = false;
        _navMeshAgent.enabled = false;

        yield return new WaitForSeconds(duration);

        _animator.enabled = true;
        _navMeshAgent.enabled = true;

    }
}
