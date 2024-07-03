using System.Collections.Generic;
using UnityEngine;

public class ArmyUIInfo : IValueChanger<ArmyUIInfo>
{
    public List<IValueChangeUnit<ArmyUIInfo>> Units { get; set; } = new();

    public ArmyUIInfo Value { get => this; }

    private string _armyName = string.Empty;   //�̸� �ٲ𶧸��� �޶���
    private Sprite _synergySprite = null;      //ó���� ���翡 ���� �������� �屺�� ������ �ٲ�
    private Sprite _skillSprite = null;        //�屺 �ٲ� �ٲ�
    private Sprite _ultimateSprite = null;     //ó���� �������� �ȹٲ�
    private int _penguinCount = 0;             //���� ���� ���� ���� �޶���


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
