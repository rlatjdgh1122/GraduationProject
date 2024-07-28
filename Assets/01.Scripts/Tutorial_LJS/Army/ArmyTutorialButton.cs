using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyTutorialButton : MonoBehaviour
{
    [SerializeField] private TutorialInfoUI _tutorialUI;
    [SerializeField] private ArmyTutorial _armyTutorial;

    private List<GameObject> _selectArmyList = new();
    private List<GameObject> _getGeneralList = new();
    private List<GameObject> _insetGeneralInArmyList = new();

    public void SelectArmyButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(0));
    }

    public void GetGeneralButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(1));
    }

    public void InsetGeneralInArmyButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(2));

        UIManager.Instance.HidePanel("ArmyUI");
    }
}