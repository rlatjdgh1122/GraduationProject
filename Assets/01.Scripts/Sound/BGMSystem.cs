using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� ���̺��� ����� �ٲ��ִ� ��Ȱ
/// </summary>

[Serializable]
public struct SoundDesignation
{
    public int wave;            //BGM�� �ٲ� ���̺�
    public SoundName bgmName;   //�ٲ� BGM
}

public class BGMSystem : MonoBehaviour
{
    [SerializeField] private SoundName _normalBGM;
    [SerializeField] private SoundName _normalBattleBGM;
    public List<SoundDesignation> SoundList;

    private SoundName _currentBGM;

    private void Awake()
    {
        //SoundManager.Play2DSound(_normalBGM, SoundType.BGM);
        //_currentBGM = _normalBGM;
        //InputButtonSfx();
    }

    private void Start()
    {
        /*SoundManager.Play2DSound(_normalBGM, SoundType.BGM);
        _currentBGM = _normalBGM;*/
    }

    private void InputButtonSfx()
    {
        foreach (Button allBtn in FindObjectsOfType<Button>())
        {
            allBtn.onClick.AddListener(() => { SoundManager.Play2DSound(SoundName.Button); });
        }
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += StartBattleBGM;
        SignalHub.OnBattlePhaseEndEvent += EndBattleBGM;

        SoundManager.Play2DSound(_normalBGM, SoundType.BGM);
        _currentBGM = _normalBGM;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= StartBattleBGM;
        SignalHub.OnBattlePhaseEndEvent -= EndBattleBGM;

        SoundManager.ResetSound();
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
