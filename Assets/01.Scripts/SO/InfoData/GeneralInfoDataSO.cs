using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("�屺 �нú� ���� UI")]
    public string _type;
    [TextArea()] public string _characteristic;

    public new General Owner { get; private set; }
    public void SetOwner(General owner)
        => Owner = owner;

}
