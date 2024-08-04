using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MouseEventController : MonoBehaviour
{
    public abstract void OnLaftClickEvent(RaycastHit hit);

    public abstract void OnRightClickEvent();
}
