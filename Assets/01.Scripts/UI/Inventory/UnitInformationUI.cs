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

    public LegionInventoryData _infoData = null;

    public override void CleanUpSlot()
    {
        _data = null;
        _unitImage.sprite = _emptyImage;
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
            _unitImage.sprite = data.penguinData.PenguinIcon;
            _name.text = data.penguinData.PenguinName;
            _className.text = data.penguinData.PenguinJobTypeName();

            data.penguinData.PenguinInformationUpdate(_weapon, _type, _characteristic, _passive, _Synergy);
        }
    }
}
