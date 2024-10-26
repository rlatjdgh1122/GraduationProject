using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoEEffectFeedback : Feedback
{
    [SerializeField] private GameObject Effect;

    public override bool StartFeedback()
    {
        Effect.SetActive(true);
        return true;
    }

    public override bool FinishFeedback()
    {
        Effect.SetActive(false);
        return true;
    }
}
