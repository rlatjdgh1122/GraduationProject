using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SynergySettingData
{
    public SynergyType SynergyType;
    public UltimateType UltimateType;
    public Sprite UltimateIcon;
    public Sprite SynergyIcon;
}

[System.Serializable]
public class GeneralSettingData
{
    public GeneralType GeneralType;
    public SkillType SkillType;
    public Sprite SkillIcon;
}

[CreateAssetMenu(menuName = "SO/Register/StatusSolt")]
public class StatusSlotRegisterSO : ScriptableObject
{
    public List<SynergySettingData> SynergyData = new();
    public List<GeneralSettingData> GeneralData = new();
}

