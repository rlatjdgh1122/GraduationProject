using TMPro;

public class NexusPanel : NexusPopupUI
{
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    public TextMeshProUGUI currentHp;
    public TextMeshProUGUI nextHp;
    public TextMeshProUGUI upgradePrice;

    private NexusStat nexusStat => presenter.nexusBase.NexusStat;

    public override void Awake()
    {
        base.Awake();

        presenter.OnUpdateNexusUI += UpdateUI;
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentLevel.text = $"Lv {nexusStat.level}";
        nextLevel.text = $"Lv {nexusStat.level + 1}";
        currentHp.text = $"{nexusStat.GetMaxHealthValue()}";
        nextHp.text = $"{nexusStat.GetUpgradedMaxHealthValue()}";
        upgradePrice.text = $"{nexusStat.upgradePrice}";
    }

    public void OnLevelUp()
    {
        presenter.LevelUp();
    }

    private void OnDestroy()
    {
        presenter.OnUpdateNexusUI -= UpdateUI;
    }
}
