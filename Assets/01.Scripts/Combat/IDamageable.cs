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
    BearSlashHit,
    BiteHit,
    IceHit,
    MopHit,
    MagicHit,
    BuildHit,
    MortarGroundStrikeHit,
    DashHit,
    KatanaHit,
    LanceUltimateHit,
}

public interface IDamageable
{
    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType, TargetObject hitTarget, bool isFeedback = true);
}

public interface IKnockbackable
{
    public void Knockback(float value, Vector3 normal = default);
}

public interface IStunable
{
    public void Stun(float value);
}

public interface IProvokedable
{
    public void Provoked(float value);
}
