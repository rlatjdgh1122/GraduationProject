using UnityEngine;

public class ArmyUIInfo
{
    public Sprite SynergySprite = null; //처음에 병사에 따라 정해지고 장군이 있을때 바뀜
    public Sprite SkillSprite = null; //장군 바뀔때 바뀜
    public Sprite UltimateSprite = null; //처음에 정해지고 안바뀜
    public int PenguinCount = 0; //군단 병사 수에 따라 달라짐
    public string ArmyName = string.Empty; //이름 바뀔때마다 달라짐   

    public OnValueUpdated<ArmyUIInfo> OnValueUpdated = null;
    public void AddCount(int value = 1)
    {
        PenguinCount += value;
    }

    public void RemoveCount(int value = 1)
    {
        PenguinCount -= value;
    }


}
