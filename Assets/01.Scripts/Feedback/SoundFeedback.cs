using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : Feedback
{
    public SoundName soundName;

    public override void CreateFeedback()
    {
        SoundManager.Play3DSound(soundName, transform.position);
    }

    public override void FinishFeedback()
    {
    }
}
