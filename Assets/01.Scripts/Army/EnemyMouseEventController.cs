using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMouseEventController : MonoBehaviour
{
    public UnityEvent OnClickToEnemyArmyEvent = null;

    public void OnLaftClickEvent(RaycastHit hit)
    {
        if (hit.collider.transform.TryGetComponent(out EnemyMouseEventHandler compo))
        {
            if (ArmyManager.Instance.CurArmy.TargetEnemyArmy == compo.EnemyArmy) return;

            compo.OnClick();
            OnClickToEnemyArmyEvent?.Invoke();
        }

        /*else //허공을 클릭할 경우
        {
            ArmyManager.Instance.SetTargetEnemyArmy(null);
            EnemyArmyManager.Instance.DeSelected();
        }*/
    }

    public void OnRightClickEvent()
    {
        ArmyManager.Instance.SetTargetEnemyArmy(null);
    }

}
