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

    public override void Awake()
    {
        base.Awake();

        OnUIUpdate += UpdateUI;
    }

    protected override void Start()
    {
        base.Start();
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentLevel.text = $"Lv {_nexusStat.level}";
        nextLevel.text = $"Lv {_nexusStat.level + 1}";
        currentHp.text = $"{_nexusStat.GetMaxHealthValue()}";
        nextHp.text = $"{_nexusStat.GetUpgradedMaxHealthValue()}";
        currentWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount}";
        nextWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount + 1}";
        buildingIcon.sprite = _nexusStat.previewBuilding.UISprite;
        upgradePrice.text = $"{_nexusStat.upgradePrice}";
    }

    public override void OnClick()
    {
        _presenter.LevelUp();
        base.OnClick();
    }

    public override void MovePanel(float x, float y, float fadeTime)
    {
        base.MovePanel(x, y, fadeTime);
    }

    private void OnDisable()
    {
        OnUIUpdate -= UpdateUI;
    }
}
