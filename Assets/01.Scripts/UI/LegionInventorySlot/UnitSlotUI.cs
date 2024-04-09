using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSlotUI : SlotUI
{
    private TextMeshProUGUI _countText;
    private Image _lockImg;
    private int _stackSize = 0;
    public EntityInfoDataSO _keyData;
    private bool _locked => _stackSize <= 0;

    protected override void Awake()
    {
        base.Awake();

        unitImage = transform.Find("Penguin").GetComponent<Image>();
        _countText = transform.Find("CountBK/Count").GetComponent<TextMeshProUGUI>();
        _lockImg = transform.Find("Lock").GetComponent<Image>();
    }

    public void Create(EntityInfoDataSO data)
    {
        _keyData = data;

        unitImage.sprite = data.PenguinIcon;

        UpdateSlot();
    }

    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data != _keyData) return;

        ++_stackSize;

        UpdateSlot();
    }

    public override void ExitSlot(EntityInfoDataSO data)
    {
        if (data != _keyData || _stackSize <= 0) return;

        _stackSize--;

        UpdateSlot();
    }

    public override void UpdateSlot()
    {
        _lockImg.gameObject.SetActive(_locked);

        _countText.text = $"{_stackSize} ¸¶¸®";
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_locked) return;

        UnitInventoryData data = new UnitInventoryData(_keyData, _stackSize);

        LegionInventoryManager.Instance.SelectInfoData(data);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(!_locked)
            base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(!_locked)
            base.OnPointerExit(eventData);
    }
}
