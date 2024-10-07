using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchEffectController : MonoBehaviour
{
    [Header("Material")]
    [SerializeField] private Material _glitchMat;

    [Header("Screen Setting")]
    [SerializeField] private float _duration;
    [SerializeField] private float _waitTime;

    private Sequence _sequence;

    private void ResetSequence()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
    }

    public void SetValue(float amount, float strength = 0.7f)
    {
        _glitchMat.SetFloat("_NoiseAmount", amount);
        _glitchMat.SetFloat("_ScreenLinesStrength", strength);
    }

    public void ReessetValue()
    {
        _glitchMat.SetFloat("_NoiseAmount", 0);
        _glitchMat.SetFloat("_ScreenLinesStrength", 1);
    }

    public void StartScreen(float endValue, Action action = null)
    {
        ResetSequence();

        SoundManager.Play2DSound(SoundName.Glitch);

        _sequence.PrependInterval(_waitTime)
                 .Append(_glitchMat.DOFloat(endValue, "_NoiseAmount", _duration))
                 .AppendCallback(() =>
                 {
                     action?.Invoke();
                 });
    }

    public void EndScreen(float endValue, Action action = null)
    {
        ResetSequence();

        SoundManager.Play2DSound(SoundName.Glitch);

        _sequence.Append(_glitchMat.DOFloat(endValue, "_NoiseAmount", _duration))
                 .AppendInterval(_waitTime)
                 .AppendCallback(() =>
                 {
                     action?.Invoke();
                 });
    }

    private void OnDisable()
    {
        _glitchMat.SetFloat("_ScreenLinesStrength", 1.0f);
    }
}
