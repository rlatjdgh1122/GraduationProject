using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialBuyPenguinUI : PopupUI
{
    private TutorialStoreUI _presenter;

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
    private float _amount = 0;

    public override void Awake()
    {
        base.Awake();

        _presenter = transform.parent.GetComponent<TutorialStoreUI>();
        _buyToPenguinNameText = transform.Find("Title/PenguinBuyText").GetComponent<TextMeshProUGUI>();
        //_amountCostText       = transform.Find("Buy/AmountCost").GetComponent<TextMeshProUGUI>();
        _buyCntText = transform.Find("Buy/BuyCount/Count").GetComponent<TextMeshProUGUI>();
        _currentCostText = transform.Find("Buy/CurrentCost").GetComponent<TextMeshProUGUI>();
        _priceText = transform.Find("Buy/Price").GetComponent<TextMeshProUGUI>();
        _buyButtonImg = transform.Find("Buttons/BuyBtn").GetComponent<Image>();
        _buyButtonText = transform.Find("Buttons/BuyBtn/Text").GetComponent<TextMeshProUGUI>();
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        _dummyPenguin = dummyPenguin;
        _infoData = infoData;
        _price = price;
    }

    private int _amountPrice;
    private void PriceUpdate() //����� �� ������ ����ؼ� ����
    {
        _amountPrice = -(_price * _cnt);

        _priceText.text = $"{_amountPrice}";

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //���� ���� ��ȭ ����
    {
        _buyToPenguinNameText.text = $"{_infoData.PenguinName} �����ϱ�";
        _currentCostText.text = $"{CostManager.Instance.Cost}";
    }

    public void PlusCnt() //UI �ȿ� �ִ� +��ư�� ������
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        _buyCntText.text = $"{_cnt}";
        AmountCostUpdate();
    }
    public void MinusCnt()//UI �ȿ� �ִ� -��ư�� ������
    {
        if (_cnt <= 1) return;

        _cnt--;
        _buyCntText.text = $"{_cnt}";
        AmountCostUpdate();
    }

    private void AmountCostUpdate() //���� ���� ��ȭ���� �� ������ �� ���� ��ȭ ����
    {
        PriceUpdate();
        float amount = CostManager.Instance.Cost - _amountPrice;

        if (_amountPrice > CostManager.Instance.Cost) //���� �Ѱ����� ���� ��ȭ���� ũ�ٸ�
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
            str = "��ȭ ����";
        }
        else
        {
            _buyButtonImg.color = Color.white;
            str = $"�����ϱ� (���� ��ȭ : {_amount})";
        }
        _buyButtonText.text = str;
    }

    public void ResetBuyPanel()
    {
        _cnt = 1;
        PriceUpdate();
        CurrentCostUpdate();
        AmountCostUpdate();
        _buyCntText.text = $"{_cnt}";
    }

    private int _spawnZIdx = 0;
    private int _spawnXIdx = 0;

    public void BuyButton()
    {
        if (!_canBuy) return;

        CostManager.Instance.SubtractFromCurrentCost(_amountPrice);

        for (int i = 0; i < _cnt; i++)
        {
            LegionInventoryManager.Instance.AddPenguin(_dummyPenguin.NotCloneInfo);

            if (_spawnXIdx >= 5)
            {
                _spawnXIdx = 0;
                _spawnZIdx++;
            }

            Vector3 spawnVec = new Vector3(6 + (_spawnXIdx * 1.5f),
                                           1.22f,
                                           -1.5f - (_spawnZIdx * 1.5f));

            _spawnXIdx++; // ���� ��ġ�� ���� idx

            Instantiate(_dummyPenguin, spawnVec, Quaternion.identity);
        }


        ResetBuyPanel();
    }

    public void OneClickBuyPenguin()
    {
        if (_price > CostManager.Instance.Cost)
        {
            UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�!");
            return;
        }

        AmountCostUpdate();
        BuyButton();
        UIManager.Instance.ShowWarningUI("���� ����!");
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public void HideBuyPanel()
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void ShowInfoPanel()
    {
        UIManager.Instance.ShowPanel("DetailInfoPanel");
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        ResetBuyPanel();
    }
}
