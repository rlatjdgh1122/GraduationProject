using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseManager : Singleton<NoiseManager>
{
    [SerializeField]
    private PhaseChangeButton _btn;


    [SerializeField] 
    private float _initMaxNosise = 100f;

    [SerializeField]
    private float _increaseMaxNoise = 1.2f;

    private float _maxNosise;
    public float MaxNoise => _maxNosise;

    private float _currentNoise = 0f;
    public float CurrentNoise => _currentNoise;

    public float NoisePercent => _currentNoise / _maxNosise;

    public Action NoiseLimitExceedEvent = null;
    public Action NoiseIncreaseEvent = null;

    public override void Awake()
    {
        _maxNosise = _initMaxNosise;

        AddNoise(_currentNoise);
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

    /// <summary>
    /// ���� ������ �� ���ϱ�
    /// </summary>
    /// <param name="noise">������ ���� ��</param>
    public void AddNoise(float noise)
    {
        _currentNoise += noise;

        NoiseIncreaseEvent?.Invoke();

        if (_currentNoise >= _maxNosise)
            NoiselimitExceed();
    }

    /// <summary>
    /// ���� ���� �ִ�ġ�� ������
    /// </summary>
    public void NoiselimitExceed()
    {
        NoiseLimitExceedEvent?.Invoke();

        _btn.ChangePhase();
        //WaveManager.Instance.BattlePhaseStartEventHandler();

        ResetNoise();
    }

    public void ResetNoise()
    {
        _currentNoise = 0;
    }
}
