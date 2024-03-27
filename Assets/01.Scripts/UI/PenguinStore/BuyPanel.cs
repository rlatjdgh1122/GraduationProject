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

    private EntityInfoDataSO _infoData;
    private DummyPenguin _dummyPenguin;

    private TextMeshProUGUI _buyCntText;
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _currentCostText;
    //private TextMeshProUGUI _amountCostText;
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
        _buyToPenguinNameText = transform.Find("Title/PenguinBuyText").GetComponent<TextMeshProUGUI>();
        //_amountCostText       = transform.Find("Buy/AmountCost").GetComponent<TextMeshProUGUI>();
        _buyCntText           = transform.Find("Buy/BuyCount/Count").GetComponent<TextMeshProUGUI>();
        _currentCostText      = transform.Find("Buy/CurrentCost").GetComponent<TextMeshProUGUI>();
        _priceText            = transform.Find("Buy/Price").GetComponent<TextMeshProUGUI>();
        _buyButtonImg         = transform.Find("Buttons/BuyBtn").GetComponent<Image>();
        _buyButtonText        = transform.Find("Buttons/BuyBtn/Text").GetComponent<TextMeshProUGUI>();
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        _dummyPenguin = dummyPenguin;
        _infoData = infoData;
        _price = price;
    }

    private int _amountPrice;
    private void PriceUpdate() //펭귄의 총 가격을 계산해서 업뎃
    {
        _amountPrice = (_price * _cnt);

        _presenter.TextUpdate(_priceText, _amountPrice.ToString());

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //현재 보유 재화 업뎃
    {
        _buyToPenguinNameText.text = $"{_infoData.PenguinName} 구매하기";
        _presenter.TextUpdate(_currentCostText, CostManager.Instance.Cost.ToString());
    }

    public void PlusCnt() //UI 안에 있는 +버튼을 누르면
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        _presenter.TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }
    public void MinusCnt()//UI 안에 있는 -버튼을 누르면
    {
        if (_cnt <= 1) return;

        _cnt--;
        _presenter.TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }

    private int _amount;
    private void AmountCostUpdate() //현재 보유 재화에서 총 가격을 뺀 남은 재화 업뎃
    {
        PriceUpdate();
        int amount = CostManager.Instance.Cost - _amountPrice;

        if (_amountPrice > CostManager.Instance.Cost) //만약 총가격이 현재 재화보다 크다면
        {
            //_amountCostText.color = Color.red;
            amount = -Mathf.Abs(amount);
            _canBuy = false;
        }
        else
        {
           //_amountCostText.color = Color.green;
            amount = Mathf.Abs(amount);
            _canBuy = true;
        }

        _amount = amount;
        BuyButtonUpdate();

        //_presenter.TextUpdate(_amountCostText, amount.ToString());
    }

    private void BuyButtonUpdate()
    {
        string str;
        if (!_canBuy)
        {
            _buyButtonImg.color = Color.red;
            str = "재화 부족";
        }
        else
        {
            _buyButtonImg.color = Color.white;
            str = $"구매하기 (남는 재화 : {_amount})";
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
            LegionInventory.Instance.AddPenguin(_dummyPenguin.PenguinUIInfo);

            _presenter._penguinFactory.SpawnPenguinHandler(_dummyPenguin);
        }


        ResetBuyPanel();
    }

    private void ShowMessage(string message) //값들 임시로 박아둔것
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
            ShowMessage("재화가 부족합니다!");
            return;
        }

        AmountCostUpdate();
        BuyButton();
        ShowMessage("구매 성공!");
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
