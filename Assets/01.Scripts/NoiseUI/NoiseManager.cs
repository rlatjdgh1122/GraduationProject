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

    private float _saveNoiseValue = 0;

    private PhaseChangeButton _btn;

    public override void Awake()
    {
        _maxNosise = _initMaxNosise;
        _btn = FindObjectOfType<PhaseChangeButton>();

        AddViewNoise(_currentViewNoise);
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += PhaseStartAddSaveNoise;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= PhaseStartAddSaveNoise;
    }

    /// <summary>
    /// ������ ���� ���� �ִ�ġ ����
    /// </summary>
    /// <param name="level">���� ����(�ؼ���)</param>
    public void IncreaseMaxNoise(int level)
    {
        float geometricSequence = Mathf.Pow(_increaseMaxNoise, level - 1);

        _maxNosise = _initMaxNosise * geometricSequence;
    }

    public void PhaseStartAddSaveNoise()
    {
        AddNoise(_saveNoiseValue);

        if (_currentNoise >= _maxNosise)
        {
            _saveNoiseValue = _currentNoise - _maxNosise;
        }
        else
        {
            _saveNoiseValue = 0;
        }
    }

    /// <summary>
    /// ���� ������ �� ���ϱ�
    /// </summary>
    /// <param name="noise">������ ���� ��</param>
    public void AddViewNoise(float noise)
    {
        _currentViewNoise += noise;

        if(_currentViewNoise > _currentNoise)
            _currentViewNoise = _currentNoise;

        if (_currentViewNoise >= _maxNosise)
            NoiselimitExceed();

        SignalHub.OnViewNoiseIncreaseEvent?.Invoke();
    }

    public void AddNoise(float noise)
    { 
        _currentNoise += noise;

        if (_currentNoise >= _maxNosise)
        {
            _saveNoiseValue = _currentNoise - _maxNosise;
        }

        SignalHub.OnNoiseIncreaseEvent?.Invoke();
    }

    public void SaveNoise()
    {
        _saveNoiseValue = _currentNoise - _currentViewNoise;

        ResetNoise();
    }

    /// <summary>
    /// ���� ���� �ִ�ġ�� ������
    /// </summary>
    public void NoiselimitExceed()
    {
        _btn.ChangePhase();

        ResetNoise();
    }

    private void ResetNoise()
    {
        _currentViewNoise = 0;
        _currentNoise = 0;
    }
}
