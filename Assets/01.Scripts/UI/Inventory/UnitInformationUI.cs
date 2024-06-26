using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationUI : MonoBehaviour
{
    public readonly char Cashing_Separator = ',';

    private EntityInfoDataSO so;

    private TextMeshProUGUI _nameText;
    private Image _penguinIcon;
    private Image _atkSlide;
    private Image _defSlide;
    private Image _rangeSlide;

    private void Awake()
    {
        _penguinIcon = transform.Find("PenguinFace").GetComponent<Image>();
        _nameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _atkSlide = transform.Find("Atk/fillAmount").GetComponent<Image>();
        _defSlide = transform.Find("Def/fillAmount").GetComponent<Image>();
        _rangeSlide = transform.Find("Range/fillAmount").GetComponent<Image>();
    }

    public void ShowInformation(EntityInfoDataSO data)
    {
        CleanUpUI();

        if (data == null)
        {
            return;
        }

        SetUIElements(data);
    }

    private void SetUIElements(EntityInfoDataSO data)
    {
        so = data;

        _penguinIcon.gameObject.SetActive(true);
        _penguinIcon.sprite = so.PenguinIcon;

        _nameText.text = so.PenguinName;

        _atkSlide.DOFillAmount(so.atk, 0.5f);
        _defSlide.DOFillAmount(so.hp, 0.5f);
        _rangeSlide.DOFillAmount(so.range, 0.5f);
    }

    private void CleanUpUI()
    {
        _penguinIcon.gameObject.SetActive(false);
        _nameText.text = string.Empty;

        _atkSlide.DOFillAmount(0, 0.5f);
        _defSlide.DOFillAmount(0, 0.5f);
        _rangeSlide.DOFillAmount(0, 0.5f);
    }
}