using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TutorialArmyAttack : MonoBehaviour
{

    private void OnMouseOver()
    {
        if (TutorialM.Instance.TutorialControllerCompo.CanClick && Input.GetMouseButtonDown(0))
        {
            TutorialM.Instance.TutorialControllerCompo.TutorialInfoUI.CompleteSlot(TutorialM.Instance.TutorialControllerCompo.CurrentTutorial(1, 1));
            TutorialM.Instance.AddTutorialIndex();

            TutorialM.Instance.TutorialControllerCompo.CanClick = false;
        }
    }
}
