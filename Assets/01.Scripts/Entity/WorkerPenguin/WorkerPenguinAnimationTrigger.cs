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

    public void WorkTrigger()
    {
        //³ªÁß¿¡
    }
}
