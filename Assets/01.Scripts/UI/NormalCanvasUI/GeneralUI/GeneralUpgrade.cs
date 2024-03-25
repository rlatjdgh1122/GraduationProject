using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class GeneralUpgrade : GeneralPopupUI
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hp;
    public Slider atk;
    public Slider range;
    public TextMeshProUGUI synergyText;

    private GeneralStat generalStat => presenter.currentGeneralStat;

    public override void Awake()
    {
        base.Awake();
    }

    public void OnUpgrade()
    {
        presenter.Upgrade();
    }

    private void UpdateUI()
    {
        nameText.text = generalStat.PenguinName;
        levelText.text = $"Lv {generalStat.GeneralData.level}";
        hp.value = generalStat.PenguinData.hp;
        atk.value = generalStat.PenguinData.atk;
        range.value = generalStat.PenguinData.range;
        synergyText.text = generalStat.GeneralData.synergy.synergyName;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        presenter.OnUpdateUpgradeUI += UpdateUI;
        UpdateUI();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        presenter.OnUpdateUpgradeUI -= UpdateUI;
    }
}
