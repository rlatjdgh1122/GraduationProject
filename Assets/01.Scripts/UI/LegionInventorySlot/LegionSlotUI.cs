using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegionSlotUI : SlotUI, IPointerEnterHandler, IPointerExitHandler
{
    public int Idx;

    private EntityInfoDataSO _data;
    private Image _bkImage;
    private Color _bkColor;

    private LegionInventoryManager _legion;

    private KeyCode _removeKey;
    private KeyCode _infoKey;

    private void Awake()
    {
        unitImage = transform.Find("Penguin").GetComponent<Image>();
        _bkImage  = transform.Find("Image").GetComponent<Image>();

        _bkColor = _bkImage.color;
        _legion  = LegionInventoryManager.Instance;
    }

    public void CreateSlot(int Index, KeyCode removeSlot, KeyCode penguinInfo)
    {
        Idx        = Index;
        _removeKey = removeSlot;
        _infoKey   = penguinInfo;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_data == null && _legion.SelectData == null) return;

        if(_data == null && _legion.SelectData != null)
        {
            EnterSlot(_legion.SelectData.infoData);
            _legion.RemovePenguin(_legion.SelectData.infoData);

            _legion.LegionInven.LegionRegistration(Idx, _data);
        }

        else if(_data != null)
        {
            if(Input.GetKey(_removeKey)) //만약 삭제키를 누르면
            {
                _legion.LegionInven.Remove(Idx);
                ExitSlot(null);
            }
            else if(Input.GetKey(_infoKey)) //만약 정보보기(군단에 들어간 펭귄을 은퇴할건지 회복할 건지) 키를 누르면
            {
                //_legion.LegionInven.OpenPanel();
            }
            else
            {
                UnitInventoryData data = new UnitInventoryData(_data, 1);

                _legion.SelectInfoData(data , SlotType.Legion);
            }
        }
    }

    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data == null) return;

        _data = data;
        UpdateSlot();
    }

    public override void UpdateSlot()
    {
        unitImage.enabled = true;
        unitImage.sprite = _data.PenguinIcon;
    }

    public override void ExitSlot(EntityInfoDataSO data)
    {
        _data = null;
        unitImage.sprite = null;
        unitImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _bkColor.a = 1;
        _bkImage.color = _bkColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _bkColor.a = 0;
        _bkImage.color = _bkColor;
    }
}
