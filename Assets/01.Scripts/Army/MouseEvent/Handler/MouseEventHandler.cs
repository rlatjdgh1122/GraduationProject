using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public abstract class MouseEventHandler : MonoBehaviour
{
    protected CapsuleCollider coliderCompo = null;

    protected virtual void Awake()
    {
        coliderCompo = GetComponent<CapsuleCollider>();
    }

    protected abstract void OnMouseEnter();

    protected abstract void OnMouseExit();

    public abstract void OnClick();

    public void SetColiderActive(bool active) => coliderCompo.enabled = active;
}
