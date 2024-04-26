using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseFeedback : Feedback
{
    [SerializeField]
    private LayerMask _noiseLayer;

    [SerializeField]
    private float _detectRange;

    public override void CreateFeedback()
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, _noiseLayer);

        float value = 0;
        string name = string.Empty;

        if (raycastSuccess)
        {
            Debug.Log(raycastHit);
        }

        //NoiseManager.Instance.IncreaseNoise(name, value);
    }

    public override void FinishFeedback()
    {
    }
}