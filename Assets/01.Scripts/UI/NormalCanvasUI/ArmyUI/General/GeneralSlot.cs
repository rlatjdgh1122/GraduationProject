using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSlot : ArmyComponentUI
{
    [SerializeField] private GeneralStat _generalData;
    [SerializeField] private Image _icon;

    private void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        _icon.sprite = _generalData.InfoData.PenguinIcon;
    }

    public void ShowInfoPanel()
    {
        generalSlotPanel.HidePanel();
        ShowGeneralInfo(_generalData);
    }
}