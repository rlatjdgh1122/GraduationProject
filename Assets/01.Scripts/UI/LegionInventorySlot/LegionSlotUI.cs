using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class LegionSlotUI : SlotUI
{
    public int Idx;

    public EntityInfoDataSO Data { get; private set; }

    protected LegionInventoryManager legion;

    protected KeyCode removeKey;
    protected KeyCode infoKey;

    protected override void Awake()
    {
        base.Awake();

        unitImage = transform.Find("Penguin").GetComponent<Image>();

        legion   = LegionInventoryManager.Instance;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (Data == null && legion.SelectData == null) return;


        if (Data != null)
        {
            if (Input.GetKey(removeKey)) //만약 삭제키를 누르면
            {
                RemovePenguinData();
                return;
            }
            else if (Input.GetKey(infoKey)) //만약 정보보기(군단에 들어간 펭귄을 은퇴할건지 회복할 건지) 키를 누르면
            {
                ChoosePenguinSituation();
                return;
            }
            else
            {
                ShowPenguinInfo();
                return;
            }
        }

        if (legion.SelectData.SlotType == SlotType.Legion) return;

        if (legion.SelectData.InfoData.JobType == PenguinJobType.General && legion.LimitOfGeneral()) //장군이 이미 군단에 있고 장군을 슬롯에 추가할려고 하면
        {
            UIManager.Instance.ShowWarningUI("장군이 이미 군단에 포함되어 있습니다!");
            return;
        }
        else if (legion.SelectData.InfoData.JobType != PenguinJobType.General && legion.ExcedLimitOfLegion()) //만약 군단 안에 펭귄이 최대 펭귄 수를 넘었으면
        {
            UIManager.Instance.ShowWarningUI("군단이 가득 찼습니다!");
            return;
        }

        if (Data == null && legion.SelectData != null) //슬롯에 데이터 넣기
        {
            PushDataInSlot();
        }
    }

    /// <summary>
    /// 슬롯의 데이터를 바꿔줌
    /// </summary>
    /// <param name="data"></param>
    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data == null) return;

        Data = data;

        UpdateSlot();
    }

    public override void UpdateSlot()
    {
        unitImage.enabled = true;
        unitImage.sprite = Data.PenguinIcon;

        if(Data.JobType == PenguinJobType.General)
        {
            unitImage.transform.localScale = new Vector3(1.3f, 1.1f, 1f);
        }
        else
        {
            unitImage.transform.localScale = new Vector3(1f, 1.1f, 1f);
        }
    }

    /// <summary>
    /// 슬롯 데이터 초기화
    /// </summary>
    /// <param name="data"></param>
    public override void ExitSlot(EntityInfoDataSO data)
    {
        Data = null;
        unitImage.sprite = null;
        unitImage.enabled = false;
    }

    public abstract void RemovePenguinData();
    public abstract void ChoosePenguinSituation();
    public abstract void PushDataInSlot();
    public abstract void ShowPenguinInfo();
}