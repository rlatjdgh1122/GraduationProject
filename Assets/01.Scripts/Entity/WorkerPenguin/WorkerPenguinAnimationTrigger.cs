using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorkerPenguinAnimationTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnWorkEvent = null;

    private Worker _worker;

    private void Awake()
    {
        _worker = transform.parent.GetComponent<Worker>();
    }

    private void WorkTrigger()
    {
        _worker.AnimationTrigger();
        OnWorkEvent?.Invoke();
        _worker.DamageCasterCompo.CastOverlap();
    }
}
