using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFeedback : Feedback
{
    public SoundName soundName;

    public override bool StartFeedback()
    {
        SoundManager.Play3DSound(soundName, transform.position);

        return true;
    }

    public override bool FinishFeedback()
    {
        return true;
    }
}
