using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    None,
    MeleeHit,
    EnemyMeleeHit,
    ArrowHit,
    CriticalHit,
    RockHit,
    WoodHit,
    BleedHit,
    GroundStrikeHit,
}

public interface IDamageable
{
    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType);
}
