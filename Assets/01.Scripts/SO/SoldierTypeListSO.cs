using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoldierType
{
    public PenguinTypeEnum type;
    public Penguin obj;
}

[CreateAssetMenu(menuName = "SO/SoldierType/list")]
public class SoldierTypeListSO : ScriptableObject
{
    public List<SoldierType> soldierTypes;
}
