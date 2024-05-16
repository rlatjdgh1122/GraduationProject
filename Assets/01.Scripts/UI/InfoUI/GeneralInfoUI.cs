using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralInfoUI : PenguinInfoUI
{
    public readonly char Cashing_Separator = ',';
    private GeneralStat stat => PenguinManager.Instance.GetCurrentStat as GeneralStat;

    private GeneralInfoDataSO info => stat.InfoData;

    [Header("장군 추가 정보")]
    [SerializeField] private TextMeshProUGUI _levelTxt = null;
    [SerializeField] private TextMeshProUGUI _synergyTxt = null;

    protected override void ShowInfo()
    {
        base.ShowInfo();

        var armyStat = stat.GeneralDetailData.abilities[0];
        var synergyText = info.Synergy.Split(Cashing_Separator);

        _synergyTxt.text = $"{synergyText[0]}<color=green>{armyStat.value}%</color>{synergyText[1]}";
        _levelTxt.text = $"{stat.Level}lv";
    }

    public override void HideInfoUI()
    {
        UIManager.Instance.HidePanel("GeneralInfoUI");
    }
}
