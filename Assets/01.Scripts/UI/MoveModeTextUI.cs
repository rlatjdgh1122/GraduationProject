using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
public class MoveModeTextUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

        SignalHub.OnBattleModeChanged += OnBattleModeChangedHandler;
    }

    private void OnBattleModeChangedHandler(MovefocusMode mode)
    {
        if (mode == MovefocusMode.Battle)
        {
            _text.text = $"움직임 : 전투 중점모드";
        }
        else
        {
            _text.text = $"움직임 : 명령 중점모드";
        }
    }

    private void OnDestroy()
    {
        //SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
    }

    private void OnDisable()
    {
        SignalHub.OnBattleModeChanged -= OnBattleModeChangedHandler;
    }
}
