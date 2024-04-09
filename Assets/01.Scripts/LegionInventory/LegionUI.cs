using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegionUI : InitLegionInventory
{
    private TextMeshProUGUI _generalCountText;
    private TextMeshProUGUI _soliderCountText;

    public override void Awake()
    {
        base.Awake();

        _generalCountText = transform.Find("LegionCount/GeneralCount").GetComponent<TextMeshProUGUI>();
        _soliderCountText = transform.Find("LegionCount/SoliderCount").GetComponent<TextMeshProUGUI>();
    }

    public void LegionCountTextSetting()
    {
        int curLegion = LegionInventoryManager.Instance.CurrentLegion;
        int maxLegion = LegionInventoryManager.Instance.LegionList()[curLegion].MaxCount;

        ColorBoolean(currentGeneral >= 1, _generalCountText);
        _generalCountText.text = $"{currentGeneral} / 1";

        ColorBoolean(currentPenguinCnt >= maxLegion, _soliderCountText);
        _soliderCountText.text 
            = $"{currentPenguinCnt} / {maxLegion}";
    }

    private void ColorBoolean(bool value, TextMeshProUGUI text)
    {
        if(value)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }
}