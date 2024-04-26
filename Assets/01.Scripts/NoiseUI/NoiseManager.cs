using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : Singleton<NoiseManager>
{
    private Dictionary<string, float> _noiseDictionary = new();
    [SerializeField] private float _initMaxNosise = 100f;
    private float _maxNosise;
    public float MaxNoise => _maxNosise; //Max Noise

    private float _currentNoise = 0f;
    public float CurrentNoise => _currentNoise; //Current Noise

    public Action NoiseLimitExceedEvent = null;

    public override void Awake()
    {
        _maxNosise = _initMaxNosise;
    }

    public void IncreaseNoise(string name, float noise)
    {
        
    }

    public void NoiselimitExceed()
    {
        NoiseLimitExceedEvent?.Invoke();

        WaveManager.Instance.BattlePhaseStartEventHandler();
    }

    public void ResetNoise()
    {
        _currentNoise = 0;
    }
}
