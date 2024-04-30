using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildingAnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnEndAnimationEvent = null;

    public void EndAnimationTrigger()
    {
        OnEndAnimationEvent?.Invoke();
    }
}
