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

    public void AoEAttackTrigger(int isKnb)
    {
        _enemy.AttackCompo.AoEAttack(isKnb == 0 ? false : true, 1.5f);
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
        _enemy.AttackCompo.RangeAttack(transform.forward);
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
