using DG.Tweening;
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
    private int _legionNumber = 0;

    private LegionChange _legionChange;

    public override void Awake()
    {
        base.Awake();

        _priceText       = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        _currentCostText = transform.Find("CurrentCost").GetComponent<TextMeshProUGUI>();
        _amountCostText  = transform.Find("FinalCost").GetComponent<TextMeshProUGUI>();

        _legionChange    = transform.parent.GetComponent<LegionChange>();
    }

    public void CheckCanBuy(int currentCost, int price, int legionNumber)
    {
        int amount = currentCost - price;

        _currentCostText.text = currentCost.ToString();
        _priceText.text = price.ToString();
        _amountCostText.text = amount.ToString();

        if (amount < 0)
        {
            _canBuy = false;
        }
        else
        {
            _canBuy = true;

            _legionNumber = legionNumber;
        }
    }

    public void BuyLegionClick()
    {
        if (!_canBuy)
        {
            UIManager.Instance.ShowWarningUI("재화가 부족합니다!");

            return;
        }

        LegionInventoryManager.Instance.LegionList()[_legionNumber].Locked = false;

        int legion = _legionNumber + 1;
        UIManager.Instance.ShowWarningUI($"{legion}군단 구매 성공!");

        _legionChange.ChangingLegion(_legionNumber);

        _canBuy = false;
        _legionNumber = 0;

        HidePanel();
    }
}