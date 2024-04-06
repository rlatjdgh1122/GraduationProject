using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegionBuyPanel : PopupUI
{
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _currentCostText;
    private TextMeshProUGUI _amountCostText;

    private bool _canBuy = false;

    public override void Awake()
    {
        base.Awake();

        _priceText       = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        _currentCostText = transform.Find("CurrentCost").GetComponent<TextMeshProUGUI>();
        _amountCostText  = transform.Find("FinalCost").GetComponent<TextMeshProUGUI>();
    }

    public void CheckCanBuy(int currentCost, int price)
    {
        _currentCostText.text = currentCost.ToString();
        _priceText.text = price.ToString();

        int amount = currentCost - price;

        if(amount < 0)
        {

        }
    }

    public void BuyLegion(int legionNumber)
    {
        LegionInventoryManager.Instance.LegionList()[legionNumber].Locked = true;
    }
}