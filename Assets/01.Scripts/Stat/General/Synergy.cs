using System;
using UnityEngine;

public enum SynergyType
{
    Will,
    Bless,
    Wind,
    Revolt,
}

[Serializable]
public class Synergy
{
    [SerializeField] private int _level = 1;
    public string synergyName;
    public SynergyType type;
    public Ability Stat;


    public int level
    {
        get
        {
            return _level;
        }
        set
        {
            _level = value;
            IncreaseStat();
        }
    }
    //public float baseValue;
    public int increaseValue;

    public void IncreaseStat()
    {
        Stat.Value += increaseValue;
    }

    public void InvokeOnValidate()
    {
        if (Stat != null)
        {
            Stat.OnValidate?.Invoke(Stat);
        }
    }
}
