using TMPro;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

public class GeneralUpgrade : GeneralPopupUI
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Image icon;
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
        nameText.text = generalStat.InfoData.PenguinName;
        levelText.text = $"Lv {generalStat.GeneralDetailData.level}";
        icon.sprite = generalStat.InfoData.PenguinIcon;
        hp.value = generalStat.InfoData.hp;
        atk.value = generalStat.InfoData.atk;
        range.value = generalStat.InfoData.range;
        synergyText.text = generalStat.GeneralDetailData.synergy.synergyName;
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
