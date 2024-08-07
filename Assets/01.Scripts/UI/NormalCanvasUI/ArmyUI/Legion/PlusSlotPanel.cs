using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlusSlotPanel : PopupUI
{
    private Button _purchaseButton;
    private LegionSlotPurchaseButton _slot;

    public override void Awake()
    {
        base.Awake();

        _purchaseButton = transform.Find("Popup/PurchaseButton").GetComponent<Button>();

        _purchaseButton.onClick.AddListener(OnPurchase);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    private void OnPurchase()
    {
        _slot.Purchase();
        HidePanel();
    }

    public void Setting(LegionSlotPurchaseButton slot)
    {
        _slot = slot;
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }
}
