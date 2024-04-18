using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : Singleton<NoiseManager>
{
    [SerializeField]
    private float maxNosise = 100f;
    [SerializeField]
    private float currentNoise = 0f;
    [SerializeField]
    Slider _noiseSlider;

    public override void Awake()
    {
        base.Awake();
        _noiseSlider.value = 0;
    }

    public void IncreaseNoise(float value)
    {
        if (maxNosise <= currentNoise)
        {
            WaveManager.Instance.BattlePhaseStartEventHandler();
        }

        currentNoise += value;
        _noiseSlider.value += value / 100f;
        Debug.Log($"현재 value는 {value}, 현재 currentNoise{currentNoise}");
    }

    public void ResetNoise()
    {
        currentNoise = 0;
        _noiseSlider.value = currentNoise;
    }
}
