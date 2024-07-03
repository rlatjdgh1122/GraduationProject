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
    private int _price = 0;

    private LegionChange _legionChange;

    public override void Awake()
    {
        base.Awake();

        _priceText       = transform.Find("Price").GetComponent<TextMeshProUGUI>();
        _currentCostText = transform.Find("CurrentCost").GetComponent<TextMeshProUGUI>();
        _amountCostText  = transform.Find("Button/BuyButton/Text").GetComponent<TextMeshProUGUI>();

        _legionChange    = transform.parent.GetComponent<LegionChange>();
    }

    public void CheckCanBuy(float currentCost, int price, int legionNumber)
    {
        float amount = currentCost - price;
        _price = price;

        _currentCostText.text = currentCost.ToString();
        _priceText.text       = $"-{price}";
        _amountCostText.text  = $"{legionNumber}군단 구매하기 (남는 재화 : {amount})";

        if (CostManager.Instance.CheckRemainingCost(price))
        {
            _canBuy = true;

            _legionNumber = legionNumber;
        }
        else _canBuy = false;
    }

    public void BuyLegionClick() //군단 사기 버튼을 누르면
    {
        if (!_canBuy) //살 수 없으면
        {
            UIManager.Instance.ShowWarningUI("재화가 부족합니다!");

            return;
        }


        int legion = _legionNumber + 1;

        LegionInventoryManager.Instance.LegionList[_legionNumber].Locked = false; //군단 버튼 해금
        UIManager.Instance.ShowWarningUI($"{legion}군단 구매 성공!");
        UIManager.Instance.ShowPanel("LegionInventory");
        _legionChange.ChangingLegion(_legionNumber); //구매한 군단으로 바꾸기
        LegionInventoryManager.Instance.ChangeLegionNumber(_legionNumber);
        //ArmyManager.Instance.CreateArmy();
        CostManager.Instance.SubtractFromCurrentCost(_price);

        _canBuy = false;

        HidePanel();
    }

    public void CancelBuyClick() //군단 구매를 취소하면
    {
        _legionNumber = LegionInventoryManager.Instance.CurrentLegion;
        LegionInventoryManager.Instance.ChangeLegion(_legionNumber); //가장 최신 군단으로 돌아가기

        HidePanel();

    }

    public override void HidePanel()
    {
        base.HidePanel();

        _legionChange.HidePanel();
    }
}