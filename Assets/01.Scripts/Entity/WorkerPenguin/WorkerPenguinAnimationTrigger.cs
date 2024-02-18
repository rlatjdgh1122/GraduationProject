using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerPenguinAnimationTrigger : MonoBehaviour
{
    private Worker _worker;

    private void Awake()
    {
        _worker = transform.parent.GetComponent<Worker>();
    }

    private void WorkTrigger()
    {
        _worker.AnimationTrigger();
        _worker.HitResource();
    }
}
