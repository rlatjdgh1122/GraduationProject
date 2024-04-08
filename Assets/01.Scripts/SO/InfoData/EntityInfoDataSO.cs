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

    [Range(0f, 1f)] public float hp;
    [Range(0f, 1f)] public float atk;
    [Range(0f, 1f)] public float range;
}
