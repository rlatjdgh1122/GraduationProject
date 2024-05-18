using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;

public class LegionSlot : LegionSlotUI
{
    private Image _hpSlide;
    private Image _hp;
    private float _hpPercent;

    protected override void Awake()
    {
        base.Awake();

        _hp = transform.Find("HP").GetComponent<Image>();
        _hpSlide = transform.Find("HP/fillAmount").GetComponent<Image>();
    }

    /// <summary>
    /// ������ ������� �� �����
    /// </summary>
    /// <param name="Index">��ġ</param>
    /// <param name="removeSlot">������ �����ִ� Ű</param>
    /// <param name="penguinInfo">�������� Ű</param>
    public void CreateSlot(int Index, KeyCode removeSlot, KeyCode penguinInfo)
    {
        Idx = Index;
        removeKey = removeSlot;
        infoKey = penguinInfo;
    }


    public override void RemovePenguinData()
    {
        legion.RemovePenguinInCurrentLegion(Idx);
    }


    public override void ChoosePenguinSituation()
    {
        legion.ShowPenguinSituation(Data, _hpPercent);
    }

    public override void PushDataInSlot()
    {
        EnterSlot(legion.SelectData.InfoData); //���� ����

        legion.RemovePenguin(legion.SelectData.InfoData);
        legion.RemoveStack();

        legion.LegionRegistration(Idx, Data); //����� ���� ���ܿ� ����ϱ�
    }

    public override void ShowPenguinInfo()
    {
        UnitInventoryData data = new UnitInventoryData(Data, 1, SlotType.Legion);

        legion.SelectInfoData(data);
    }

    public override void ExitSlot(EntityInfoDataSO data)
    {
        base.ExitSlot(data);

        HideHP();
    }

    public void HpValue(float value)
    {
        _hpPercent = value;
    }

    public void ShowHP()
    {
        _hp.gameObject.SetActive(true); 
        _hpSlide.DOFillAmount(_hpPercent, 0.3f);
    }

    public void HideHP() => _hp.gameObject.SetActive(false);
}