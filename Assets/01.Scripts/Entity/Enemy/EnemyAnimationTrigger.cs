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

    public void AoEAttackTrigger()
    {
        _enemy.AttackCompo.AoEAttack();
    }

    public void AttackTrigger()
    {
        _enemy.AttackCompo.MeleeAttack();
    }

    private void RangeAttackTrigger()
    {
        _enemy.AttackCompo.RangeAttack();
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
