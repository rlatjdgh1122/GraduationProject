using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBuilding : MonoBehaviour
{
    [SerializeField] private TutorialController _controller;

    private StrengthBuffBuilding _buffBuilding;

    public void FindBuilding()
    {
        StartCoroutine(FindCoroutine());
    }

    private IEnumerator FindCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        _buffBuilding = FindObjectOfType<StrengthBuffBuilding>();

        if(_buffBuilding != null)
            _buffBuilding.OnInstalledEvent += InstallEvent;
    }

    public void InstallEvent()
    {
        _buffBuilding.OnInstalledEvent -= InstallEvent;

        _controller.TutorialInfoUI.CompleteSlot(_controller.CurrentTutorial(1, 1));
        TutorialM.Instance.AddTutorialIndex();
    }
}