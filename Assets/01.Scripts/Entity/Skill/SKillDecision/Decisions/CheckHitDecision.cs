using SkillSystem;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CheckHitDecision : SKillDecision
{
    private bool _checkSkillReady = true;

    protected override void OnHit(int hitCount)
    {
        if (!_checkSkillReady) return;

        OnSkillActionEnterEvent?.Invoke();

        if (MakeDecision()) //스킬사용 조건이 처음 만족할때 한번 실행
        {
            OnSkillReadyEvent?.Invoke();
            _checkSkillReady = false;
        }
    }

    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();

        skillActionData.AddSkillUsedCount();
        saveValue = entityActionData.HitCount;
        _checkSkillReady = true;
    }

    public override bool MakeDecision()
    {
        return maxValue + saveValue <= entityActionData.HitCount;
    }

    public override void LevelUp(int value)
    {
        maxValue -= value;
    }
}
