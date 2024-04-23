using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

[System.Serializable]
public class ParameterKnockbackEvent
{
    public bool knockback = false;
    public float value = 1f;
}

public class EnemyAnimationTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPrevAttackEffectEvent = null;
    [SerializeField] private UnityEvent OnEndAttackEffectEvent = null;
    [SerializeField] private UnityEvent OnAttackSoundEvent = null;
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }
    public void PrevAttackEffectTrigger()
    {
        OnPrevAttackEffectEvent?.Invoke();
    }

    public void EndAttackEffectEventTrigger()
    {
        OnEndAttackEffectEvent?.Invoke();
    }

    public void AoEAttackTrigger(float knbValue)
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.AoEAttack(knbValue);
    }

    public void SphereAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MeleeSphereAttack();
    }

    public void AttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MeleeAttack();
    }

    private void RangeAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.RangeAttack(transform.forward);
    }

    private void MagicAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MagicAttack(transform.forward);
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
