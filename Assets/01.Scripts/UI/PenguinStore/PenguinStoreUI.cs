using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PenguinStorePanel
{
    public CanvasGroup panel;
    public float panelAlphaFadeTime;
}

[Serializable]
public class PenguinUnitSlot
{
    public int price;
    public Penguin spawnPenguinPrefab;
}

public class PenguinStoreUI : PopupUI
{
    public List<PenguinStorePanel> _panelList;
    public List<PenguinUnitSlot> _slotList;
    [Header("SpawnPenguin")]
    [SerializeField] private Transform UnitInventoryParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;

    [Header("PenguinStore")]
    [SerializeField] private Transform _spawnPenguinButtonParent;

    [Header("BuyPanel")]
    private TextMeshProUGUI _buyCntText;
    private TextMeshProUGUI _priceText;
    private TextMeshProUGUI _currentCostText;
    private TextMeshProUGUI _amountCostText;
    private TextMeshProUGUI _buyToPenguinNameText;
    private Image _buyButtonImg;
    private TextMeshProUGUI _buyButtonText;
    [SerializeField] private int _maxCount;
    private int _cnt = 1;
    private int _price = 0;
    private bool _canBuy;

    [Header("PenguinInfoPanel")]
    private TextMeshProUGUI _infoPenguinNameText;
    private Image _penguinFace;
    private Slider _rangeSlider;
    private Slider _hpSlider;
    private Slider _atkSlider;

    private PenguinStat _stat;
    private Penguin _spawnPenguin;
    private PenguinFactory _penguinFactory;

    private CanvasGroup _statuCanvas;
    private TextMeshProUGUI _statuesMessageText;

    public override void Awake()
    {
        base.Awake();

        _penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();

        #region BuyPanelComponent

        _buyToPenguinNameText = _panelList[1].panel.transform.Find("Buy/PenguinName").GetComponent<TextMeshProUGUI>();
        _amountCostText       = _panelList[1].panel.transform.Find("Buy/AmountCost").GetComponent<TextMeshProUGUI>();
        _buyCntText           = _panelList[1].panel.transform.Find("Buy/BuyCount/Count").GetComponent<TextMeshProUGUI>();
        _currentCostText      = _panelList[1].panel.transform.Find("Buy/CurrentCost").GetComponent<TextMeshProUGUI>();
        _priceText            = _panelList[1].panel.transform.Find("Buy/Price").GetComponent<TextMeshProUGUI>();
        _buyButtonImg         = _panelList[1].panel.transform.Find("Buttons/BuyBtn").GetComponent<Image>();
        _buyButtonText        = _panelList[1].panel.transform.Find("Buttons/BuyBtn/Text").GetComponent<TextMeshProUGUI>();

        #endregion
        #region PenguinInfoPanel Component
        _infoPenguinNameText = _panelList[2].panel.transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _penguinFace         = _panelList[2].panel.transform.Find("PenguinImg").GetComponent<Image>();
        _rangeSlider         = _panelList[2].panel.transform.Find("Rng").GetComponent<Slider>();
        _hpSlider            = _panelList[2].panel.transform.Find("Hp").GetComponent<Slider>();
        _atkSlider           = _panelList[2].panel.transform.Find("Atk").GetComponent<Slider>();
        _statuCanvas         = transform.Find("StatusMessage").GetComponent<CanvasGroup>();
        _statuesMessageText  = _statuCanvas.transform.Find("WhenBuyPenguin").GetComponent<TextMeshProUGUI>();
        #endregion

        foreach (var slot in _slotList)
        {
            SpawnPenguinButton btn = Instantiate(_spawnPenguinButtonPrefab, UnitInventoryParent);
            btn.InstantiateSelf(slot.spawnPenguinPrefab.Stat as PenguinStat, slot.spawnPenguinPrefab, slot.price);
            btn.SlotUpdate();
        }
    }

    #region OnOffPanel

    public void OnEnableStorePanel() //����� �г� Ȱ��ȭ
    {
        _panelList[0].panel.DOFade(1, _panelList[0].panelAlphaFadeTime);
        Debug.Log(_panelList[0]);
        DisableRayExceptSelf(_panelList[0].panel);
    }

    public void OnDisableStorePanel()//����� �г� ��Ȱ��ȭ
    {
        _panelList[0].panel.DOFade(0, _panelList[0].panelAlphaFadeTime);
        _panelList[0].panel.blocksRaycasts = false;
    }
    public void OnEnableBuyPanel() //���� �г� Ȱ��ȭ
    {
        _panelList[1].panel.DOFade(1, _panelList[1].panelAlphaFadeTime);
        DisableRayExceptSelf(_panelList[1].panel);

        CurrentCostUpdate();
        AmountCostUpdate();
    }

    public void OnDisableBuyPanel()//���� �г� ��Ȱ��ȭ
    {
        _panelList[1].panel.DOFade(0, _panelList[1].panelAlphaFadeTime);
        DisableRayExceptSelf(_panelList[0].panel);

        ResetBuyPanel();
    }

    public void OnEnablePenguinInfo() //��� ���� Ȱ��ȭ
    {
        _panelList[2].panel.DOFade(1, _panelList[2].panelAlphaFadeTime);
        DisableRayExceptSelf(_panelList[2].panel);

        UpdatePenguinInfo(_panelList[2].panelAlphaFadeTime);
    }

    public void OnDisablePenguinInfo() //��� ���� ��Ȱ��ȭ
    {
        _panelList[2].panel.DOFade(0, _panelList[2].panelAlphaFadeTime);
        DisableRayExceptSelf(_panelList[1].panel);
    }

    public void OneClickBuyPenguin()
    {
        if(!_canBuy)
        {
            ShowMessage("��ȭ�� �����մϴ�!");
            return;
        }

        AmountCostUpdate();
        BuyButton();
        ShowMessage("���� ����!");
    }

    #endregion

    #region BuyPanel

    private int _amountPrice;
    private void PriceUpdate() //����� �� ������ ����ؼ� ����
    {
        _amountPrice = (_price * _cnt);

        TextUpdate(_priceText, _amountPrice.ToString());

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //���� ���� ��ȭ ����
    {
        _buyToPenguinNameText.text = _stat.PenguinName;
        TextUpdate(_currentCostText, CostManager.Instance.Cost.ToString());
    }

    public void PlusCnt() //UI �ȿ� �ִ� +��ư�� ������
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }
    public void MinusCnt()//UI �ȿ� �ִ� -��ư�� ������
    {
        if (_cnt <= 1) return;

        _cnt--;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }

    private void AmountCostUpdate() //���� ���� ��ȭ���� �� ������ �� ���� ��ȭ ����
    {
        PriceUpdate();
        int amount = CostManager.Instance.Cost - _amountPrice;
        
        if(_amountPrice > CostManager.Instance.Cost) //���� �Ѱ����� ���� ��ȭ���� ũ�ٸ�
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

        TextUpdate(_amountCostText, amount.ToString());    
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
        TextUpdate(_buyButtonText, str);
    }

    public void ResetBuyPanel()
    {
        _cnt = 1;
        PriceUpdate();
        CurrentCostUpdate();
        AmountCostUpdate();
        TextUpdate(_buyCntText, _cnt.ToString());
    }

    public void BuyButton()
    {
        if (!_canBuy) return;

        CostManager.Instance.SubtractFromCurrentCost(_amountPrice);

        for(int i = 0; i < _cnt; i++)
        {
            LegionInventory.Instance.AddPenguin(_spawnPenguin.ReturnGenericStat<PenguinStat>());

            _penguinFactory.SpawnPenguinHandler(_spawnPenguin);
        }


        ResetBuyPanel();
        OnDisableBuyPanel();
    }

    private void ShowMessage(string message) //���� �ӽ÷� �ھƵа�
    {
        UIManager.Instance.InitializHudTextSequence();

        _statuesMessageText.text = message;

        UIManager.Instance.HudTextSequence.Append(_statuCanvas.DOFade(1, 0.04f))
                .AppendInterval(0.8f)
                .Append(_statuCanvas.DOFade(0, 0.04f));
    }


    #endregion

    #region PenguinInfo

    private void UpdatePenguinInfo(float time)
    {
        _penguinFace.sprite = _stat.PenguinIcon;
        _infoPenguinNameText.text = _stat.PenguinName;

        _atkSlider.DOValue(_stat.PenguinData.atk, time);
        _hpSlider.DOValue(_stat.PenguinData.hp, time);
        _rangeSlider.DOValue(_stat.PenguinData.range, time);
    }


    #endregion

    private void DisableRayExceptSelf(CanvasGroup self)
    {
        for(int i = 0; i < _panelList.Count; i++)
        {
            if(self != _panelList[i].panel)
            {
                _panelList[i].panel.blocksRaycasts = false;
            }
            else
            {
                _panelList[i].panel.blocksRaycasts = true;
            }
        }
    }

    public void PenguinInformataion(Penguin spawnPenguin, PenguinStat stat, int price) //���Կ��� ��� ���� �ޱ�
    {
        _price = -price; //����
        _spawnPenguin = spawnPenguin;
        _stat = stat;


        PriceUpdate();
    }

    public void TextUpdate(TextMeshProUGUI text, string str)
    {
        text.text = str;
    }
}
