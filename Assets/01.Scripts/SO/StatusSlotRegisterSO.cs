using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SynergySettingData
{

    public SynergyType SynergyType;
    public SynergyData SynergyInfo;
    public UltimateData UltimateInfo;

    [System.Serializable]
    public struct SynergyData
    {
        public Sprite SynergyIcon;
        public string SynergyName;
        [TextArea] public string SynergyExplain;
    }

    [System.Serializable]
    public struct UltimateData
    {
        public Sprite UltimateIcon;
        public UltimateType UltimateType;
        public string UltimateName;
        [TextArea] public string UltimateExplain;
    }
}

[System.Serializable]
public class GeneralSettingData
{
    public GeneralType GeneralType;
    public SkillData SkillInfo;

    [System.Serializable]
    public struct SkillData
    {
        public SkillType SkillType;
        public Sprite SkillIcon;
        public string SkillName;
        [TextArea] public string SkillExplain;
    }

}

[CreateAssetMenu(menuName = "SO/Register/StatusSolt")]
public class StatusSlotRegisterSO : ScriptableObject
{
    public List<SynergySettingData> SynergyData = new();
    public List<GeneralSettingData> GeneralData = new();
}

