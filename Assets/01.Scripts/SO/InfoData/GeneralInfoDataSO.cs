using UnityEngine;


[CreateAssetMenu(menuName = "SO/InfoData/General")]
public class GeneralInfoDataSO : PenguinInfoDataSO
{
    [Header("�屺 �нú� ����")]
    [TextArea()] public string Type;
    [TextArea()] public string Characteristic;
}
