using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.Events;


public class PenguinAnimationTrigger : MonoBehaviour
{
    protected Penguin _penguin;

    [SerializeField] private UnityEvent OnPrevAttackEffectEvent = null;
    [SerializeField] private UnityEvent OnEndAttackEffectEvent = null;

    [SerializeField] private UnityEvent OnSpecialAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnAoEAttackTriggerEvent = null;
    [SerializeField] private UnityEvent OnStunTriggerEvent = null;
    [SerializeField] private UnityEvent OnRangeAttackTriggerEvent = null;

    [SerializeField] private UnityEvent OnDeadCompleteTriggerEvent = null;

    [SerializeField] private UnityEvent OnAnimationEndTriggerEvent = null;
    [SerializeField] private UnityEvent OnDashEndTriggerEvent = null;

    [SerializeField] private UnityEvent OnEffectOnTriggerEvent = null;
    [SerializeField] private UnityEvent OnEffectOffTriggerEvent = null;

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
        _penguin.AttackCompo?.SpecialAttack(AfewTimes);
        OnSpecialAttackTriggerEvent?.Invoke();
    }

    /// <summary>
    /// 광역 공격
    /// </summary>
    public void AoEAttackTrigger(string parmeter)
    {
        OnAoEAttackTriggerEvent?.Invoke();

        var value = parmeter.Split(' ');
        try
        {
            if (float.TryParse(value[0], out float knbValue)
               && float.TryParse(value[1], out float stunValue))
            {
                _penguin.AttackCompo.AoEAttack(knbValue, stunValue);
            }
        }
        catch
        {
            Debug.LogError($"Put values ​​for the parameters. target : {transform.parent.name}, AoEAttackTrigger");
        }

    }

    /// <summary>
    /// 찌르기 궁극기
    /// </summary>
    public void AoEPrickTrigger(string parmeter)
    {
        OnAoEAttackTriggerEvent?.Invoke();

        var value = parmeter.Split(' ');
        try
        {
            if (float.TryParse(value[0], out float knbValue)
               && float.TryParse(value[1], out float stunValue))
            {
                _penguin.AttackCompo.AoEPrickAttack(knbValue, stunValue);
            }
        }
        catch
        {

        }

    }

    public void AttackTrigger(string parmeter)
    {
        OnAttackTriggerEvent?.Invoke();

        if (string.IsNullOrWhiteSpace(parmeter))
        {
            Debug.LogError($"Put values ​​for the parameters. target : {transform.parent.name}, AttackTrigger");
            return;
        }

        var value = parmeter.Split(' ');

        if (float.TryParse(value[0], out float knbValue)
            && float.TryParse(value[1], out float stunValue))
        {
            _penguin.AttackCompo?.MeleeAttack(knbValue, stunValue);
        }
    }

    public void DashAttackTrigger()
    {
        _penguin.AttackCompo.DashAttack();
    }

    public void RangeAttackTrigger()
    {
        if (_penguin.CurrentTarget != null)
        {
            _penguin.AttackCompo?.RangeAttack();
        }
        else
        {
            _penguin?.AttackCompo?.RangeAttack();
        }
        OnRangeAttackTriggerEvent?.Invoke();

    }

    public void SkillRangeAttackTrigger()
    {
        if (_penguin.CurrentTarget != null)
        {
            _penguin.AttackCompo?.SkillRangeAttack();
        }
        else
        {
            _penguin?.AttackCompo?.SkillRangeAttack();
        }
    }
    

    public void UltimateAttackTrigger()
    {
        if (_penguin.CurrentTarget != null)
        {
            _penguin.AttackCompo?.UltimateRangeAttack();
        }
        else
        {
            _penguin?.AttackCompo?.UltimateRangeAttack();
        }
    }

    private void MagicAttackTrigger()
    {
        if (_penguin.CurrentTarget != null)
        {
            _penguin.AttackCompo?.MagicAttack(_penguin.CurrentTarget.transform.position);
        }
        else
        {
            _penguin?.AttackCompo?.MagicAttack(_penguin.transform.forward);
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

    public void DashEndTrigger()
    {
        OnDashEndTriggerEvent?.Invoke();
    }

    public void EffectStartTrigger()
    {
        OnEffectOnTriggerEvent?.Invoke();
    }

    public void EffectEndTrigger()
    {
        OnEffectOffTriggerEvent?.Invoke();
    }
}