using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class ParameterKnockbackEvent
{
    public bool knockback = false;
    public float value = 1f;
}

public class EnemyAnimationTrigger : MonoBehaviour
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    public void AoEAttackTrigger(bool Knb, float value)
    {
        _enemy.AttackCompo.AoEAttack(Knb, value);
    }

    public void SphereAttackTrigger()
    {
        _enemy.AttackCompo.MeleeSphereAttack();
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

    public void AnimationEndTrigger()
    {
        _enemy.AnimationTrigger();
    }
}
