using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
using UnityEngine.UI;
using DG.Tweening;
public class MoveModeUI : MonoBehaviour
{
    [Header("수치")]
    public float FadeInDuration = 1.2f;
    public float FadeOutDuration = 0.5f;

    [Header("컴포넌트")]
    public TextMeshProUGUI MoveModeText = null;
    public Image Icon = null;
    public Image BackGround = null;

    [Header("으어엉")]
    public Sprite BattleIcon = null;
    public Sprite CommandIcon = null;

    [Header("우아앙")]
    public Color BattleColor = Color.white;
    public Color CommandColor = Color.white;

    //A42A20 - 빨
    //2D5C34 - 초

    private CanvasGroup _canvasGroup = null;

    private void OnEnable()
    {
        SignalHub.OnBattleModeChanged += OnBattleModeChangedHandler;
        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEndHandler;
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        Init();
    }

    private void Init()
    {
        _canvasGroup.DOFade(0, FadeOutDuration);
    }

    private void OnBattleModeChangedHandler(MovefocusMode mode)
    {
        if (mode == MovefocusMode.Battle)
        {
            MoveModeText.text = "전투 모드";
            Icon.sprite = BattleIcon;
            BackGround.color = BattleColor;
        }
        else
        {
            MoveModeText.text = "명령 모드";
            Icon.sprite = CommandIcon;
            BackGround.color = CommandColor;
        }
    }

    private void OnBattleStartHandler()
    {
        _canvasGroup.DOFade(1, FadeInDuration);
    }

    private void OnBattleEndHandler()
    {
        _canvasGroup.DOFade(0, FadeOutDuration);
    }



    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEndHandler;
    }
}
