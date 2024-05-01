using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorillaAnimationTrigger : EnemyAnimationTrigger
{
    private EnemyGorilla Gorilla => _enemy as EnemyGorilla;

    private void VigilanceTrigger()
    {
        Gorilla.GorillaVigilance.OnVigilance();
    }
}
