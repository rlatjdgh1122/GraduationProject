using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFeedback : Feedback
{
    private NoiseManager _noiseManager;

    protected override void Start()
    {
        base.Start();
        _noiseManager = NoiseManager.Instance;
    }

    public override void CreateFeedback()
    {
        _noiseManager.IncreaseNoise(20f);
    }

    public override void FinishFeedback()
    {
    }
}
