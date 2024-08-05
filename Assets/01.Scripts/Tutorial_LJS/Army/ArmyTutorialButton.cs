using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArmyTutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialInfoUI _tutorialUI;
    [SerializeField] private TutorialController _armyTutorial;

    public UnityEvent _getGeneralEvent = new();
    public UnityEvent _endTutorialEvent = new();

    public void SelectArmyButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(0, 1));
    }

    public void GetGeneralButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(1, 1));

        _getGeneralEvent?.Invoke();
    }

    public void InsetGeneralInArmyButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(2, 1));
        _endTutorialEvent?.Invoke();

        UIManager.Instance.HidePanel("ArmyUI");
        UIManager.Instance.ResetPanel();

        TutorialM.Instance.AddTutorialIndex();

    }
}