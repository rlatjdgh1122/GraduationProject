using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitInformationUI : MonoBehaviour
{
    private EntityInfoDataSO so;

    private TextMeshProUGUI _classNameText;
    private TextMeshProUGUI _nameText;
    private Image _penguinIcon;
    private Image _atkSlide;
    private Image _defSlide;
    private Image _rangeSlide;

    private CanvasGroup _generalInfo;
    private TextMeshProUGUI _synergyText;
    private TextMeshProUGUI _passiveText;

    private void Awake()
    {
        _penguinIcon   = transform.Find("PenguinFace").GetComponent<Image>();
        _classNameText = transform.Find("PenguinClassTex").GetComponent<TextMeshProUGUI>();
        _nameText   = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _atkSlide   = transform.Find("Atk/fillAmount").GetComponent<Image>();
        _defSlide   = transform.Find("Def/fillAmount").GetComponent<Image>();
        _rangeSlide = transform.Find("Range/fillAmount").GetComponent<Image>();

        _generalInfo = transform.Find("GeneralInfo").GetComponent<CanvasGroup>();
        _synergyText = _generalInfo.transform.Find("Synergy").GetComponent<TextMeshProUGUI>();
        _passiveText = _generalInfo.transform.Find("Passive").GetComponent<TextMeshProUGUI>();
    }

    public void ShowInformation(UnitInventoryData data)
    {
        if (data == null)
        {
            CleanUpUI();
        }
        else
        {
            SetUIElements(data);
        }

        if(data.infoData.JobType == PenguinJobType.General)
        {
            ShowGeneralInfo();
        }
        else
        {

        }
    }

    private void SetUIElements(UnitInventoryData data)
    {
        so = data.infoData;

        _penguinIcon.gameObject.SetActive(true);
        _penguinIcon.sprite = so.PenguinIcon;
        _classNameText.text = so.JobType.ToString();
        _nameText.text = so.PenguinName;

        _atkSlide.DOFillAmount(so.atk, 0.5f);
        _defSlide.DOFillAmount(so.hp, 0.5f);
        _rangeSlide.DOFillAmount(so.range, 0.5f);
    }

    private void ShowGeneralInfo()
    {

    }

    private void CleanUpUI()
    {
        _penguinIcon.gameObject.SetActive(false);
        _classNameText.text = string.Empty;
        _nameText.text = string.Empty;
        
        _atkSlide.DOFillAmount(0, 0.5f);
        _defSlide.DOFillAmount(0, 0.5f);
        _rangeSlide.DOFillAmount(0, 0.5f);
    }
}