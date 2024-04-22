using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("장군 패시브 설명")]
    [TextArea()] public string Type;
    [TextArea()] public string Characteristic;
}
