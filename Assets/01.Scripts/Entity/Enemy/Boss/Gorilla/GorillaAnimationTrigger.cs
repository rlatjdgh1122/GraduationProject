using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GorillaAnimationTrigger : EnemyAnimationTrigger
{
    private EnemyGorilla Gorilla => _enemy as EnemyGorilla;

    [SerializeField] private UnityEvent EndVigilanceEvent = null;

    private void VigilanceTrigger()
    {
        Gorilla.VigilanceSkill.PlaySkill();
    }

    private void EndVigilanceTrigger()
    {
        EndVigilanceEvent?.Invoke();
    }
}
