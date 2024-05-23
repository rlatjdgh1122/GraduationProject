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

            EffectPlayer buffEffect = PoolManager.Instance.Pop(_penguinEffect) as EffectPlayer;

            buffEffect.transform.SetParent(penguin.transform);
            buffEffect.transform.localPosition = Vector3.zero;
            buffEffect.transform.localScale = Vector3.one * 0.5f;
            buffEffect.transform.rotation = Quaternion.identity;

            var main = buffEffect.Particles[0].main;
            main.startSize = 0.3f;

            buffEffect.StartPlay(OutoffRangeBuffDuration);
        }
    }

    protected override void ExitTarget(Collider coll)
    {
        if (coll.gameObject.TryGetComponent(out Penguin penguin))
        {
            CoroutineUtil.CallWaitForSeconds(OutoffRangeBuffDuration, null, () =>
            {
                penguin.Stat.RemoveStat(GetBuffValue(), buffStatType, StatMode.Increase);
                EndBuffEffect(coll, penguin);
            });
        }
    }

    private void EndBuffEffect(Collider coll, Penguin penguin)
    {
        penguin.StrengthBuffEffect?.Stop();

        RemoveExitTargetList(coll);

        CheckEnterTarget();
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
