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
    public float Strength { get; private set; }
    public float Alpha    { get; private set; }

    private Sequence _sequence;

    private void ResetSequence()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();
    }

    public void SetValue(float amount, float strength, float alpha)
    {
        Amount = amount;
        Strength = strength;
        Alpha = alpha;

        _glitchMat.SetFloat("_NoiseAmount", Amount);
        _glitchMat.SetFloat("_GlitStrength", Strength);
        _glitchMat.SetFloat("_ScreenLinesStrength", Alpha);
    }

    public void StartScreen(Action action = null)
    {
        ResetSequence();

        _sequence.PrependInterval(_waitTime)
                 .Append(_glitchMat.DOFloat(Amount,   "_NoiseAmount",         _duration))
                 .Join  (_glitchMat.DOFloat(Strength, "_GlitStrength",        _duration))
                 .Join  (_glitchMat.DOFloat(Alpha,    "_ScreenLinesStrength", _duration))
                 .AppendCallback(() =>
                 {
                     action?.Invoke();
                 });
    }

    public void EndScreen(Action action = null)
    {
        ResetSequence();

        _sequence.PrependInterval(_waitTime)
                .Append(_glitchMat.DOFloat(Amount,   "_NoiseAmount",         _duration))
                .Join  (_glitchMat.DOFloat(Strength, "_GlitStrength",        _duration))
                .Join  (_glitchMat.DOFloat(Alpha,    "_ScreenLinesStrength", _duration))
                .AppendCallback(() =>
                {
                    action?.Invoke();
                });
    }
}
