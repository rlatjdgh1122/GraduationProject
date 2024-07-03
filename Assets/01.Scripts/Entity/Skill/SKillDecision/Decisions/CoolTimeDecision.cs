using SkillSystem;
using UnityEngine;

public class CoolTimeDecsion : SKillDecision
{
    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();

        saveValue = Time.time;
    }

    public override bool MakeDecision()
    {
        return saveValue + maxValue <= Time.time;
    }

    public override void LevelUp(int value)
    {
        maxValue -= value;
    }
}