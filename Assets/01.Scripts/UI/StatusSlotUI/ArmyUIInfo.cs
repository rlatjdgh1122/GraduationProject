using System.Collections.Generic;
using UnityEngine;


public interface IValueChanger<T>
{

    public List<IValueChangeUnit<T>> Units { get; set; }

    public T Value
    {
        get;
        set;
    }

    public void ChangedValue()
    {

        foreach (var item in Units)
        {

            item.ChangedValue(Value);

        }
    }

    public void Add(IValueChangeUnit<T> v)
    {

        Units.Add(v);

    }

    public void Remove(IValueChangeUnit<T> v)
    {

        Units.Remove(v);

    }

}

public interface IValueChangeUnit<T>
{
    public void ChangedValue(T n);

}

public class ArmyUIInfo : IValueChanger<ArmyUIInfo>
{
    private string _armyName;

    public Sprite SynergySprite = null; //ó���� ���翡 ���� �������� �屺�� ������ �ٲ�
    public Sprite SkillSprite = null; //�屺 �ٲ� �ٲ�
    public Sprite UltimateSprite = null; //ó���� �������� �ȹٲ�
    public int PenguinCount = 0; //���� ���� ���� ���� �޶���
    public string ArmyName  //�̸� �ٲ𶧸��� �޶���
    {
        get => _armyName;
        set
        {
            _armyName = value;
            (this as IValueChanger<ArmyUIInfo>).ChangedValue();
        }
    }

    public List<IValueChangeUnit<ArmyUIInfo>> Units { get; set; } = new();

    public ArmyUIInfo Value { get; set; } = null;

    public void AddCount(int value = 1)
    {
        PenguinCount += value;
    }

    public void RemoveCount(int value = 1)
    {
        PenguinCount -= value;
    }


}
