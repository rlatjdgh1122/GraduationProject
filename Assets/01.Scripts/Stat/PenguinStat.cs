using JetBrains.Annotations;
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

public enum GeneralCharacterType
{
    Melee,
    Range,
    MiddleRange,
}

[Serializable]
public class PenguinDetailData
{
    public int level = 1;
    public int price;
    public int levelUpPrice;
    public string _weapon;
    public string _type;
    [Range(0f, 1f)] public float atk;
    [Range(0f, 1f)] public float def;
    [Range(0f, 1f)] public float range;
    public string _characteristic;
    public string _passive;
    public string _Synergy;
    public GeneralCharacterType characterType;
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

    public void UpdateAblitiyUI(TextMeshProUGUI name, Slider atk, Slider def, Slider rng)
    {
        name.text = PenguinName;
        atk.value = PenguinData.atk;
        def.value = PenguinData.def;
        rng.value = PenguinData.range;
    }

    public void PenguinInformationUpdate(TextMeshProUGUI weapon, TextMeshProUGUI type, TextMeshProUGUI characteristic
        , TextMeshProUGUI passive, TextMeshProUGUI synergy)
    {
        weapon.text = PenguinData._passive;
        type.text = PenguinData._type;
        characteristic.text = PenguinData._characteristic;
        passive.text = PenguinData._passive;
        synergy.text = PenguinData._Synergy;
    }
}