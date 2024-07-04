using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgrade : PopupUI
{
    private TextMeshProUGUI _costText;
    private TextMeshProUGUI _upgradeText;
    private Button _purchaseButton;

    private float _upgradeCost = 200f;
    private int _currentIndex = 0;

    public List<BuildingUpgradeSlot> UpgradeList;

    public override void Awake()
    {
        base.Awake();
        UpgradeList = transform.GetComponentsInChildren<BuildingUpgradeSlot>().ToList();
        _costText = transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        _purchaseButton = transform.Find("InteractionButton").GetComponent<Button>();
        _upgradeText = transform.Find("InteractionButton/UpgradeText").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _costText.text = _upgradeCost.ToString();
    }

    public void OnPurchaseUpgrade()
    {
        if (UpgradeList.Count <= 0)
        {
            _upgradeText.text = "모든 효과를 해금하셨습니다";
            _purchaseButton.interactable = false;
            return;
        }
        
        CostManager.Instance.SubtractFromCurrentCost(_upgradeCost, () =>
        {
            _upgradeCost *= 2f;
            _costText.text = _upgradeCost.ToString();

            UpgradeList.FirstOrDefault().OnUnlock();
            UpgradeList.Remove(UpgradeList[_currentIndex]);
        });
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, _panelFadeTime);
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
        ShowPanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
