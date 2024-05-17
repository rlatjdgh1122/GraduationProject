using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SynergyUI : MonoBehaviour
{
    public string ColorName = "Green";
    private TextMeshProUGUI _text;

    private Synergy _synergy = null;


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
        if (newArmy.General)
        {
            _synergy = newArmy.General.ReturnGenericStat<GeneralStat>().GeneralDetailData.synergy;

            _text.text = 
                $"{_synergy.synergyName} - {_synergy.Stat.abilityName} <color={ColorName}>{_synergy.Stat.Value}%</color> 증가";
        }
        else
        {
            _text.text = $"군단장을 보유하고 있지 않음";
        }
    }

    private void OnDisable()
    {
        SignalHub.OnArmyChanged -= OnArmyChangedHandler;
    }
}
