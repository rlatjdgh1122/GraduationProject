using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : Singleton<NoiseManager>
{
    [SerializeField] 
    private float _initMaxNosise = 100f;

    [SerializeField]
    private float _increaseMaxNoise = 1.2f;

    private float _maxNosise;
    public float MaxNoise => _maxNosise;

    private float _currentNoise = 0f;
    public float CurrentNoise => _currentNoise;

    private float _currentViewNoise = 0f;
    public float CurrentViewNoise => _currentViewNoise;

    public float ViewNoisePercent => _currentViewNoise / _maxNosise;
    public float NoisePercent => _currentNoise / _maxNosise;

    public Action NoiseLimitExceedEvent = null;
    public Action ViewNoiseIncreaseEvent = null;
    public Action NoiseIncreaseEvent = null;

    private PhaseChangeButton _btn;

    public override void Awake()
    {
        _maxNosise = _initMaxNosise;
        _btn = FindObjectOfType<PhaseChangeButton>();

        AddViewNoise(_currentViewNoise);
    }

    /// <summary>
    /// 레벨에 따라 소음 최대치 변경
    /// </summary>
    /// <param name="level">현재 레벨(넥서스)</param>
    public void IncreaseMaxNoise(int level)
    {
        float geometricSequence = Mathf.Pow(_increaseMaxNoise, level - 1);

        _maxNosise = _initMaxNosise * geometricSequence;
    }

    /// <summary>
    /// 현재 소음에 값 더하기
    /// </summary>
    /// <param name="noise">더해질 소음 값</param>
    public void AddViewNoise(float noise)
    {
        _currentViewNoise += noise;

        ViewNoiseIncreaseEvent?.Invoke();

        if (_currentViewNoise >= _maxNosise)
            NoiselimitExceed();
    }

    public void AddNoise(float nosie)
    {
        _currentNoise += nosie;

        NoiseIncreaseEvent?.Invoke();

        if (_currentNoise >= _maxNosise)
            NoiselimitExceed();
    }

    /// <summary>
    /// 소음 값이 최대치를 넘으면
    /// </summary>
    public void NoiselimitExceed()
    {
        NoiseLimitExceedEvent?.Invoke();

        _btn.ChangePhase();

        ResetNoise();
    }

    private void ResetNoise()
    {
        _currentViewNoise = 0;
        _currentNoise = 0;
    }
}
