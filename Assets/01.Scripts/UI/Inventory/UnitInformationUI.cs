using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnitInformationUI : SlotUI
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _className;
    [SerializeField] TextMeshProUGUI _weapon;
    [SerializeField] TextMeshProUGUI _type;
    [SerializeField] TextMeshProUGUI _characteristic;
    [SerializeField] TextMeshProUGUI _passive;
    [SerializeField] TextMeshProUGUI _Synergy;

    public override void CleanUpSlot()
    {
        _data = null;
        _unitImage.sprite = _emptyImage;
    }

    public override void UpdateSlot(LegionInventoryData data)
    {
        _data = data;

        if(_data != null)
        {
            _unitImage.sprite = _data.penguinData.PenguinIcon;
            _name.text = _data.penguinData.PenguinName;
            _className.text = _data.penguinData.PenguinJobTypeName();

            _data.penguinData.PenguinInformationUpdate(_weapon, _type, _characteristic, _passive, _Synergy);
        }
    }
}
