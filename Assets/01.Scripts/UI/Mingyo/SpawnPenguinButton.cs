using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int _price;

    private PenguinStat _penguinStat;
    private Penguin _spawnPenguin;
    private PenguinStoreUI _penguinStore;
    
    private Image _icon;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;

    private Image _selectImg;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _icon         = transform.Find("PenguinImg/PenguinFace").GetComponent<Image>();
        _nameText     = transform.Find("PenguinImg/PenguinName").GetComponent<TextMeshProUGUI>();
        _priceText    = transform.Find("PenguinImg/Cost/CostText").GetComponent<TextMeshProUGUI>();
        _selectImg    = transform.Find("SelectImg").GetComponent<Image>();
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

    private void SpawnPenguinLeftEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        _penguinStore.PenguinInformataion(_spawnPenguin, _penguinStat, _price);
        _penguinStore.OnEnableBuyPanel();
    }
    private void SpawnPenguinRightEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        _penguinStore.PenguinInformataion(_spawnPenguin, _penguinStat, _price);
        _penguinStore.BuyPanel.OneClickBuyPenguin();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
        {
            SpawnPenguinLeftEventHandler();
        }
        if(Input.GetMouseButtonDown(1)) //마우스 오른쪽 버튼
        {
            SpawnPenguinRightEventHandler();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _selectImg.DOFade(0.6f, 0);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _selectImg.DOFade(0, 0);
    }
}
