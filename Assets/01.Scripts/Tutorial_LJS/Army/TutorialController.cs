using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TutorialQuest
{
    public string TutorialTitle;

    [TextArea]
    public string[] TutorialDescription;

    public UnityEvent TutorialEvent = null;
}

public class TutorialController : MonoBehaviour
{
    [SerializeField] public TutorialUI _tutorialUI;

    public TutorialInfoUI TutorialInfoUI;

    public List<TutorialQuest> QuestList = new();

    public bool CanClick { get; set; } = false;

    public void Quest(int index = 0)
    {
        _tutorialUI.ShowPanel(QuestList[index]);
        QuestList[index].TutorialEvent?.Invoke();
    }

    public void HideTutorialUI(float waitTime = 0)
    {
        _tutorialUI.FadeOut(waitTime);
    }

    public string CurrentTutorial(int index, int currentIndex = 0)
    {
        return QuestList[currentIndex].TutorialDescription[index];
    }
}