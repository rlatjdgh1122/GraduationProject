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

    private PenguinStat _stat;
    private Penguin _spawnPenguin;

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

    public void PenguinInformataion(Penguin spawnPenguin, PenguinStat penguinStat)
    {
        _stat = penguinStat;
        _spawnPenguin = spawnPenguin;
    }

    private void UpdatePenguinInfo(float time)
    {
        _penguinFace.sprite = _stat.PenguinIcon;
        _infoPenguinNameText.text = _stat.PenguinName;

        _atkSlider.DOValue(_stat.PenguinData.atk, time);
        _hpSlider.DOValue(_stat.PenguinData.hp, time);
        _rangeSlider.DOValue(_stat.PenguinData.range, time);
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
