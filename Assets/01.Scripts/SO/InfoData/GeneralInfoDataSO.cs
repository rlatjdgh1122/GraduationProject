using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("장군 패시브 설명 UI")]
    public string _type;
    [TextArea()] public string _characteristic;

    public new General Owner { get; private set; }
    public void SetOwner(General owner)
        => Owner = owner;

}
