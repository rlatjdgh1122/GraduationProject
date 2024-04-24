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

    public void AoEAttackTrigger(string parmeter = "0 0")
    {
        var value = parmeter.Split(' ');
        float.TryParse(value[0], out float knbValue);
        float.TryParse(value[1], out float stunValue);

        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.AoEAttack(knbValue, stunValue);
    }
    public void AttackTrigger(string parmeter = "0 0")
    {
        var value = parmeter.Split(' ');
        float.TryParse(value[0], out float knbValue);
        float.TryParse(value[1], out float stunValue);

        OnAttackSoundEvent?.Invoke();
        _enemy.AttackCompo.MeleeAttack(knbValue,stunValue);
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
