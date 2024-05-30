using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSlot : ArmyComponentUI
{
    [SerializeField] private GeneralStat _generalData;
    [SerializeField] private GeneralDummyPengiun _dummy;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;

    private void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        _nameText.text = $"{_generalData.InfoData.PenguinName}";
        _icon.sprite = _generalData.InfoData.PenguinIcon;
    }

    public void ShowInfo()
    {
        infoPanel.SetPurchaseInfo(_generalData, factory, _dummy);
        //new GeneralInfo(_generalData, factory, _dummy);
    }
}
