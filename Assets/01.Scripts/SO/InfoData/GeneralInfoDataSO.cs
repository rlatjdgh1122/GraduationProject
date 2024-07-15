using SynergySystem;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("장군 패시브 설명")]
    [TextArea()] public string Description;
    [TextArea()] public string Synergy;
    [TextArea()] public string SkillName;
    [TextArea()] public string SkillDescription;

    public SynergyType SynergyType;
}
