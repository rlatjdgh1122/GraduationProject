using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitType
{
    None,
    MeleeHit,
    ArrowHit,
}

public interface IDamageable
{
    public void ApplyDamage(int damage, Vector3 point, Vector3 normal, HitType hitType);
}
