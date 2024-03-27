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
    private TextMeshProUGUI _infoPenguinNameText;
    private Image _penguinFace;
    private Slider _rangeSlider;
    private Slider _hpSlider;
    private Slider _atkSlider;

    private EntityInfoDataSO _infoData;
    private DummyPenguin _dummyPenguin;

    public override void Awake()
    {
        base.Awake();

        _presenter = transform.parent.GetComponent<PenguinStoreUI>();

        _infoPenguinNameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _penguinFace = transform.Find("PenguinImg").GetComponent<Image>();
        _rangeSlider = transform.Find("Rng").GetComponent<Slider>();
        _hpSlider = transform.Find("Hp").GetComponent<Slider>();
        _atkSlider = transform.Find("Atk").GetComponent<Slider>();
        _infoPenguinNameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData)
    {
        _infoData = infoData;   
        _dummyPenguin = dummyPenguin;
    }

    private void UpdatePenguinInfo(float time)
    {
        _penguinFace.sprite = _infoData.PenguinIcon;
        _infoPenguinNameText.text = _infoData.PenguinName;

        _atkSlider.DOValue(_infoData.atk, time);
        _hpSlider.DOValue(_infoData.hp, time);
        _rangeSlider.DOValue(_infoData.range, time);
    }


    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UpdatePenguinInfo(0.5f);
    }
}
