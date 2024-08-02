using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Tutorial
{
    [TextArea]
    public string[] TutorialTexts;

    public UnityEvent EndTutorialEvent = null;
}

public class TutorialM : Singleton<TutorialM>
{
    private int _tutorialIndex = 0;
    public int TutorialIndex => _tutorialIndex;

    [SerializeField] private DialogSystem _dialogSystem;

    [SerializeField] private List<Tutorial> _tutorialData = new();


    private void Start()
    {
        StartDialog();
    }

    public void AddTutorialIndex(int count = 1)
    {
        _tutorialIndex += count;

        StartDialog();
    }

    public void StartDialog()
    {
        Tutorial tutorial = _tutorialData[TutorialIndex];

        _dialogSystem.Begin(tutorial.TutorialTexts, tutorial.EndTutorialEvent);
    }
}