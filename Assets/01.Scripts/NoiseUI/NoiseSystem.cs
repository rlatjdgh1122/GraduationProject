using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseSystem : MonoBehaviour
{
    [SerializeField]
    private float maxNosise = 100f;
    [SerializeField]
    private float currentNoise = 0f;
    [SerializeField]
    Slider _noiseSlider;

    private void Start()
    {
        _noiseSlider.value = 0;
    }

    private void Update()
    {
        if(maxNosise <= currentNoise)
        {
            //전투시작
        }
    }


    public void IncreaseNoise(float value)
    {
        currentNoise += value;
        _noiseSlider.value += value / 100f;
    }

    public void ResetNoise()
    {
        currentNoise = 0;
        _noiseSlider.value = currentNoise;
    }
}
