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

        //_generalCountText = transform.Find("LegionCount/GeneralCount").GetComponent<TextMeshProUGUI>();
        //_soliderCountText = transform.Find("LegionCount/SoliderCount").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (ChangedInCurrentLegion())
            {
                UIManager.Instance.ShowWarningUI($"저장해주세요");
                return;
            }

            _showHP = !_showHP;

            ShowHP();
        }
    }

    private void ShowHP()
    {
        if (_showHP)
        {
            ImportHpValue();

            foreach (var slot in slotList)
            {
                if (slot.Data != null)
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

    protected void ImportHpValue()
    {
        foreach (var hp in currentLegionList)
        {
            var penguin = PenguinManager.Instance.GetPenguinByInfoData(hp);

            float curHp = penguin.HealthCompo.currentHealth;
            float maxHp = penguin.HealthCompo.maxHealth;

            float hpPercent = curHp / maxHp;

            slotList[hp.SlotIdx].HpValue(hpPercent);
        }
    }

    public void LegionCountTextSetting()
    {
        int curLegion = LegionInventoryManager.Instance.CurrentLegion;
        int maxLegion = LegionInventoryManager.Instance.LegionList[curLegion].MaxCount;

        ColorBoolean(currentGeneral >= 1, _generalCountText);
        _generalCountText.text = $"{currentGeneral} / 1";

        ColorBoolean(currentPenguinCnt >= maxLegion, _soliderCountText);
        _soliderCountText.text
            = $"{currentPenguinCnt} / {maxLegion}";
    }

    private void ColorBoolean(bool value, TextMeshProUGUI text)
    {
        if (value)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    public bool LimitOfGeneral()
    {
        return currentGeneral > 0 ? true : false;
    }

    public bool ChangedInCurrentLegion()
    {
        bool value = false;

        foreach (var data in currentLegionList)
        {
            if (!savedLegionList.Contains(data))
            {
                value = true;
            }
        }

        foreach (var data in currentRemovePenguinList)
        {
            if (savedLegionList.Contains(data))
            {
                value = true;
            }
        }

        return value;
    }

    public bool ExcedLimitOfLegion(int legionNumber)
    {
        if (legion.LegionList[legionNumber].MaxCount <= currentPenguinCnt - currentRemovePenguinCnt)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}