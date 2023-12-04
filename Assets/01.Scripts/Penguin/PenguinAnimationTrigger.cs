using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAnimationTrigger : MonoBehaviour
{
    private Penguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<Penguin>();
    }

    public void AttackTrigger()
    {
        _penguin.Attack();
    }

    public void DeadCompleteTrigger()
    {
        _penguin.enabled = false;
    }

    public void AnimationTrigger()
    {
        _penguin.AnimationFinishTrigger();
    }
}
