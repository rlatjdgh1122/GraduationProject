using Define.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioVolumeController : MonoBehaviour
{
    private AudioMixer _mainMixer;

    private Slider _sfxSlider;
    private Slider _bgmSlider;
    private Slider _masterSlider;

    private void Awake()
    {
        _mainMixer = VResources.Load<AudioMixer>("Audio/MainMixer");

        _sfxSlider = transform.Find("SFXSlider").GetComponent<Slider>();
        _bgmSlider = transform.Find("BGMSlider").GetComponent<Slider>();
        _masterSlider = transform.Find("MasterSlider").GetComponent<Slider>();

        InitSlider();
    }

    private void InitSlider()
    {
        _sfxSlider.onValueChanged.AddListener(SFXVolume);
        _bgmSlider.onValueChanged.AddListener(BGMVolume);
        _masterSlider.onValueChanged.AddListener(MasterVolume);
    }

    public void SFXVolume(float volume)
    {
        _mainMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
    public void BGMVolume(float volume)
    {
        _mainMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }
    public void MasterVolume(float volume)
    {
        _mainMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }
}