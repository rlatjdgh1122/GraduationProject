using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    public void AttackTrigger()
    {
        _enemy.Attack();
    }

    private void RangeAttackTrigger()
    {
        _enemy.RangeAttack();
    }

    public void DeadCompleteTrigger()
    {
        _enemy.enabled = false;
    }

    private void AnimationEndTrigger()
    {
        _enemy.AnimationTrigger();
    }
}
