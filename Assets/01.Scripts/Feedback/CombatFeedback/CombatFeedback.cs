using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatFeedback : Feedback
{
    public abstract float Value { get; set; }
}
