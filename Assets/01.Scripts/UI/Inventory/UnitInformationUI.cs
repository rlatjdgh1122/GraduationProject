using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInformationUI : SlotUI
{
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
        }
    }
}
