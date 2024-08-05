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

    public float Amount   { get; private set; }

    private Sequence _sequence;

    private void ResetSequence()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
    }

    public void SetValue(float amount)
    {
        Amount = amount;

        _glitchMat.SetFloat("_NoiseAmount", Amount);
    }

    public void StartScreen(float endValue, Action action = null)
    {
        ResetSequence();

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

        _sequence.Append(_glitchMat.DOFloat(endValue, "_NoiseAmount", _duration))
                 .AppendInterval(_waitTime)
                 .AppendCallback(() =>
                 {
                     action?.Invoke();
                 });
    }
}
