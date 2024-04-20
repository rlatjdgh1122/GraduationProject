using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InfoPanel : PopupUI
{
    private PenguinStoreUI _presenter;

    [Header("PenguinInfoPanel")]
    protected TextMeshProUGUI infoPenguinNameText;
    protected Image penguinFace;
    protected Slider rangeSlider;
    protected Slider hpSlider;
    protected Slider atkSlider;

    private EntityInfoDataSO _infoData;

    public override void Awake()
    {
        base.Awake();

        _presenter = transform.parent.GetComponent<PenguinStoreUI>();

        infoPenguinNameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        penguinFace = transform.Find("PenguinImg").GetComponent<Image>();
        rangeSlider = transform.Find("Rng").GetComponent<Slider>();
        hpSlider = transform.Find("Hp").GetComponent<Slider>();
        atkSlider = transform.Find("Atk").GetComponent<Slider>();
        infoPenguinNameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
    }

    public void PenguinInformataion(EntityInfoDataSO infoData)
    {
        _infoData = infoData;
    }

    public void UpdatePenguinInfo()
    {
        penguinFace.sprite = _infoData.PenguinIcon;
        infoPenguinNameText.text = _infoData.PenguinName;
    }

    public void UpdateSlider(float time)
    {
        atkSlider.DOValue(_infoData.atk, time);
        hpSlider.DOValue(_infoData.hp, time);
        rangeSlider.DOValue(_infoData.range, time);
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UpdatePenguinInfo();
    }
}
