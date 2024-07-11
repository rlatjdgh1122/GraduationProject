using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ParameterKnockbackEvent
{
    public bool knockback = false;
    public float value = 1f;
}

public class EnemyAnimationTrigger : MonoBehaviour
{
    public UnityEvent OnPrevAttackEffectEvent = null;
    public UnityEvent OnEndAttackEffectEvent = null;
    public UnityEvent OnAttackSoundEvent = null;

    protected Enemy _enemy;

    protected virtual void Awake()
    {
        _enemy = transform.parent.GetComponent<Enemy>();
    }

    protected virtual void PrevAttackEffectTrigger()
    {
        OnPrevAttackEffectEvent?.Invoke();
    }

    protected virtual void EndAttackEffectEventTrigger()
    {
        OnEndAttackEffectEvent?.Invoke();
    }

    protected virtual void AoEAttackTrigger(string parmeter = "0 0")
    {
        var value = parmeter.Split(' ');
        float.TryParse(value[0], out float knbValue);
        float.TryParse(value[1], out float stunValue);

        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.AoEAttack(knbValue, stunValue);
    }

    protected virtual void AttackTrigger(string parmeter = "0 0")
    {
        var value = parmeter.Split(' ');
        float.TryParse(value[0], out float knbValue);
        float.TryParse(value[1], out float stunValue);

        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MeleeAttack(knbValue,stunValue);
    }

    protected virtual void BombAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.BombAttack();
    }

    protected virtual void RangeAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.RangeAttack();
    }

    protected virtual void MagicAttackTrigger()
    {
        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MagicAttack(transform.forward);
    }

    protected virtual void DeadCompleteTrigger()
    {
        _enemy.enabled = false;
    }

    protected virtual void AnimationEndTrigger()
    {
        _enemy.AnimationTrigger();
    }
}
