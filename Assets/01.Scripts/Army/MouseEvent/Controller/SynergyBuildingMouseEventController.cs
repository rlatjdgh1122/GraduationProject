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
                    UIManager.Instance.ShowWarningUI("�ó����� ���� �ʾ� �� �� �����ϴ�.");
                }

            } //end if
            else
            {
                UIManager.Instance.ShowWarningUI("�ó����� Ȱ��ȭ���� �ʾҽ��ϴ�.");
            }
        }
    }

    public override void OnRightClickEvent()
    {

    }
}
