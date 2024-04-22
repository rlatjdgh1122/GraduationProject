using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : Singleton<NoiseManager>
{
    [SerializeField] private float maxNosise = 100f;
    [SerializeField] private float currentNoise = 0f;
    [SerializeField] private Image _noisebar;

    private Sequence _fadeSequence;

    public override void Awake()
    {
        base.Awake();
        _noisebar.fillAmount = 0;
    }

    public void IncreaseNoise(float value)
    {
        if (maxNosise <= currentNoise)
        {
            WaveManager.Instance.BattlePhaseStartEventHandler();
        }

        currentNoise += value;
        //_noisebar.fillAmount += value / 100f;
        _noisebar.DOFillAmount(currentNoise / maxNosise, 1f);
    }

    public void ResetNoise()
    {
        currentNoise = 0;
        _noisebar.fillAmount = currentNoise;
    }
}
