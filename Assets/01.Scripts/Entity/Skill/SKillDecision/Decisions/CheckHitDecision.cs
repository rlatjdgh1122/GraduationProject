public class CheckHitDecision : SKillDecision
{
    public int MaxHitCount = 3;

    private int _hitCount = 0;
    private int _saveHitCount = 0;

    private void Start()
    {
        _hitCount = -MaxHitCount; //처음에도 사용가능하게
    }

    public override void ResetValue()
    {
        _hitCount = 0;
        _saveHitCount = _actionData.HitCount;
    }

    public override bool MakeDecision()
    {
        return _hitCount + MaxHitCount <= _actionData.HitCount - _saveHitCount;
    }

    public override float GetDecisionValue()
    {
        return _hitCount;
    }
}
