using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class PenguinStoreUI : PopupUI
{
    [Header("PenguinStore")]
    [SerializeField] private Transform _spawnPenguinButtonParent;

    public PenguinStat _stat;
    public Penguin _spawnPenguin;
    public PenguinFactory _penguinFactory;

    public CanvasGroup _statuCanvas;
    public TextMeshProUGUI _statuesMessageText;

    public override void Awake()
    {
        base.Awake();

        _penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        _statuCanvas = transform.Find("StatusMessage").GetComponent<CanvasGroup>();
        _statuesMessageText = _statuCanvas.transform.Find("WhenBuyPenguin").GetComponent<TextMeshProUGUI>();
    }

    #region OnOffPanel

    public void OnEnableStorePanel() //����� �г� Ȱ��ȭ
    {
        ShowPanel();
        UIManager.Instance.ShowPanel("StorePanel");
    }

    public void OnDisableStorePanel()//����� �г� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("StorePanel");
    }

    public void OnEnableBuyPanel() //���� �г� Ȱ��ȭ
    {
        UIManager.Instance.ShowPanel("BuyPanel");
    }

    public void OnDisableBuyPanel()//���� �г� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("BuyPanel");
    }

    public void OnEnablePenguinInfo() //��� ���� Ȱ��ȭ
    {
        UIManager.Instance.ShowPanel("InfoPanel");
    }

    public void OnDisablePenguinInfo() //��� ���� ��Ȱ��ȭ
    {
        UIManager.Instance.HidePanel("InfoPanel");
    }

    #endregion

    //private void DisableRayExceptSelf(CanvasGroup self)
    //{
    //    for(int i = 0; i < _panelList.Count; i++)
    //    {
    //        if(self != _panelList[i].panel)
    //        {
    //            _panelList[i].panel.blocksRaycasts = false;
    //        }
    //        else
    //        {
    //            _panelList[i].panel.blocksRaycasts = true;
    //        }
    //    }
    //}

    //public void PenguinInformataion(Penguin spawnPenguin, PenguinStat stat, int price) //���Կ��� ��� ���� �ޱ�
    //{
    //    _price = -price; //����
    //    _spawnPenguin = spawnPenguin;
    //    _stat = stat;


    //    PriceUpdate();
    //}

    public void TextUpdate(TextMeshProUGUI text, string str)
    {
        text.text = str;
    }
}
