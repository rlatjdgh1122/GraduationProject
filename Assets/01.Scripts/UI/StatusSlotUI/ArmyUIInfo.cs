using System.Collections.Generic;
using UnityEngine;

public class ArmyUIInfo : IValueChanger<ArmyUIInfo>
{
    public List<IValueChangeUnit<ArmyUIInfo>> Units { get; set; } = new();

    public ArmyUIInfo Value { get => this; }

    private string _armyName = string.Empty;   //이름 바뀔때마다 달라짐
    private Sprite _synergySprite = null;      //처음에 병사에 따라 정해지고 장군이 있을때 바뀜
    private Sprite _skillSprite = null;        //장군 바뀔때 바뀜
    private Sprite _ultimateSprite = null;     //처음에 정해지고 안바뀜
    private int _penguinCount = 0;             //군단 병사 수에 따라 달라짐


    #region event property

    public Sprite SynergySprite
    {
        get => _synergySprite;
        set
        {
            _synergySprite = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    public Sprite SkillSprite
    {
        get => _skillSprite;
        set
        {
            _skillSprite = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    public Sprite UltimateSprite
    {
        get => _ultimateSprite;
        set
        {
            _ultimateSprite = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    public int PenguinCount
    {
        get => _penguinCount;
        set
        {
            _penguinCount = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    public string ArmyName
    {
        get => _armyName;
        set
        {
            _armyName = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    #endregion

    public void AddPenguinCount(int value = 1)
    {
        PenguinCount += value;
    }

    public void RemovePenguinCount(int value = 1)
    {
        PenguinCount -= value;
    }


}
