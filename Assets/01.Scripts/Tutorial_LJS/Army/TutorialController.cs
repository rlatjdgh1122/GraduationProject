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
    [SerializeField] private TutorialUI _tutorialUI;
    [SerializeField] private GlitchEffectController _glitchEffectController;

    public List<TutorialQuest> QuestList = new();

    public void Quest()
    {
        int index = TutorialM.Instance.TutorialIndex;

        _tutorialUI.ShowPanel(QuestList[index]);
        QuestList[index].TutorialEvent?.Invoke();
    }

    public string CurrentTutorial(int index)
    {
        int currentIndex = TutorialM.Instance.TutorialIndex;

        return QuestList[currentIndex].TutorialDescription[index];
    }

    public string CurrentTutorial()
    {
        int currentIndex = TutorialM.Instance.TutorialIndex;

        return QuestList[currentIndex].TutorialTitle;
    }
}