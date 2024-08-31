using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNexus : MonoBehaviour
{
    [SerializeField] private TutorialWorldCanvas _tutorialCanvas;
    [SerializeField] private TutorialInfoUI _tutorialUI;
    [SerializeField] private TutorialController _armyTutorial;

    public void ShowArrow()
    {
        _tutorialCanvas.SetTarget(transform, 6.5f);
        _tutorialCanvas.Init(TutorialImage.Arrow);
        _tutorialCanvas.ShowUI();
    }

    public void UpgradeNexusButton()
    {
        _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(0, 0));
        TutorialM.Instance.AddTutorialIndex();
    }

    public void OnMouseDown()
    {
        _tutorialCanvas.FadeOutImmediately();
    }
}