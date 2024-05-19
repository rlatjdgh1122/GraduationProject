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
            if (Input.GetKey(removeKey)) //���� ����Ű�� ������
            {
                RemovePenguinData();
                return;
            }
            else if (Input.GetKey(infoKey)) //���� ��������(���ܿ� �� ����� �����Ұ��� ȸ���� ����) Ű�� ������
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

        if (legion.SelectData.InfoData.JobType == PenguinJobType.General && legion.LimitOfGeneral()) //�屺�� �̹� ���ܿ� �ְ� �屺�� ���Կ� �߰��ҷ��� �ϸ�
        {
            UIManager.Instance.ShowWarningUI("�屺�� �̹� ���ܿ� ���ԵǾ� �ֽ��ϴ�!");
            return;
        }
        else if (legion.SelectData.InfoData.JobType != PenguinJobType.General && legion.ExcedLimitOfLegion()) //���� ���� �ȿ� ����� �ִ� ��� ���� �Ѿ�����
        {
            UIManager.Instance.ShowWarningUI("������ ���� á���ϴ�!");
            return;
        }

        if (Data == null && legion.SelectData != null) //���Կ� ������ �ֱ�
        {
            PushDataInSlot();
        }
    }

    /// <summary>
    /// ������ �����͸� �ٲ���
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
    /// ���� ������ �ʱ�ȭ
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