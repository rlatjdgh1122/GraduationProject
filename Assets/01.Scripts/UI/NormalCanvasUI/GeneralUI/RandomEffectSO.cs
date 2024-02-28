using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RandomTreeType
{
    General,
    Legion
}

[Serializable]
public class Effects
{
    public StatType statType;
    public StatMode statMode;
    public int increasePercentage;
    public string description;
}

[CreateAssetMenu(menuName = "SO/RandomEffect")]
public class RandomEffectSO : ScriptableObject
{
    public List<Effects> generalData;
    public List<Effects> legionData;
}
