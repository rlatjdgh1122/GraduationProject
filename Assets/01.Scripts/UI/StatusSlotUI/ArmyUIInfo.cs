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

    public Sprite SynergySprite = null; //처음에 병사에 따라 정해지고 장군이 있을때 바뀜
    public Sprite SkillSprite = null; //장군 바뀔때 바뀜
    public Sprite UltimateSprite = null; //처음에 정해지고 안바뀜
    public int PenguinCount = 0; //군단 병사 수에 따라 달라짐
    public string ArmyName  //이름 바뀔때마다 달라짐
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
