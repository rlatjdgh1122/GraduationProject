using System.Security.Cryptography;
using UnityEngine;

public class PenguinAnimationTrigger : MonoBehaviour
{
    private Penguin _penguin;

    private void Awake()
    {
        _penguin = transform.parent.GetComponent<Penguin>();
    }

    /// <summary>
    /// 몇 배 만큼 더쎄게 공격
    /// </summary>
    /// <param name="AfewTimes"> 스탯의 몇 배</param>
    public void SpecialAttack(float AfewTimes)
    {
        _penguin.AttackCompo.SpecialAttack(AfewTimes);
    }
    public void AttackTrigger()
    {
        _penguin.AttackCompo.MeleeAttack();
    }

    /// <summary>
    /// 광역 공격
    /// </summary>
    public void AoEAttackTrigger()
    {
        _penguin.AttackCompo.AoEAttack();
    }

    private void RangeAttackTrigger()
    {
        _penguin.AttackCompo.RangeAttack();
    }

    public void DeadCompleteTrigger()
    {
        _penguin.enabled = false;
    }

    private void AnimationEndTrigger()
    {
        _penguin.AnimationTrigger();
    }
}
