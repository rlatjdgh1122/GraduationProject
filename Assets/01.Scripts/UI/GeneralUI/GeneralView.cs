using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GeneralView : GeneralPopupUI, IPointerEnterHandler
{
    public GeneralStat generalStat;
    public Penguin generalObj;

    [SerializeField] private Color _outlineColor;

    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI purchaseText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI stateText;
    public Image icon;
    public Image outline;
    public Button purchaseButton;
    public Button upgradeButton;

    public override void Awake()
    {
        base.Awake();

        if (generalStat.GeneralData.IsAvailable)
            SetUpgradeUI(generalStat);
        else
            UpdateDefaultUI(generalStat);
    }

    public void UpdateDefaultUI(GeneralStat generalStat)
    {
        nameText.text = generalStat.PenguinName;
        stateText.text = "잠금됨";
        purchaseText.text = $"{generalStat.PenguinData.price:N0} 영입하기";
    }

    public void SetUpgradeUI(GeneralStat generalStat)
    {
        purchaseButton.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(true);
        icon.DOColor(Color.white, 0.6f);
        outline.DOColor(_outlineColor, 0.6f);
        levelText.gameObject.SetActive(true);
        stateText.text = "보유중";
        upgradeText.text = $"{generalStat.GeneralData.levelUpPrice.GetValue():N0} 업그레이드";
        levelText.text = $"Lv {generalStat.GeneralData.level}";
    }

    public void UpdateUpgradeUI(GeneralStat generalStat)
    {
        upgradeText.text = $"{generalStat.GeneralData.levelUpPrice.GetValue():N0} 업그레이드";
        levelText.text = $"Lv {generalStat.GeneralData.level}";
    }

    public void OnPurchase()
    {
        presenter.Purchase(generalStat);
    }

    public void OnUpgrade()
    {
        presenter.ShowUpgradePanel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (presenter != null)
            presenter.SetCurrentView(this);
    }
}
