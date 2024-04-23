public class NoiseEffectFeedback : EffectFeedback
{
    private NoiseManager _noiseManager;

    protected override void Start()
    {
        base.Start();
        _noiseManager = NoiseManager.Instance;
    }

    public override bool StartFeedback()
    {
        if (_noiseManager == null)
            return false;
        _noiseManager.IncreaseNoise(20f);

        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
