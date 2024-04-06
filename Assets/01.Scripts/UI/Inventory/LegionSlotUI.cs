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

    [SerializeField]
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
        }

        else if(_data != null)
        {
            if(Input.GetKeyDown(_removeKey))
            {

            }
            else if(Input.GetKeyDown(_infoKey))
            {
                Debug.Log("Ã¢ ¶ç¿ì±â");
            }
            else
            {
                UnitInventoryData data = new UnitInventoryData(_data, 1);

                _legion.SelectInfoData(data , SlotType.Legion);
            }
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        _bkColor.a = 0;
        _bkImage.color = _bkColor;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _bkColor.a = 1;
        _bkImage.color = _bkColor;
    }


    public override void UpdateSlot()
    {
        unitImage.enabled = true;
        unitImage.sprite = _data.PenguinIcon;
    }



    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data == null) return;

        _data = data;

        UpdateSlot();

        _legion.RemovePenguin(_legion.SelectData.infoData);
    }


    public override void ExitSlot(EntityInfoDataSO data)
    {
        _data = null;
        unitImage.sprite = null;
        unitImage.enabled = false;
    }
}
