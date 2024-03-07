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
    /// �� �� ��ŭ ����� ����
    /// </summary>
    /// <param name="AfewTimes"> ������ �� ��</param>
    public void SpecialAttackTrigger(float AfewTimes)
    {
        _penguin.AttackCompo.SpecialAttack(AfewTimes);
        OnSpecialAttackTriggerEvent?.Invoke();
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void AoEAttackTrigger(int isKnb)
    {
        _penguin.AttackCompo.AoEAttack(isKnb  == 0 ? false : true, 1.2f);
        OnAoEAttackTriggerEvent?.Invoke();
    }
    public void AttackTrigger()
    {
        _penguin.AttackCompo.MeleeAttack();
        OnAttackTriggerEvent?.Invoke();

    }

    private void RangeAttackTrigger()
    {
        _penguin.AttackCompo.RangeAttack(_penguin.CurrentTarget.transform.position);
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
