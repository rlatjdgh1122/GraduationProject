using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PenguinUniqueType
{
    Fight,
    Production
}

public enum PenguinJobType
{
    General,
    Solider
}

[Serializable]
public class PenguinDetailData
{
    public string Weapon;
    [Range(0f, 1f)] public float atk;
    [Range(0f, 1f)] public float def;
    [Range(0f, 1f)] public float range;
    public string Passive;
    public string Synergy;
}

[CreateAssetMenu(menuName = "SO/Stat/Penguin")]
public class PenguinStat : BaseStat
{
    [Header("Penguin UI Data")]
    public PenguinUniqueType UniqueType;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
    public string PenguinName;
    public Sprite PenguinIcon;
    public PenguinDetailData PenguinData;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }

    public string PenguinJobTypeName()
    {
        if (JobType == PenguinJobType.General)
        {
            return "장군";
        }
        else
        {
            return "병사";
        }
    }

    public void UpdateAblitiyUI(Slider atk, Slider def, Slider rng)
    {
        atk.value = PenguinData.atk;
        def.value = PenguinData.def;
        rng.value = PenguinData.range;
    }

    public void PenguinInformationTextUpdate(TextMeshProUGUI weapon, TextMeshProUGUI passive, TextMeshProUGUI synergy)
    {
        weapon.text = PenguinData.Weapon;
        passive.text = PenguinData.Passive;
        synergy.text = PenguinData.Synergy;
    }
}