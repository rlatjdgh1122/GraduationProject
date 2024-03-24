using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour, IPointerDownHandler
{
    private int _price;

    private PenguinStat _penguinStat;
    private Penguin _spawnPenguin;
    private PenguinStoreUI _penguinStore;
    
    private Image _icon;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _icon         = transform.Find("PenguinImg/PenguinFace").GetComponent<Image>();
        _nameText     = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _priceText    = transform.Find("Cost/CostText").GetComponent<TextMeshProUGUI>();
    }

    public void InstantiateSelf(PenguinStat stat, Penguin spawnPenguin, int price)
    {
        _penguinStat = stat;
        _spawnPenguin = spawnPenguin;

        _price = price;
    }

    public void SlotUpdate()
    {
        _icon.sprite = _penguinStat.PenguinIcon;
        _nameText.text = _penguinStat.PenguinName;
        _priceText.text = _price.ToString();
    }

    private void SpawnPenguinLeftEventHandler() //Inspector ��ư �̺�Ʈ���� ������ �Լ�
    {
        _penguinStore.PenguinInformataion(_spawnPenguin, _penguinStat, _price);
        _penguinStore.OnEnableBuyPanel();
    }
    private void SpawnPenguinRightEventHandler() //Inspector ��ư �̺�Ʈ���� ������ �Լ�
    {
        _penguinStore.PenguinInformataion(_spawnPenguin, _penguinStat, _price);
        _penguinStore.BuyPanel.OneClickBuyPenguin();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(0)) //���콺 ���� ��ư
        {
            SpawnPenguinLeftEventHandler();
        }
        if(Input.GetMouseButtonDown(1)) //���콺 ������ ��ư
        {
            SpawnPenguinRightEventHandler();
        }
    }
}
