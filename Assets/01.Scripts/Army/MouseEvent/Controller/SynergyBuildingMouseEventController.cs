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
            if (selectedArmy.CheckEmpty()) return;

            if (selectedArmy.IsSynergy)
            {
                if (selectedArmy.SynergyType == compo.BuildingSynergyType)
                {
                    compo.SetArmy(selectedArmy);
                    compo.OnClick();

                } //end if
                else
                {
                    UIManager.Instance.ShowWarningUI("시너지가 맞지 않아 들어갈 수 없습니다.");
                }

            } //end if
            else
            {
                UIManager.Instance.ShowWarningUI("시너지가 활성화되지 않았습니다.");
            }
        }
    }

    public override void OnRightClickEvent()
    {

    }
}
