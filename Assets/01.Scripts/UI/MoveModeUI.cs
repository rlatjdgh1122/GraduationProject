using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
using UnityEngine.UI;
public class MoveModeUI : MonoBehaviour
{
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

    private void Awake()
    {
        SignalHub.OnBattleModeChanged += OnBattleModeChangedHandler;
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

    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
    }
}
