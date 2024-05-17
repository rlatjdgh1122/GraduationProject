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
    [Header("��ġ")]
    public float WaitFadeInTime = 0.5f;
    public float FadeInDuration = 1.2f;
    public float WaitFadeOutTime = 0.5f;
    public float FadeOutDuration = 0.5f;

    [Header("������Ʈ")]
    public TextMeshProUGUI MoveModeText = null;
    public Image Icon = null;
    public Image BackGround = null;

    [Header("�����")]
    public Sprite BattleIcon = null;
    public Sprite CommandIcon = null;

    [Header("��ƾ�")]
    public Color BattleColor = Color.white;
    public Color CommandColor = Color.white;

    //A42A20 - ��
    //2D5C34 - ��
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
            MoveModeText.text = "���� ���";
            Icon.sprite = BattleIcon;
            BackGround.color = BattleColor;
        }
        else
        {
            MoveModeText.text = "��� ���";
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
