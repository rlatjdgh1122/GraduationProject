using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickBuilding : MonoBehaviour
{
    public event Action<bool> ClickEvent;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(pos, out hit, Camera.main.farClipPlane))
            {
                if(hit.collider.CompareTag("Building"))
                {
                    ClickEvent?.Invoke(true);
                }
                else
                {
                    ClickEvent?.Invoke(false);
                }
            }
        }
    }
}
