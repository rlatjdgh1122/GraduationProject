using SynergySystem;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("�屺 �нú� ����")]
    [TextArea()] public string Description;
    [TextArea()] public string Synergy;
    [TextArea()] public string SkillName;
    [TextArea()] public string SkillDescription;

    public SynergyType SynergyType;
}
