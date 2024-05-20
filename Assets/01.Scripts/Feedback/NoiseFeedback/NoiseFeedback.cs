using UnityEngine;

public class NoiseFeedback : Feedback
{
    [SerializeField]
    private float _detectRange;

    [SerializeField]
    private LayerMask _targetLayer;

    [SerializeField]
    private WorkerType _workerType;

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

        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, _targetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent(out WorkableObject obj))
        {
            if (_workerType == WorkerType.Builder)
                obj.IncreaseCurrentNoise();
            else
            {
                _noiseManager.AddNoise(obj.NoiseValue);
                _noiseManager.AddViewNoise(obj.NoiseValue);
            }
        }

        /*var Colls = Physics.OverlapSphere(transform.position, _detectRange, _targetLayer);
        foreach (var col in Colls)
        {
            if (col.TryGetComponent<WorkableObject>(out WorkableObject obj))
            {
                if(_workerType == WorkerType.Builder)
                    obj.IncreaseCurrentNoise();
                else
                {
                    _noiseManager.AddNoise(obj.NoiseValue);
                    _noiseManager.AddViewNoise(obj.NoiseValue);
                }
            }
        }*/

        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.forward * _detectRange);
    }
#endif
}
