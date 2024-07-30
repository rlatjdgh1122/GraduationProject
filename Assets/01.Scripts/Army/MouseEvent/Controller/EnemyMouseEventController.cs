using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMouseEventController : MouseEventController
{
    public UnityEvent OnClickToEnemyArmyEvent = null;

    public override void OnLaftClickEvent(RaycastHit hit)
    {
        if (hit.collider.transform.TryGetComponent(out EnemyMouseEventHandler compo))
        {
            if (ArmyManager.Instance.CurArmy.TargetEnemyArmy == compo.Target) return;

            compo.OnClick();
            OnClickToEnemyArmyEvent?.Invoke();
        }

        /*else //허공을 클릭할 경우
        {
            ArmyManager.Instance.SetTargetEnemyArmy(null);
            EnemyArmyManager.Instance.DeSelected();
        }*/
    }

    public override void OnRightClickEvent()
    {
        ArmyManager.Instance.SetTargetEnemyArmy(null);
    }

}
