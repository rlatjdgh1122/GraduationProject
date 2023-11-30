using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickBuilding : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;

    public bool _isClick;
    public bool IsClick => _isClick;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray pos = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(pos, out hit, _whatIsGround))
            {
                if(hit.collider.gameObject.CompareTag("Building"))
                {
                    _isClick = true;
                }
                else
                {
                    _isClick = false;
                }
            }
        }
    }
}
