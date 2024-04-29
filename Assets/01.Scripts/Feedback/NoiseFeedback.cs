using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NoiseFeedback : Feedback
{
    [SerializeField]
    private LayerMask _noiseLayer;

    [SerializeField]
    private float _detectRange;

    public override void CreateFeedback()
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, _noiseLayer);

        foreach (var col in Colls)
        {
            if(col.TryGetComponent<WorkableObject>(out WorkableObject obj))
            {
                NoiseManager.Instance.IncreaseNoise(obj.NoiseValue);
            }
        }
    }

    public override void FinishFeedback()
    {
    }
}