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
        _amountCostText.text  = $"{legionNumber}���� �����ϱ� (���� ��ȭ : {amount})";

        if (CostManager.Instance.CheckRemainingCost(price))
        {
            _canBuy = true;

            _legionNumber = legionNumber;
        }
        else _canBuy = false;
    }

    public void BuyLegionClick() //���� ��� ��ư�� ������
    {
        if (!_canBuy) //�� �� ������
        {
            UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�!");

            return;
        }


        int legion = _legionNumber + 1;

        LegionInventoryManager.Instance.LegionList[_legionNumber].Locked = false; //���� ��ư �ر�
        UIManager.Instance.ShowWarningUI($"{legion}���� ���� ����!");
        UIManager.Instance.ShowPanel("LegionInventory");
        _legionChange.ChangingLegion(_legionNumber); //������ �������� �ٲٱ�
        LegionInventoryManager.Instance.ChangeLegionNumber(_legionNumber);
        //ArmyManager.Instance.CreateArmy();
        CostManager.Instance.SubtractFromCurrentCost(_price);

        _canBuy = false;

        HidePanel();
    }

    public void CancelBuyClick() //���� ���Ÿ� ����ϸ�
    {
        _legionNumber = LegionInventoryManager.Instance.CurrentLegion;
        LegionInventoryManager.Instance.ChangeLegion(_legionNumber); //���� �ֽ� �������� ���ư���

        HidePanel();

    }

    public override void HidePanel()
    {
        base.HidePanel();

        _legionChange.HidePanel();
    }
}