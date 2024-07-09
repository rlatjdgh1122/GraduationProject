using SkillSystem;
using System.Collections;
using UnityEngine;

public class CoolTimeDecsion : SKillDecision
{
    //private WaitForSeconds HeartBeat = new WaitForSeconds(0.05f);

    protected override void Init()
    {
        saveValue = Time.time;
    }

    public override void OnUsed()
    {
        OnSkillUsedEvent?.Invoke();
        saveValue = Time.time;

        StartCoroutine(CoolTimeCorou());
    }

    public override bool MakeDecision()
    {
        return saveValue + maxValue <= Time.time;
    }

    public override void LevelUp(int value)
    {
        maxValue -= value;
    }

    private IEnumerator CoolTimeCorou()
    {
        float elapsedTime = 0f;

        while (elapsedTime < maxValue)
        {
            elapsedTime += Time.deltaTime;
            yield return null;

            OnSkillActionEnterEvent?.Invoke();
        }

        //스킬 사용 가능
        OnSkillReadyEvent?.Invoke();
    }

    
}