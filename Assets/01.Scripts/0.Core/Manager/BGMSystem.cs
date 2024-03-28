using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 전투 웨이브의 배경을 바꿔주는 역활
/// </summary>

[Serializable]
public struct SoundDesignation
{
    public int wave;            //BGM이 바뀔 웨이브
    public SoundName bgmName;   //바뀔 BGM
}

public class BGMSystem : MonoBehaviour
{
    [SerializeField] private SoundName _normalBGM;
    [SerializeField] private SoundName _normalBattleBGM;
    public List<SoundDesignation> SoundList;

    private SoundName _currentBGM;

    private void Awake()
    {
        SoundManager.Play2DSound(_normalBGM, SoundType.BGM);
        _currentBGM = _normalBGM;
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += StartBattleBGM;
        SignalHub.OnBattlePhaseEndEvent += EndBattleBGM;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= StartBattleBGM;
        SignalHub.OnBattlePhaseEndEvent -= EndBattleBGM;
    }

    public void StartBattleBGM()
    {
        SoundName name = _normalBattleBGM;

        foreach (var sound in SoundList)
        {
            if (sound.wave == WaveManager.Instance.CurrentWaveCount)
            {
                name = sound.bgmName;
            }
        }

        BGMChange(name);
    }

    public void EndBattleBGM()
    {
        BGMChange(_normalBGM);
    }

    public void BGMChange(SoundName name)
    {
        SoundManager.StopBGM(_currentBGM);
        SoundManager.Play2DSound(name, SoundType.BGM);

        _currentBGM = name;
    }
}
