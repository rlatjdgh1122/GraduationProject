public class NoiseEffectFeedback : EffectFeedback
{
    private NoiseManager _noiseManager;

    protected override void Start()
    {
        base.Start();
        _noiseManager = NoiseManager.Instance;
    }

    public override void StartFeedback()
    {
        _noiseManager.IncreaseNoise(20f);
    }

    public override void FinishFeedback()
    {
    }
}
