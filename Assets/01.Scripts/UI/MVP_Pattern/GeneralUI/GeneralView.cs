using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GeneralView : MonoBehaviour, IPointerEnterHandler
{
    public GeneralStat generalStat;
    public GeneralMainUI MainUI;

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

    private void Awake()
    {
        if (generalStat.GeneralData.IsAvailable)
            SetUpgradeUI(generalStat);
        else
            SetDefaultUI(generalStat);
    }

    public void SetDefaultUI(GeneralStat generalStat)
    {
        nameText.text = generalStat.PenguinName;
        stateText.text = "��ݵ�";
        purchaseText.text = $"{generalStat.PenguinData.price:N0} �����ϱ�";
    }

    public void SetUpgradeUI(GeneralStat generalStat)
    {
        purchaseButton.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(true);
        icon.DOColor(Color.white, 0.6f);
        outline.DOColor(_outlineColor, 0.6f);
        levelText.gameObject.SetActive(true);
        stateText.text = "������";
        upgradeText.text = $"{generalStat.GeneralData.levelUpPrice.GetValue():N0} ���׷��̵�";
        levelText.text = $"Lv {generalStat.GeneralData.level}";
    }

    public void UpdateUpgradeUI(GeneralStat generalStat)
    {
        upgradeText.text = $"{generalStat.GeneralData.levelUpPrice.GetValue():N0} ���׷��̵�";
        levelText.text = $"Lv {generalStat.GeneralData.level}";
    }

    public void OnPurchase()
    {
        MainUI.Purchase(generalStat);
    }

    public void OnUpgrade()
    {
        MainUI.ShowUpgradePanel(generalStat);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MainUI.SetGeneralView(this);
    }
}
