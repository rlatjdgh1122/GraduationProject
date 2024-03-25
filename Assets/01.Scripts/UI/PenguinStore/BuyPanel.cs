using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyPanel : PopupUI
{
    private PenguinStoreUI _presenter;

    [Header("BuyPanel")]
    [SerializeField] private int _maxCount;

    private PenguinStat _stat;
    private Penguin _spawnPenguin;

    private TextMeshProUGUI _buyCntText;
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _currentCostText;
    private TextMeshProUGUI _amountCostText;
    private TextMeshProUGUI _buyToPenguinNameText;
    private TextMeshProUGUI _buyButtonText;
    private Image _buyButtonImg;

    private int _cnt = 1;
    private int _price = 0;
    private bool _canBuy;

    public override void Awake()
    {
        base.Awake();

        _presenter            = transform.parent.GetComponent<PenguinStoreUI>();
        _buyToPenguinNameText = transform.Find("Buy/PenguinName").GetComponent<TextMeshProUGUI>();
        _amountCostText       = transform.Find("Buy/AmountCost").GetComponent<TextMeshProUGUI>();
        _buyCntText           = transform.Find("Buy/BuyCount/Count").GetComponent<TextMeshProUGUI>();
        _currentCostText      = transform.Find("Buy/CurrentCost").GetComponent<TextMeshProUGUI>();
        _priceText            = transform.Find("Buy/Price").GetComponent<TextMeshProUGUI>();
        _buyButtonImg         = transform.Find("Buttons/BuyBtn").GetComponent<Image>();
        _buyButtonText        = transform.Find("Buttons/BuyBtn/Text").GetComponent<TextMeshProUGUI>();
    }

    public void PenguinInformataion(Penguin spawnPenguin, PenguinStat penguinStat, int price)
    {
        _spawnPenguin = spawnPenguin;
        _stat = penguinStat;
        _price = price;
    }

    private int _amountPrice;
    private void PriceUpdate() //����� �� ������ ����ؼ� ����
    {
        _amountPrice = (_price * _cnt);

        _presenter.TextUpdate(_priceText, _amountPrice.ToString());

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //���� ���� ��ȭ ����
    {
        _buyToPenguinNameText.text = _stat.PenguinName;
        _presenter.TextUpdate(_currentCostText, CostManager.Instance.Cost.ToString());
    }

    public void PlusCnt() //UI �ȿ� �ִ� +��ư�� ������
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        _presenter.TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }
    public void MinusCnt()//UI �ȿ� �ִ� -��ư�� ������
    {
        if (_cnt <= 1) return;

        _cnt--;
        _presenter.TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }

    private void AmountCostUpdate() //���� ���� ��ȭ���� �� ������ �� ���� ��ȭ ����
    {
        PriceUpdate();
        int amount = CostManager.Instance.Cost - _amountPrice;

        if (_amountPrice > CostManager.Instance.Cost) //���� �Ѱ����� ���� ��ȭ���� ũ�ٸ�
        {
            _amountCostText.color = Color.red;
            amount = -Mathf.Abs(amount);
            _canBuy = false;
        }
        else
        {
            _amountCostText.color = Color.green;
            amount = Mathf.Abs(amount);
            _canBuy = true;
        }

        BuyButtonUpdate();

        _presenter.TextUpdate(_amountCostText, amount.ToString());
    }

    private void BuyButtonUpdate()
    {
        string str;
        if (!_canBuy)
        {
            _buyButtonImg.color = Color.red;
            str = "��ȭ ����";
        }
        else
        {
            _buyButtonImg.color = Color.white;
            str = "����";
        }
        _presenter.TextUpdate(_buyButtonText, str);
    }

    public void ResetBuyPanel()
    {
        _cnt = 1;
        PriceUpdate();
        CurrentCostUpdate();
        AmountCostUpdate();
        _presenter.TextUpdate(_buyCntText, _cnt.ToString());
    }

    public void BuyButton()
    {
        if (!_canBuy) return;

        CostManager.Instance.SubtractFromCurrentCost(_amountPrice);

        for (int i = 0; i < _cnt; i++)
        {
            LegionInventory.Instance.AddPenguin(_spawnPenguin.ReturnGenericStat<PenguinStat>());

            _presenter._penguinFactory.SpawnPenguinHandler(_spawnPenguin);
        }


        ResetBuyPanel();
        _presenter.OnDisableBuyPanel();
    }

    private void ShowMessage(string message) //���� �ӽ÷� �ھƵа�
    {
        UIManager.Instance.InitializHudTextSequence();

        _presenter._statuesMessageText.text = message;

        UIManager.Instance.HudTextSequence.Append(_presenter._statuCanvas.DOFade(1, 0.04f))
                .AppendInterval(0.8f)
                .Append(_presenter._statuCanvas.DOFade(0, 0.04f));
    }

    public void OneClickBuyPenguin()
    {
        if (_price > CostManager.Instance.Cost)
        {
            ShowMessage("��ȭ�� �����մϴ�!");
            return;
        }

        AmountCostUpdate();
        BuyButton();
        ShowMessage("���� ����!");
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        ResetBuyPanel();
    }
}
