using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationUI : SlotUI
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _className;
    [Header("PenguinDataDetail")]
    [SerializeField] private Slider _atk;
    [SerializeField] private Slider _def;
    [SerializeField] private Slider _range;
    [SerializeField] private TextMeshProUGUI _weapon;

    [Header("GeneralInfo")]
    [SerializeField] private CanvasGroup _generalInfoCanvasGroup;
    [SerializeField] private TextMeshProUGUI _passive;
    [SerializeField] private TextMeshProUGUI _Synergy;

    public LegionInventoryData _infoData = null;
    private LegionInventoryData _privateData = null;

    public override void CleanUpSlot()
    {
        _infoData = null;
        _data = null;
        _unitImage.sprite = _emptyImage;

        _atk.DOValue(0, 0.2f);
        _def.DOValue(0, 0.2f);
        _range.DOValue(0, 0.2f);

        _generalInfoCanvasGroup.DOFade(0, 0.1f);
        _name.text = null;
        _className.text = null;
        _weapon.text = null;
        _passive.text = null;
        _Synergy.text = null;
    }

    public void InfoDataSlot(LegionInventoryData data)
    {
        _infoData = data;

        UpdateInformation(_infoData);
    }

    public override void UpdateSlot(LegionInventoryData data)
    {
        _data = data;

        UpdateInformation(_data);
    }

    private void UpdateInformation(LegionInventoryData data)
    {
        if (data != null)
        {
            _privateData = data;

            _unitImage.sprite = data.penguinData.PenguinIcon;
            _name.text = data.penguinData.PenguinName;
            _className.text = data.penguinData.PenguinJobTypeName();

            if(data.penguinData.JobType == PenguinJobType.General)
            {
                _generalInfoCanvasGroup.DOFade(1, 0.1f);
            }
            else
            {
                _generalInfoCanvasGroup.DOFade(0, 0.1f);
            }

            _atk.DOValue(_privateData.penguinData.PenguinData.atk, 0.2f);
            _def.DOValue(_privateData.penguinData.PenguinData.hp, 0.2f);
            _range.DOValue(_privateData.penguinData.PenguinData.range, 0.2f);

            //data.penguinData.PenguinInformationTextUpdate(_weapon, _passive, _Synergy);
        }
    }
}
