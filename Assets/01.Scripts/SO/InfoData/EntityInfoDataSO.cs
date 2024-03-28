using System;
using UnityEngine;

public enum PenguinUniqueType
{
    Fight,
    Production
}

public enum PenguinJobType
{
    General,
    Solider
}

public class EntityInfoDataSO : ScriptableObject
{
    [Header("Penguin UI Data")]
    public Sprite PenguinIcon;
    public PenguinUniqueType UniqueType;
    public PenguinJobType JobType;
    public PenguinTypeEnum PenguinType;
    public string PenguinName;
    public string Weapon;

    [Range(0f, 1f)] public float hp;
    [Range(0f, 1f)] public float atk;
    [Range(0f, 1f)] public float range;

    public Entity Owner { get; private set; }
    public void SetOwner(Entity owner)
    => Owner = owner;
}
