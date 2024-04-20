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

    private PenguinInfoDataSO _infoData;
    private DummyPenguin _dummyPenguin;
    private PenguinStoreUI _penguinStore;
    
    private Image _icon;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;

    private Image _selectImg;
    private Image _locked;

    private bool _isLocked = false;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _icon         = transform.Find("PenguinImg/PenguinFace").GetComponent<Image>();
        _nameText     = transform.Find("PenguinImg/PenguinName").GetComponent<TextMeshProUGUI>();
        _priceText    = transform.Find("PenguinImg/Cost/CostText").GetComponent<TextMeshProUGUI>();
        _selectImg    = transform.Find("SelectImg").GetComponent<Image>();
        _locked       = transform.Find("Locked").GetComponent<Image>();
    }

    public void InstantiateSelf(PenguinInfoDataSO infoData, DummyPenguin dummyPenguin, int price, bool isLocked = false)
    {
        _infoData = infoData;
        _dummyPenguin = dummyPenguin;
        _price = price;

        _isLocked = isLocked;
    }

    public void SlotUpdate()
    {
        _icon.sprite = _infoData.PenguinIcon;
        _nameText.text = _infoData.PenguinName;
        _priceText.text = _price.ToString();
        _locked.gameObject.SetActive(_isLocked);
        _nameText.gameObject.SetActive(!_isLocked);

        if (_isLocked)
        {
            _icon.color = Color.black;
        }
    }

    public void UnLockedButton()
    {
        _isLocked = false;
        _icon.color = Color.white;
        _locked.gameObject.SetActive(_isLocked);
        _nameText.gameObject.SetActive(!_isLocked);
    }

    private void SpawnPenguinLeftEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        _penguinStore.PenguinInformataion(_dummyPenguin, _infoData, _price);
        _penguinStore.OnEnableBuyPanel();
    }
    private void SpawnPenguinRightEventHandler() //Inspector 버튼 이벤트에서 구독할 함수
    {
        _penguinStore.PenguinInformataion(_dummyPenguin, _infoData, _price);
        _penguinStore.BuyPanel.OneClickBuyPenguin();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(_isLocked)
        {
            UIManager.Instance.ShowWarningUI("잠겨있습니다!");
            return;
        }

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
