using UnityEngine;

public struct ArmyUIInfo
{
    public Sprite SynergySprite { get; private set; }
    public Sprite SkillSprite { get; private set; }
    public Sprite UltimateSprite { get; private set; }
    public int PenguinCount { get; private set; }
    public string ArmyName { get; private set; }

    public ArmyUIInfo(Sprite synergySprite, Sprite skillSprite, Sprite ultimateSprite, int penguinCount, string penguinName)
    {
        SynergySprite = synergySprite;
        SkillSprite = skillSprite;
        UltimateSprite = ultimateSprite;
        PenguinCount = penguinCount;
        ArmyName = penguinName;
    }
}
