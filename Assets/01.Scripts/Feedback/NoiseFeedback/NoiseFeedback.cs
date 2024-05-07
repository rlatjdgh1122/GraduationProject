using UnityEngine;

public class NoiseFeedback : Feedback
{
    [SerializeField]
    private float _detectRange;

    [SerializeField]
    private LayerMask _targetLayer;

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

        var Colls = Physics.OverlapSphere(transform.position, _detectRange, _targetLayer);
        foreach (var col in Colls)
        {
            if (col.TryGetComponent<WorkableObject>(out WorkableObject obj))
            {
                obj.IncreaseCurrentNoise();
            }
        }

        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
