using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegionNameTextUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();

    }

    private void OnEnable()
    {
        SignalHub.OnArmyChanged += OnArmyChangedHandler;
    }

    private void OnArmyChangedHandler(Army prevArmy, Army newArmy)
    {
        _text.text = $"{newArmy.LegionName}";
    }

    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
