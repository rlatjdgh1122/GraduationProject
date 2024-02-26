using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffBuilding
{
    public abstract Collider[] BuffRunning(FeedbackPlayer feedbackPlayer, Collider[] _curcolls, Collider[] previousColls);
}
