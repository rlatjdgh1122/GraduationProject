using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

[Serializable]
public class PenguinUnitSlot
{
    public int price;
    public Penguin spawnPenguinPrefab;
}

public class PenguinStoreUI : PopupUI
{
    [Header("Make Penguin Slot")]
    [SerializeField] private Transform _spawnPenguinButtonParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;
    public List<PenguinUnitSlot> _slotList;

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

        foreach (var slot in _slotList) //Make Penguin Slot
        {
            SpawnPenguinButton btn = Instantiate(_spawnPenguinButtonPrefab, _spawnPenguinButtonParent);
            btn.InstantiateSelf(slot.spawnPenguinPrefab.Stat as PenguinStat, slot.spawnPenguinPrefab, slot.price);
            btn.SlotUpdate();
        }
    }

    public void PenguinInformataion(Penguin spawnPenguin, PenguinStat penguinStat, int price)
    {
        BuyPanel.PenguinInformataion(spawnPenguin, penguinStat, price);
        _infoPanel.PenguinInformataion(spawnPenguin, penguinStat);
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
