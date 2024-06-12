using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMouseEventController : MonoBehaviour
{
    public void OnRaftClickEvent(RaycastHit hit)
    {
        if (hit.collider.transform.TryGetComponent(out EnemyMouseEventHandler compo))
        {
            compo.OnClick();
        }//end if
        else
        {
            EnemyArmyManager.Instance.DeSelected();
            ArmyManager.Instance.SetTargetEnemyArmy(null);
        }
    }

}
