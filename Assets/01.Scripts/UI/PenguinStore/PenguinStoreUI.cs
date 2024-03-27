using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PenguinStoreUI : PopupUI
{
    [Header("Make Penguin Slot")]
    [SerializeField] private Transform _spawnPenguinButtonParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;
    public List<DummyPenguin> _slotList; 

    #region Component
    public PenguinFactory _penguinFactory { get; private set; }

    public CanvasGroup _statuCanvas {get; private set;}
    public TextMeshProUGUI _statuesMessageText { get; private set; }

    public BuyPanel BuyPanel { get; private set; }
    private InfoPanel _infoPanel;
    #endregion

    public override void Awake()
    {
        base.Awake();

        #region Componenet

        _penguinFactory     = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        _statuCanvas        = transform.Find("StatusMessage").GetComponent<CanvasGroup>();
        _statuesMessageText = _statuCanvas.transform.Find("WhenBuyPenguin").GetComponent<TextMeshProUGUI>();
        BuyPanel            = transform.Find("BuyPanel").GetComponent<BuyPanel>();
        _infoPanel          = transform.Find("DetailInfoPanel").GetComponent<InfoPanel>();

        #endregion

        foreach (var spwanObj in _slotList) //Make Penguin Slot
        {
            var dummyPenguin = spwanObj;
            var UIinfo = spwanObj.PenguinUIInfo;

            SpawnPenguinButton btn = Instantiate(_spawnPenguinButtonPrefab, _spawnPenguinButtonParent);
            btn.InstantiateSelf(UIinfo, dummyPenguin, UIinfo.Price);
            btn.SlotUpdate();
        }
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        BuyPanel.PenguinInformataion(dummyPenguin, infoData, price);
        _infoPanel.PenguinInformataion(dummyPenguin, infoData);
    }

    public void OnEnableStorePanel() //스토어 패널 활성화
    {
        ShowPanel();
        UIManager.Instance.ShowPanel("StorePanel");
    }

    public void OnDisableStorePanel()//스토어 패널 비활성화
    {
        UIManager.Instance.HidePanel("StorePanel");
    }

    public void OnEnableBuyPanel() //구매 패널 활성화
    {
        UIManager.Instance.ShowPanel("BuyPanel");
    }

    public void OnDisableBuyPanel()//구매 패널 비활성화
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void OnEnablePenguinInfo() //펭귄 정보 활성화
    {
        UIManager.Instance.ShowPanel("DetailInfoPanel");
    }

    public void OnDisablePenguinInfo() //펭귄 정보 비활성화
    {
        UIManager.Instance.HidePanel("DetailInfoPanel");
    }


    public void TextUpdate(TextMeshProUGUI text, string str)
    {
        text.text = str;
    }
}
