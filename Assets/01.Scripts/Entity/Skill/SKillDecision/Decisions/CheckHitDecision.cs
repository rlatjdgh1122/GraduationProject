public class CheckHitDecision : SKillDecision
{
    public int MaxHitCount = 3;

    private int _hitCount = 0;

    private void Start()
    {
        _hitCount = -MaxHitCount; //ó������ ��밡���ϰ�
    }

    public override void Init()
    {
        _hitCount = _actionData.HitCount;
    }

    public override bool MakeDecision()
    {
        return _hitCount + MaxHitCount <= _actionData.HitCount;
    }
}
