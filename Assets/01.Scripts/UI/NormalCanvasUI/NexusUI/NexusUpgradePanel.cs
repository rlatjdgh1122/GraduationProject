using DG.Tweening;
using System.Collections;
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

    private Coroutine _panelCoroutine;

    public override void Awake()
    {
        base.Awake();
    }

    public void UpdateUI()
    {
        _level.text = $"·¹º§ {_nexusStat.level}";
        _previousHp.text = $"{_nexusInfo.previousMaxHealth}";
        _currentHp.text = $"{_nexusInfo.currentMaxHealth}";
        _previousWorkerCount.text = $"{_nexusInfo.previousWorkerCount}";
        _currentWorkerCount.text = $"{_nexusInfo.currentWorkerCount}";
        if (_nexusInfo.unlockedBuilding != null)
        {
            _unlockedImage.sprite = _nexusInfo.unlockedBuilding.UISprite;
            _unlockedName.text = $"{_nexusInfo.unlockedBuilding.Name}";
        }
    }

    private void PanelLogic()
    {
        if (_panelCoroutine != null)
            StopCoroutine(_panelCoroutine);

        _panelCoroutine = StartCoroutine(PanelLogicCorutine());
    }

    private IEnumerator PanelLogicCorutine()
    {
        yield return new WaitForSeconds(0.1f);
        _element.DOAnchorPosX(0, .8f).SetEase(Ease.OutBack, 0.9f);
        yield return new WaitForSeconds(2.5f);
        if (_nexusInfo.unlockedBuilding == null)
        {
            _element.DOAnchorPosX(618, 0f).OnComplete(() => HidePanel());
        }
        else
        {
            _element.DOAnchorPosX(-618, .8f).SetEase(Ease.OutBack, 0.9f);
            yield return new WaitForSeconds(2.5f);
            _element.DOAnchorPosX(618, 0f).OnComplete(() => HidePanel());
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        PanelLogic();
        UpdateUI();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void UIUpdate()
    {
    }
}
