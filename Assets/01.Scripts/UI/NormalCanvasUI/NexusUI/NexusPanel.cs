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
    }

    protected override void Start()
    {
        base.Start();

        _panel.alpha = 1.0f;
        UpdateUI();
    }

    public void UpdateUI()
    {
        currentLevel.text = $"Lv {_nexusStat.level}";   
        nextLevel.text = $"Lv {_nexusStat.level + 1}";
        currentHp.text = $"{_nexusInfo.currentMaxHealth}";
        nextHp.text = $"{_nexusInfo.nextMaxHealth}";
        currentWorkerCount.text = $"{_nexusInfo.currentWorkerCount}";
        nextWorkerCount.text = $"{_nexusInfo.nextWorkerCount}";
        if (_nexusInfo.previewBuilding == null)
        {
            buildingIcon.gameObject.SetActive(false);
        }
        else
        {
            buildingIcon.gameObject.SetActive(true);
            buildingIcon.sprite = _nexusInfo.previewBuilding.UISprite;
        }
        upgradePrice.text = $"{_nexusStat.upgradePrice}";
    }

    public void OnLevelUp()
    {
        _presenter.LevelUp();
    }

    public override void MovePanel(float x, float y, float fadeTime)
    {
        base.MovePanel(x, y, fadeTime);
    }

    public override void UIUpdate()
    {
        UpdateUI();   
    }
}
