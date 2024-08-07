using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSlot : ArmyComponentUI
{
    public GeneralStat GeneralData;
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _ownText;

    private void Start()
    {
        SetUI();
    }

    public void SetUI()
    {
        if (GeneralData == null) return;

        if (GeneralData.GeneralDetailData != null && GeneralData.GeneralDetailData.IsAvailable)
        {
            // _ownText�� �ı����� �ʾҴ��� Ȯ�� �� SetActive ȣ��
            if (_ownText != null)
                _ownText.SetActive(true);
        }

        // _icon�� null�� �ƴ��� Ȯ�� �� ��������Ʈ ����
        if (_icon != null && GeneralData.InfoData != null)
        {
            _icon.sprite = GeneralData.InfoData.PenguinIcon;
        }
    }

    public void ShowInfoPanel()
    {
        generalSlotPanel.HidePanel();
        ShowGeneralInfo(GeneralData);
    }
}