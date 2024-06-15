using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMouseEventController : MonoBehaviour
{

    public UnityEvent OnClickToEnemyArmyEvent = null;
    public void OnRaftClickEvent(RaycastHit hit)
    {
        if (hit.collider.transform.TryGetComponent(out EnemyMouseEventHandler compo))
        {
            compo.OnClick();
            OnClickToEnemyArmyEvent?.Invoke();
        }//end if

        /*else
        {
            EnemyArmyManager.Instance.DeSelected();
            ArmyManager.Instance.SetMoveForcusCommand();
        }*/
    }

}
