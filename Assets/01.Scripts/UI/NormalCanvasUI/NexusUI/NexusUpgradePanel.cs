using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NexusUpgradePanel : NexusPopupUI
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _previousHp;
    [SerializeField] private TextMeshProUGUI _currentHp;
    [SerializeField] private TextMeshProUGUI _previousWorkerCount;
    [SerializeField] private TextMeshProUGUI _currentWorkerCount;
    [SerializeField] private Image _unlockedImage;
    [SerializeField] private TextMeshProUGUI _unlockedName;
    [SerializeField] private RectTransform _element;    
    
    public override void Awake()
    {
        base.Awake();

        //OnUIUpdate += UpdateUI;
    }

    public void UpdateUI()
    {
        _level.text = $"·¹º§ {_nexusStat.level}";
        _previousHp.text = $"{_nexusInfo.previousMaxHealth}";
        _currentHp.text = $"{_nexusInfo.currentMaxHealth}";
        _previousWorkerCount.text = $"{_nexusInfo.previousWorkerCount}";
        _currentWorkerCount.text = $"{_nexusInfo.currentWorkerCount}";
        _unlockedImage.sprite = _nexusInfo.unlockedBuilding.UISprite;
        _unlockedName.text = $"{_nexusInfo.unlockedBuilding.Name}";
    }

    private void PanelLogic()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(.5f);
        seq.Append(_element.DOAnchorPosX(0, 1f)).SetEase(Ease.OutBack, 0.9f);
        seq.AppendInterval(3f);
        seq.Append(_element.DOAnchorPosX(-618, 1f)).SetEase(Ease.OutBack, 0.9f);
        seq.AppendInterval(3f);
        seq.Append(_element.DOAnchorPosX(618, 0f).OnComplete(() => HidePanel()));
    }

    public override void ShowPanel()
    {
        //OnUIUpdate += UpdateUI;
        base.ShowPanel();
        UpdateUI();
        PanelLogic();
    }

    public override void HidePanel()
    {
        //OnUIUpdate -= UpdateUI;
        base.HidePanel();
    }
}
