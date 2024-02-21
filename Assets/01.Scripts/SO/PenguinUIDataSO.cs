using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

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
    public string _weapon;
    public string _type;
    public string _characteristic;
    public string _passive;
    public string _Synergy;
}

[CreateAssetMenu(menuName = "SO/Penguin/UI")]
public class PenguinUIDataSO : ScriptableObject
{
    public PenguinUniqueType UniqueType;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
    public string PenguinName;
    public Sprite PenguinIcon;
    public PenguinStat PenguinStateSO;
    public PenguinDetailData PenguinData;

    protected StringBuilder _stringBuilder = new StringBuilder();

    public virtual string GetDescription()
    {
        return string.Empty;
    }

    public string PenguinJobTypeName()
    {
        if(JobType == PenguinJobType.General)
        {
            return "장군";
        }
        else
        {
            return "병사";
        }
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
