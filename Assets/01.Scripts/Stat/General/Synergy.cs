using System;
using UnityEngine;

public enum SynergyEnum
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
    public SynergyEnum type;
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
