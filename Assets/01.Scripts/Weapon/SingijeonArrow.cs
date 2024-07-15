using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SingijeonArrow : Arrow, IParabolicProjectile
{
    public override void Setting(TargetObject owner, LayerMask layer)
    {
        base.Setting(owner, layer);
    }

    public void ExecuteAttack(Vector3 startPosition, Vector3 targetPosition, float maxTime, bool isPool)
    {
        SoundManager.Play3DSound(SoundName.ArrowHit, transform.position);
        StartCoroutine(Parabola.ParabolaMove(this,_rigid, startPosition, targetPosition, maxTime, isPool, false));
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        base.OnTriggerEnter(coll);
    }


}
