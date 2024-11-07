using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class LegionHealingPanel : ArmyComponentUI
{
    [SerializeField] private int _generalHealingCost;
    [SerializeField] private int _healingCost;

    [SerializeField] private TextMeshProUGUI _text;
    private Penguin _penguin;
    private Action _action;

    public void HealingPenguin<T>(T penguin, Action action) where T : Penguin
    {
        if (penguin is General)
        {
            _text.text = _generalHealingCost.ToString();
        }
        else
        {
            _text.text = _healingCost.ToString();
        }

        ShowPanel();

        _action = action;
        _penguin = penguin;
    }

    public void Heal()
    {
        _penguin.OnResurrected();
        _penguin.MyArmy.ResurrectPenguin(_penguin);
        _penguin.HealthCompo.SetMaxHealth();


        if (_penguin is General)
        {
            UIManager.Instance.ShowWarningUI("¿Â±∫¿Ã ªÏæ∆≥µΩ¿¥œ¥Ÿ!");
            CostManager.Instance.SubtractFromCurrentCost(_generalHealingCost);
        }
        else
        {
            UIManager.Instance.ShowWarningUI("∆Î±œ¿Ã ªÏæ∆≥µΩ¿¥œ¥Ÿ!");
            CostManager.Instance.SubtractFromCurrentCost(_healingCost);
        }

        _action?.Invoke();
        Cancel();
    }

    public void Cancel()
    {
        HidePanel();
    }
}