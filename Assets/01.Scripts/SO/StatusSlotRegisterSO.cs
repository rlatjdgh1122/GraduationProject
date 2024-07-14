using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillDefaultData
{
    public Sprite Icon;
    public string Name;
    [TextArea] public string Explain;
}

[System.Serializable]
public class SynergySettingData
{

    public SynergyType SynergyType;
    public SynergyData SynergyInfo;
    public UltimateData UltimateInfo;

    [System.Serializable]
    public class SynergyData : SkillDefaultData
    {

    }

    [System.Serializable]
    public class UltimateData : SkillDefaultData
    {
        public UltimateType UltimateType;
    }
}

[System.Serializable]
public class GeneralSettingData
{
    public GeneralType GeneralType;
    public SkillData SkillInfo;

    [System.Serializable]
    public class SkillData : SkillDefaultData
    {
        public SkillType SkillType;
    }

}

[CreateAssetMenu(menuName = "SO/Register/StatusSolt")]
public class StatusSlotRegisterSO : ScriptableObject
{
    public List<SynergySettingData> SynergyData = new();
    public List<GeneralSettingData> GeneralData = new();
}

