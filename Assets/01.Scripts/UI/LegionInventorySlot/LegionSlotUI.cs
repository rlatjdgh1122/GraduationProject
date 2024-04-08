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


    /// <summary>
    /// 슬롯이 만들어질 때 실행됨
    /// </summary>
    /// <param name="Index">위치</param>
    /// <param name="removeSlot">슬롯을 지워주는 키</param>
    /// <param name="penguinInfo">정보보기 키</param>
    public void CreateSlot(int Index, KeyCode removeSlot, KeyCode penguinInfo)
    {
        Idx        = Index;
        _removeKey = removeSlot;
        _infoKey   = penguinInfo;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(_data == null && _legion.SelectData == null) return;

        if (_data != null)
        {
            if (Input.GetKey(_removeKey)) //만약 삭제키를 누르면
            {
                _legion.RemovePenguinInCurrentLegion(Idx);
                ExitSlot(null);
                return;
            }
            else if (Input.GetKey(_infoKey)) //만약 정보보기(군단에 들어간 펭귄을 은퇴할건지 회복할 건지) 키를 누르면
            {
                //_legion.LegionInven.OpenPanel();
            }
            else if (_legion.SelectData != null) return;
            else //슬롯 데이터를 펭귄 정보창에 띄우기
            {
                UnitInventoryData data = new UnitInventoryData(_data, 1);

                _legion.SelectInfoData(data, SlotType.Legion);
            }
        }

        if(_legion.SelectData.infoData.JobType == PenguinJobType.General && _legion.LimitOfGeneral()) //장군이 이미 군단에 있고 장군을 슬롯에 추가할려고 하면
        {
            UIManager.Instance.ShowWarningUI("장군이 이미 군단에 포함되어 있습니다!");
            return;
        }
        if (_legion.SelectData.infoData.JobType != PenguinJobType.General && _legion.ExcedLimitOfLegion()) //만약 군단 안에 펭귄이 최대 펭귄 수를 넘었으면
        {
            return;
        }


        if(_data == null && _legion.SelectData != null) //슬롯에 데이터 넣기
        {
            EnterSlot(_legion.SelectData.infoData); //슬롯 업뎃

            _legion.RemovePenguin(_legion.SelectData.infoData);
            _legion.RemoveStack();

            _legion.LegionRegistration(Idx, _data); //펭귄을 현재 군단에 등록하기
        }
    }

    /// <summary>
    /// 슬롯의 데이터를 바꿔줌
    /// </summary>
    /// <param name="data"></param>
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

    /// <summary>
    /// 슬롯 데이터 초기화
    /// </summary>
    /// <param name="data"></param>
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
