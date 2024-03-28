using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SettingSoliderType
{
    public PenguinTypeEnum type;
    public Penguin obj;
}

[CreateAssetMenu(menuName = "SO/SoldierType/list")]
public class SoldierListSO : ScriptableObject
{
    public List<SettingSoliderType> soldierTypes;
}
