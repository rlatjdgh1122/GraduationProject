using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LunarEclipseUI : WorldUI
{
    [SerializeField]
    private Color _beforeGlow;

    [SerializeField]
    private Color _afterGlow = Color.red;

    [SerializeField]
    private float _localMoveXValue;

    [SerializeField] private Image _glowImge;
    [SerializeField] private Image _redMoonImage;

    [SerializeField] private float _duration;

    [SerializeField]
    private UnityEvent OnEndEclipseEvent;

    public Sequence HudTextSequence;

    public override void Update()
    {
        base.Update();
    }

    public void StartEclipse()
    {
        HudTextSequence?.Kill(); // 기존 시퀀스를 중단
        HudTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당

        HudTextSequence
            .Append(canvas.DOFade(1, 0.7f))
            .Join(_redMoonImage.DOFade(1, 0.5f))
            .Join(_redMoonImage.transform.DOLocalMoveX(0, _duration))
            .Join(_redMoonImage.DOColor(Color.white, 7f))
            .Join(_glowImge.DOColor(_afterGlow, _duration))
            .AppendInterval(2.3f) //나중에 이부분 지워야함
            .AppendCallback(() => EndEclipse(_duration));
    }

    public void EndEclipse(float duration)
    {
        HudTextSequence?.Kill(); // 기존 시퀀스를 중단
        HudTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당

        OnEndEclipseEvent?.Invoke();

        HudTextSequence
            .Append(_redMoonImage.DOFade(0, duration))
            .Join(_redMoonImage.transform.DOLocalMoveX(_localMoveXValue, duration))
            .Join(_glowImge.DOColor(_beforeGlow, duration))
            .Join(_redMoonImage.DOColor(Color.black, 7f))
            .Join(canvas.DOFade(0, duration))
            .Append(_redMoonImage.transform.DOLocalMoveX(-_localMoveXValue,0)); //다시 원래 자리로 옮기기
    }
}
