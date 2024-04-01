using TMPro;
using UnityEngine.UI;

public class NexusPanel : NexusPopupUI
{
    public TextMeshProUGUI currentLevel;
    public TextMeshProUGUI nextLevel;
    public TextMeshProUGUI currentHp;
    public TextMeshProUGUI nextHp;
    public TextMeshProUGUI currentWorkerCount;
    public TextMeshProUGUI nextWorkerCount;
    public Image buildingIcon;
    public TextMeshProUGUI upgradePrice;

    private NexusStat nexusStat => presenter.nexusBase.NexusStat;

    public override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        presenter.OnUpdateNexusUI += UpdateUI;
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentLevel.text = $"Lv {nexusStat.level}";
        nextLevel.text = $"Lv {nexusStat.level + 1}";
        currentHp.text = $"{nexusStat.GetMaxHealthValue()}";
        nextHp.text = $"{nexusStat.GetUpgradedMaxHealthValue()}";
        currentWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount}";
        nextWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount + 1}";
        buildingIcon.sprite = nexusStat.previewBuilding.UISprite;
        upgradePrice.text = $"{nexusStat.upgradePrice}";
    }

    public void OnLevelUp()
    {
        presenter.LevelUp();
    }

    public override void MovePanel(float x, float y, float fadeTime)
    {
        base.MovePanel(x, y, fadeTime);
    }

    private void OnDisable()
    {
        presenter.OnUpdateNexusUI -= UpdateUI;
    }
}
