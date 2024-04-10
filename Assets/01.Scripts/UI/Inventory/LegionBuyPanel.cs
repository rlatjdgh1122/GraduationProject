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
        _priceText.text       = price.ToString();
        _amountCostText.text  = amount.ToString();

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

        LegionInventoryManager.Instance.LegionList()[_legionNumber].Locked = false; //���� ��ư �ر�

        int legion = _legionNumber + 1;
        UIManager.Instance.ShowWarningUI($"{legion}���� ���� ����!");

        _legionChange.ChangingLegion(_legionNumber); //������ �������� �ٲٱ�
        LegionInventoryManager.Instance.ChangeLegionNumber(_legionNumber);
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