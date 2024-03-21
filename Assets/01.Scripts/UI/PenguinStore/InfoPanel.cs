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

    #region PenguinInfo

    private void UpdatePenguinInfo(float time)
    {
        _penguinFace.sprite = _presenter._stat.PenguinIcon;
        _infoPenguinNameText.text = _presenter._stat.PenguinName;

        _atkSlider.DOValue(_presenter._stat.PenguinData.atk, time);
        _hpSlider.DOValue(_presenter._stat.PenguinData.hp, time);
        _rangeSlider.DOValue(_presenter._stat.PenguinData.range, time);
    }


    #endregion

    //public void PenguinInformataion(Penguin spawnPenguin, PenguinStat stat, int price) //½½·Ô¿¡¼­ Æë±Ï Á¤º¸ ¹Þ±â
    //{
    //    _presenter._price = -price; //°¡°Ý
    //    _spawnPenguin = spawnPenguin;
    //    _stat = stat;


    //    PriceUpdate();
    //}

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
