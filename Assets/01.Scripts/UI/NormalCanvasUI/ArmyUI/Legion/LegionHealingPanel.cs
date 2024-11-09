using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;

public class LegionHealingPanel : ArmyComponentUI
{
    [SerializeField] private int _generalHealingCost;
    [SerializeField] private int _healingCost;

    [SerializeField] private TextMeshProUGUI _text;
    private Penguin _penguin;
    private EntityInfoDataSO _infoData;
    private Action _action;

    public void HealingPenguin<T>(T penguin, EntityInfoDataSO infoData, Action action) where T : Penguin
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
        _infoData = infoData;
        _penguin = penguin;
    }

    public void Heal()
    {
        if (_penguin is General)
        {
            if (_generalHealingCost > CostManager.Instance.Cost)
            {
                UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�.");
                return;
            }

            UIManager.Instance.ShowWarningUI("�屺�� ��Ƴ����ϴ�!");
            CostManager.Instance.SubtractFromCurrentCost(_generalHealingCost);
        }
        else
        {
            if (_healingCost > CostManager.Instance.Cost)
            {
                UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�.");
                return;
            }

            UIManager.Instance.ShowWarningUI("����� ��Ƴ����ϴ�!");
            CostManager.Instance.SubtractFromCurrentCost(_healingCost);
        }
        PenguinManager.Instance.ResurrectedSoldierPenguin(_penguin);

        _penguin.OnResurrected();
        _penguin.MyArmy.ResurrectPenguin(_penguin);
        _penguin.HealthCompo.SetMaxHealth();

        _penguin.gameObject.SetActive(false);


        _action?.Invoke();
        Cancel();
    }

    public void Cancel()
    {
        HidePanel();
    }
}