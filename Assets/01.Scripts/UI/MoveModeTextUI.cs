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
            _text.text = $"������ : ���� �������";
        }
        else
        {
            _text.text = $"������ : ��� �������";
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
