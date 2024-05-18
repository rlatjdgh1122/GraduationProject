using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralSelection : GeneralPopupUI
{
    public TextMeshProUGUI generalText;
    public TextMeshProUGUI synergyText;

    private Ability ability => presenter.selectedAbility;
    private GeneralStat generalStat => presenter.currentGeneralStat;

    public override void Awake()
    {
        base.Awake();
    }

    public void UpdateUI()
    {
        generalText.text = $"군단장의 {ability.abilityName} {ability.value}% 증가";

        synergyText.text = $"{generalStat.GeneralDetailData.synergy.synergyName}\n Lv {generalStat.GeneralDetailData.synergy.level} -> Lv {generalStat.GeneralDetailData.synergy.level + 1}"; 
    }

    public void SelectGeneralBox()
    {
        presenter.SelectGeneralBox();
    }

    public void SelectSynergyBox()
    {
        presenter.SelectSynergyBox();
    }

    public override void ShowPanel()
    {
        UpdateUI();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
        presenter.OnUpdateUpgradeUI?.Invoke();
    }
}
