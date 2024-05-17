using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;
public class MoveModeUI : MonoBehaviour
{
    [Header("수치")]
    public float WaitFadeInTime = 0.5f;
    public float FadeInDuration = 1.2f;
    public float WaitFadeOutTime = 0.5f;
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
    private RectTransform _trm = null;

    private void OnEnable()
    {
        SignalHub.OnBattleModeChanged += OnBattleModeChangedHandler;
        SignalHub.OnBattlePhaseStartEvent += OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEndHandler;
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _trm = GetComponent<RectTransform>();
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
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.HudTextSequence
            .PrependInterval(WaitFadeInTime)
            .Append(_trm.DOAnchorPosY(-30, FadeInDuration));

        //_canvasGroup.DOFade(1, FadeInDuration);
    }

    private void OnBattleEndHandler()
    {
        UIManager.Instance.InitializHudTextSequence();
        UIManager.Instance.HudTextSequence
            .PrependInterval(WaitFadeOutTime)
            .Append(_trm.DOAnchorPosY(180, FadeOutDuration));
        // _canvasGroup.DOFade(0, FadeOutDuration);
    }



    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStartHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEndHandler;
    }
}
