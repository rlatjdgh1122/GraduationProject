using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingSoldierType
{
    public PenguinTypeEnum Type;
    public Penguin Obj;
    public BaseStat Stat;
    public EntityInfoDataSO InfoData;
}

[CreateAssetMenu(menuName = "SO/Register/Solider")]
public class SoldierRegisterSO : ScriptableObject
{
    public List<SettingSoldierType> Data = new();
}
