using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("�屺 �нú� ����")]
    public string _type;
    [TextArea()] public string _characteristic;

    public General _owner;

    public void SetOwner(General owner)
        => _owner = owner;

}
