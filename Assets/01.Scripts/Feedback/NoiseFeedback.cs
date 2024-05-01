using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

public class NoiseFeedback : Feedback
{
    [SerializeField]
    private LayerMask _noiseLayer;

    [SerializeField]
    private float _detectRange;

    public override bool StartFeedback()
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, _noiseLayer);

        foreach (var col in Colls)
        {
            if(col.TryGetComponent<WorkableObject>(out WorkableObject obj))
            {
                NoiseManager.Instance.AddNoise(obj.NoiseValue);
            }
        }

        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}