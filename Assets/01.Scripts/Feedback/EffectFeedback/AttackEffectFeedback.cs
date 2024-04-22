using UnityEngine;
public class AttackEffectFeedback : Feedback
{
    [SerializeField] private float _endTime;

    private int _calledNum = 0;

    public override void StartFeedback()
    {
        _calledNum++;

        if (_calledNum > 2)
            _calledNum = 1;

        EffectPlayer effect = PoolManager.Instance.Pop($"SlashEffect0{_calledNum}") as EffectPlayer;
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;
        effect.StartPlay(_endTime);
    }

    public override void FinishFeedback()
    {
        
    }
}
