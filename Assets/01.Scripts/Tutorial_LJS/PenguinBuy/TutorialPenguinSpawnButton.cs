using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialPenguinSpawnButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    private int _price;

    private PenguinInfoDataSO _infoData;
    public PenguinInfoDataSO InfoData => _infoData;

    private DummyPenguin _dummyPenguin;
    private TutorialStoreUI _penguinStore;

    private Image _icon;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;

    private Image _selectImg;

    public bool IsWaveLock { get; set; } = false;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.GetComponent<TutorialStoreUI>();
        _icon = transform.Find("PenguinImg/PenguinFace").GetComponent<Image>();
        _nameText = transform.Find("PenguinImg/PenguinName").GetComponent<TextMeshProUGUI>();
        _priceText = transform.Find("PenguinImg/Cost/CostText").GetComponent<TextMeshProUGUI>();
        _selectImg = transform.Find("SelectImg").GetComponent<Image>();
    }

    public void InstantiateSelf(PenguinInfoDataSO infoData, DummyPenguin dummyPenguin, int price)
    {
        _infoData = infoData;
        _dummyPenguin = dummyPenguin;
        _price = price;
    }

    public void SlotUpdate()
    {
        _icon.sprite = _infoData.PenguinIcon;
        _nameText.text = _infoData.PenguinName;
        _priceText.text = _price.ToString();
    }

    private void SpawnPenguinLeftEventHandler()
    {
        _penguinStore.PenguinInformataion(_dummyPenguin, _infoData, _price);
        UIManager.Instance.ShowPanel("BuyPanel");
    }
    private void SpawnPenguinRightEventHandler()
    {
        _penguinStore.PenguinInformataion(_dummyPenguin, _infoData, _price);
        _penguinStore.BuyPanel.OneClickBuyPenguin();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0)) //마우스 왼쪽 버튼
        {
            SpawnPenguinLeftEventHandler();
        }
        if (Input.GetMouseButtonDown(1)) //마우스 오른쪽 버튼
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
