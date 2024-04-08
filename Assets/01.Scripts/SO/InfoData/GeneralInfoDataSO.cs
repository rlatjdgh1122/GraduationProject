using System;
using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("장군 패시브 설명")]
    public string _type;
    [TextArea()] public string _characteristic;
}
