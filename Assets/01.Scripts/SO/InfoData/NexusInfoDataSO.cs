using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/InfoData/Nexus")]
public class NexusInfoDataSO : ScriptableObject
{
    public int currentMaxHealth;
    public int previousMaxHealth;
    public int nextMaxHealth;
    public int currentWorkerCount;
    public int previousWorkerCount;
    public int nextWorkerCount;
    public BuildingItemInfo previewBuilding; //���� ������ �� �رݵǴ� �ǹ��� ��������
    public BuildingItemInfo unlockedBuilding;
}
