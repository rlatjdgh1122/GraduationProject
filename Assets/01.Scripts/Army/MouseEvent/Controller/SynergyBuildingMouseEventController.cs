using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SynergyBuildingMouseEventController : MouseEventController
{

    public override void OnLaftClickEvent(RaycastHit hit)
    {
        if (hit.collider.transform.TryGetComponent(out SynergyBuildingMouseEventHandler compo))
        {
            Army selectedArmy = ArmyManager.Instance.CurArmy;
            if (selectedArmy.IsSynergy && selectedArmy.SynergyType == compo.BuildingSynergyType)
            {
                compo.SetArmy(selectedArmy);
                compo.OnClick();
            }
            else
            {
                UIManager.Instance.ShowWarningUI("해당 군단은 들어갈 수 없습니다.");
            }
        }
    }

    public override void OnRightClickEvent()
    {

    }
}
