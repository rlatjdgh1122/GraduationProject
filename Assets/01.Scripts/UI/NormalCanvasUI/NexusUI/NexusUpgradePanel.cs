using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NexusUpgradePanel : PopupUI
{
    [SerializeField] private TextMeshProUGUI _level;
    [SerializeField] private TextMeshProUGUI _previousHp;
    [SerializeField] private TextMeshProUGUI _currentHp;
    [SerializeField] private TextMeshProUGUI _previousWorkerCount;
    [SerializeField] private TextMeshProUGUI _currentWorkerCount;
    [SerializeField] private Image _unlockedImage;
    [SerializeField] private TextMeshProUGUI _unlockedName;
    [SerializeField] private RectTransform _element;    

    public NexusBase nexus;

    public override void Awake()
    {
        base.Awake();
    }

    public void UpdateUI()
    {
        _level.text = $"·¹º§ {nexus.NexusStat.level}";
        _previousHp.text = $"{nexus.NexusStat.maxHealth.GetValue()}";
        _currentHp.text = $"{nexus.NexusStat.maxHealth.GetValue()}";
        _previousWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount - 1}";
        _currentWorkerCount.text = $"{WorkerManager.Instance.MaxWorkerCount}";
        _unlockedImage.sprite = nexus.NexusStat.unlockedBuilding.UISprite;
        _unlockedName.text = $"{nexus.NexusStat.unlockedBuilding.Name}";

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
        base.ShowPanel();

        UpdateUI();
    }

    public override void HidePanel()
    {       
        base.HidePanel();
    }
}
