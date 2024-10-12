using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMineTrap : BaseTrap
{
    protected override void CatchEnemy(Enemy enemy, RaycastHit raycastHit)
    {
        _damageCaster.CaseAoEDamage();

        SoundManager.Play3DSound(SoundName.Explosion, transform.position);
        RemoveTrap();
    }
}