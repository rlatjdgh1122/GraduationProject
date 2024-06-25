using ArmySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatusSlot : MonoBehaviour
{
    private Army _army = null;

    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    public void SetArmy(Army army)
    {
        _army = army;

        _army.OnLegionNameChanged += LegionNameChangedHandler;
    }

    private void OnDisable()
    {
        if (_army != null)
            _army.OnLegionNameChanged -= LegionNameChangedHandler;

    }

    private void LegionNameChangedHandler(string prevValue, string curValue)
    {
        _legionNameTxt.text = $"{curValue}";
    }
}
