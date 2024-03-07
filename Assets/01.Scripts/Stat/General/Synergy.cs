using System;
using UnityEngine;

public enum SynergyType
{
    Will,
    Bless,
}

[Serializable]
public class Synergy
{
    public string synergyName;
    public SynergyType type;
    [SerializeField] private int _level = 1;
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
    public float baseValue;
    public float increaseValue;

    public void IncreaseStat()
    {
        baseValue += increaseValue;
    }
}
