using UnityEngine;

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
        }
    }

    protected override void ExitTarget(Collider coll)
    {
        if (coll.gameObject.TryGetComponent(out Penguin penguin))
        {
            StartCoroutine(penguin.RemoveStatCorou(OutoffRangeBuffDuration, GetBuffValue(), buffStatType, buffStatMode,
                () => EndBuffEffect(coll)));

            EffectPlayer buffEffect = PoolManager.Instance.Pop(_penguinEffect) as EffectPlayer;

            buffEffect.transform.SetParent(penguin.transform);
            buffEffect.transform.localPosition = Vector3.zero;
            buffEffect.transform.rotation = Quaternion.identity;

            var main = buffEffect.Particles[0].main;
            main.startSize = 0.1f;

            buffEffect.StartPlay(OutoffRangeBuffDuration);
        }
    }

    private void EndBuffEffect(Collider coll)
    {
        AddExitTargetList(coll);
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
