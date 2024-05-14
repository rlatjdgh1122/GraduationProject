using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum PenguinJobType
{
    General,
    Solider
}

public class EntityInfoDataSO : ScriptableObject
{
    [Header("Penguin UI Data")]
    public Sprite PenguinIcon;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
    public string PenguinName;
    public string Weapon;
    public string PenguinTypeName;
    public string PenguinDescription;
    public string PenguinPersonality;

    [Range(0f, 1f)] public float hp;
    [Range(0f, 1f)] public float atk;
    [Range(0f, 1f)] public float range;

    public string LegionName { get; set; } = "소속된 군단 없음";
    public int SlotIdx { get; set; }
}
