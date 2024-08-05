using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialArmyMove : MonoBehaviour
{
    [SerializeField] private TutorialWorldCanvas _tutorialCanvas;
    [SerializeField] private ParticleSystem _clickHere;

    [SerializeField] private TutorialInfoUI _tutorialUI;
    [SerializeField] private TutorialController _armyTutorial;

    public UnityEvent OnAction = null;

    public void Show()
    {
        _clickHere.Play();
        _tutorialCanvas.Init(TutorialImage.Arrow_Right);
        _tutorialCanvas.SetTarget(transform, 4f);
        _tutorialCanvas.ShowUI();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(1))
        {
            _tutorialUI.CompleteSlot(_armyTutorial.CurrentTutorial(0, 1));
            _tutorialCanvas.FadeOutImmediately();
            _clickHere.Stop();

            StartCoroutine(StartActionCoroutine());
        }
    }

    private IEnumerator StartActionCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        OnAction?.Invoke();
    }
}