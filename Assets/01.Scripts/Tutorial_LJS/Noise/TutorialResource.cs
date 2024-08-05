using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TutorialResource : MonoBehaviour
{
    [SerializeField] private TutorialWorldCanvas _tutorialCanvas;
    [SerializeField] private float _yPos;
    private ResourceObjectClickTrigger _resourceObj;
    [SerializeField] private TutorialController _controller;

    private int _index;

    private void Start()
    {
        _resourceObj = GetComponent<ResourceObjectClickTrigger>();
    }

    public void ShowUI()
    {
        _tutorialCanvas.SetTarget(transform, _yPos);
        _tutorialCanvas.Init(TutorialImage.Arrow);
        _tutorialCanvas.ShowUI();

        _resourceObj.CanClick = true;
    }

    public void PlusButton()
    {
        if (_index > 5) return;
        ++_index;
    }

    public void MinusButton()
    {
        if (_index <= 0) return;
        --_index;
    }

    public void ButtonClick()
    {
        if(_index >= 2)
        {
            _controller.TutorialInfoUI.CompleteSlot(_controller.CurrentTutorial(0, 1));

            TutorialM.Instance.AddTutorialIndex();
        }
    }
}