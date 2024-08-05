using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    [SerializeField] private GlitchEffectController _screenController;

    [SerializeField] private List<Tutorial> _tutorialData = new();

    public TutorialController TutorialControllerCompo { get; set; }

    public override void Awake()
    {
        TutorialControllerCompo = GetComponent<TutorialController>();
    }

    private void Start()
    {
        _screenController.SetValue(100);
        _screenController.StartScreen(0, StartDialog);
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

    public void EndTutorial(string sceneName)
    {
        _screenController.EndScreen(100, () => SceneManager.LoadScene(sceneName));
    }
}