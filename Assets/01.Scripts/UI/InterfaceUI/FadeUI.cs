using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//후에 확장성 넓어서 쓰기
[RequireComponent(typeof(CanvasGroup))]
public class FadeUI : MonoBehaviour
{
    public float FadeInDuration = 1f;
    public float FadeOutDuration = 1f;

    private CanvasGroup _canvasGroup = null;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += BattlePhaseStartHandler;
        SignalHub.OnBattlePhaseEndEvent += BattlePhaseEndHandler;
    }

    private void BattlePhaseStartHandler()
    {
        _canvasGroup.DOFade(0, FadeOutDuration);
    }


    private void BattlePhaseEndHandler()
    {
        _canvasGroup.DOFade(1, FadeInDuration);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= BattlePhaseStartHandler;
        SignalHub.OnBattlePhaseEndEvent -= BattlePhaseEndHandler;
    }
}
