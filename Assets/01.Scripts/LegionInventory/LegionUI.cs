using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LegionUI : InitLegionInventory
{
    private TextMeshProUGUI _generalCountText;
    private TextMeshProUGUI _soliderCountText;

    private bool _showHP = false;

    public override void Awake()
    {
        base.Awake();

        _generalCountText = transform.Find("LegionCount/GeneralCount").GetComponent<TextMeshProUGUI>();
        _soliderCountText = transform.Find("LegionCount/SoliderCount").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _showHP = !_showHP;

            ShowHP();
        }
    }

    private void ShowHP()
    {
        if(_showHP)
        {
            foreach(var slot in slotList)
            {
                if(slot.Data != null)
                    slot.ShowHP();
            }
        }
        else
        {
            foreach (var slot in slotList)
            {
                slot.HideHP();
            }
        }
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