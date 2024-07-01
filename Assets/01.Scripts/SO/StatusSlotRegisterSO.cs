using SkillSystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SynergySettingData
{
    public SynergyType SynergyType;
    public Image SynergyImage;
}

public class GeneralSettingData
{
    public GeneralType GeneralType;
    public DecisionType DecisionType;
    public Image SkillImage;
}

[CreateAssetMenu(menuName = "SO/Register/StatusSolt")]
public class StatusSlotRegisterSO : ScriptableObject
{
    public List<SynergySettingData> Data = new();
}

