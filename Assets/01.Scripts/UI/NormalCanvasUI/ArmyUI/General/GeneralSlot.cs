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
            // _ownText가 파괴되지 않았는지 확인 후 SetActive 호출
            if (_ownText != null)
                _ownText.SetActive(true);
        }

        // _icon이 null이 아닌지 확인 후 스프라이트 설정
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