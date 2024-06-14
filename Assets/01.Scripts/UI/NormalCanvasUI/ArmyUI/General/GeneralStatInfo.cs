using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralStatInfo : ArmyComponentUI
{
    [Header("UI")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _defText;
    [SerializeField] private TextMeshProUGUI _atkText;
    [SerializeField] private TextMeshProUGUI _rngText;
    [SerializeField] private TextMeshProUGUI _skillNameText;
    [SerializeField] private TextMeshProUGUI _skillDescriptionText;

    public override void Awake()
    {
        base.Awake();

        OnShowGeneralInfo += ShowInfoLogic;
        OnHideGeneralInfo += HideInfoLogic;
    }

    public void ShowInfoLogic(GeneralStat data)
    {
        _icon.sprite = data.InfoData.PenguinIcon;
        _nameText.text = data.InfoData.PenguinName;
        _defText.text = data.InfoData.hpRank;
        _atkText.text = data.InfoData.atkRank;
        _rngText.text = data.InfoData.rangeRank;
        _skillNameText.text = data.InfoData.SkillName;
        _skillDescriptionText.text = data.InfoData.SkillDescription;

        ShowPanel();
    }

    public void HideInfoLogic()
    {
        HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    protected virtual void OnDestroy()
    {
        OnShowGeneralInfo -= ShowInfoLogic;
        OnHideGeneralInfo -= HideInfoLogic;
    }
}
