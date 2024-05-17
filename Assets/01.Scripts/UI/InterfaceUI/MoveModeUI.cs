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
    [Header("��ġ")]
    public float FadeInDuration = 1.2f;
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
