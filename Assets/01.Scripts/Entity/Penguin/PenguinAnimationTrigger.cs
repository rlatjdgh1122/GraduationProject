using UnityEngine;
using UnityEngine.Events;


public class PenguinAnimationTrigger : MonoBehaviour
{
    private Penguin _penguin;

    [SerializeField] private UnityEvent OnPrevAttackEffectEvent = null;
    [SerializeField] private UnityEvent OnEndAttackEffectEvent = null;

    [SerializeField] private UnityEvent OnSpecialAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnAoEAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnStunTriggerEvent = null;
    [SerializeField] private UnityEvent OnRangeAttackTriggerEvent = null;

    [SerializeField] private UnityEvent OnDeadCompleteTriggerEvent = null;

    [SerializeField] private UnityEvent OnAnimationEndTriggerEvent = null;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<Penguin>();
    }

    public void PrevAttackEffectTrigger()
    {
        OnPrevAttackEffectEvent?.Invoke();
    }

    public void EndAttackEffectEventTrigger()
    {
        OnEndAttackEffectEvent?.Invoke();
    }

    /// <summary>
    /// 몇 배 만큼 더쎄게 공격
    /// </summary>
    /// <param name="AfewTimes"> 스탯의 몇 배</param>
    public void SpecialAttackTrigger(float AfewTimes)
    {
        _penguin.AttackCompo.SpecialAttack(AfewTimes);
        OnSpecialAttackTriggerEvent?.Invoke();
    }

    /// <summary>
    /// 광역 공격
    /// </summary>
    public void AoEAttackTrigger(int isKnb)
    {
        _penguin.AttackCompo.AoEAttack(isKnb == 0 ? false : true, 1.5f);
        OnAoEAttackTriggerEvent?.Invoke();
    }
    public void AttackTrigger()
    {
        _penguin.AttackCompo.MeleeAttack();
        OnAttackTriggerEvent?.Invoke();

    }

    public void StunTigger(int isStun)
    {
        _penguin.AttackCompo.StunAttack(isStun == 0 ? false : true, 3f);
        OnStunTriggerEvent?.Invoke();
    }

    private void RangeAttackTrigger()
    {
        if (_penguin.CurrentTarget != null)
        {
            _penguin.AttackCompo.RangeAttack(_penguin.CurrentTarget.transform.position);
        }
        else
        {
            _penguin.AttackCompo.RangeAttack(_penguin.transform.forward);
        }
        OnRangeAttackTriggerEvent?.Invoke();

    }

    public void DeadCompleteTrigger()
    {
        _penguin.enabled = false;
        OnDeadCompleteTriggerEvent?.Invoke();

    }

    private void AnimationEndTrigger()
    {
        _penguin.AnimationTrigger();
        OnAttackTriggerEvent?.Invoke();

    }
}
