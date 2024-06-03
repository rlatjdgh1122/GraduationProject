using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class StrengthBuffBuilding : BuffBuilding
{
    private readonly string _penguinEffect = "PenguinDamageUp";

    protected override void Awake()
    {
        base.Awake();

        buildingEffectFeedback = transform.Find("BuffFeedback").GetComponent<FeedbackPlayer>();
    }

    protected override void EnterTarget(Collider coll)
    {
        if (coll.gameObject.TryGetComponent(out Penguin penguin))
        {
            penguin.Stat.AddStat(GetBuffValue(), buffStatType, buffStatMode);
            penguin.StrengthBuffEffect?.ParticleStart();
        }
    }

    protected override void ExitTarget(Collider coll)
    {
        if (coll.gameObject.TryGetComponent(out Penguin penguin))
        {
            CoroutineUtil.CallWaitForSeconds(OutoffRangeBuffDuration, () =>
            {
                penguin.Stat.RemoveStat(GetBuffValue(), buffStatType, StatMode.Increase);
                EndBuffEffect(coll, penguin);
            });
        }
    }

    private void EndBuffEffect(Collider coll, Penguin penguin)
    {
        penguin.StrengthBuffEffect?.ParticleStop();

        RemoveExitTargetList(coll);

        CheckEnterTarget();
    }

    protected override void HandleDie()
    {
        base.HandleDie();

        foreach(var coll in _buffTargetList)
        {
            if (coll.gameObject.TryGetComponent(out Penguin penguin))
            {
                CoroutineUtil.CallWaitForSeconds(OutoffRangeBuffDuration, () =>
                {
                    penguin.Stat.RemoveStat(GetBuffValue(), buffStatType, StatMode.Increase);
                    EndBuffEffect(coll, penguin);
                });
            }
        }
    }


    #region Set Value

    protected override void SetBuffValue(int value)
    {
        this.buffValue = value;
    }

    protected override int GetBuffValue()
    {
        return this.buffValue;
    }

    protected override void SetOutoffRangeBuffDuration(float value)
    {
        this.OutoffRangeBuffDuration = value;
    }

    protected override float GetOutoffRangeBuffDuration()
    {
        return this.OutoffRangeBuffDuration;
    }
    #endregion
}
