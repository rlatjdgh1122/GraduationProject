using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgrade : BuildingUIComponent, ICreateSlotUI
{
    [Header("Building Upgrade UI")]
    [SerializeField] private BuildingUpgradeSlot _upgradeSlotPrefab;
    [SerializeField] private List<Color> _colorOfUpgradeList = new();
    [SerializeField] private Transform _upgradeSlotParent;

    private TextMeshProUGUI _costText;
    private TextMeshProUGUI _upgradeText;
    private Button _purchaseButton;

    public override void Awake()
    {
        base.Awake();

        _costText = transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        _purchaseButton = transform.Find("InteractionButton").GetComponent<Button>();
        _upgradeText = transform.Find("InteractionButton/UpgradeText").GetComponent<TextMeshProUGUI>();
    }

    public void OnPurchaseUpgrade()
    {
        
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
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

    public void CreateSlot()
    {
        for(int i = 0; i  < _colorOfUpgradeList.Count; i++)
        {
            var upgradeSlot = Instantiate(_upgradeSlotPrefab, _upgradeSlotParent);

            upgradeSlot.Init(i, _colorOfUpgradeList[i], infoData.BuildingAbilityList[i].BuildingUpgradeDescription);

            upgradeSlot.UpdateSlot();
        }
    }
}
