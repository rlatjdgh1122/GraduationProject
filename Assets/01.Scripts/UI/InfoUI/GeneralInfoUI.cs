using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralInfoUI : PenguinInfoUI
{
    private GeneralStat stat => PenguinManager.Instance.GetCurrentStat as GeneralStat;

    private GeneralInfoDataSO info => stat.InfoData;

    [Header("장군 추가 정보")]
    [SerializeField] private TextMeshProUGUI _levelTxt = null;
    [SerializeField] private TextMeshProUGUI _synergyTxt = null;

    protected override void ShowInfo()
    {
        base.ShowInfo();

        _synergyTxt.text = info.Synergy;
        _levelTxt.text = $"{stat.Level.ToString()}lv";
    }

    public override void HideInfoUI()
    {
        UIManager.Instance.HidePanel("GeneralInfoUI");
    }
}
