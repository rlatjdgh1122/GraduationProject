using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerPenguin : MonoBehaviour
{
    public void LookTarget(Transform target)
    {
        Vector3 directionToTarget = target.transform.position - transform.position;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5.0f);
    }
}
