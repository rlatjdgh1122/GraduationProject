using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSound : MonoBehaviour
{
    private void OnEnable()
    {
        SignalHub.OnEndQuestEvent += OnQuestStartSound;
        SignalHub.OnStartQuestEvent += OnQuestEndSound;
    }

    private void OnDisable()
    {
        SignalHub.OnEndQuestEvent -= OnQuestStartSound;
        SignalHub.OnStartQuestEvent -= OnQuestEndSound;
    }
    public void OnQuestStartSound()
    {
        SoundManager.Play2DSound(SoundName.QuestStart);
    }

    public void OnQuestEndSound()
    {
        SoundManager.Play2DSound(SoundName.QuestEnd);
    }
}
