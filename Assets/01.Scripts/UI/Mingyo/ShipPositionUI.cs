using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPositionUI : MonoBehaviour
{
    private Transform _shipPos;
    private Vector3 _shipScreenPos
    {
        get
        {
            if (_shipPos == null)
            {
                //¿Ã∞‘ ø÷ null
                _shipPos = GameObject.FindObjectOfType<GeneralShip>().transform.GetChild(0).transform;
            }

            Vector3 pos = Define.CamDefine.Cam.MainCam.WorldToScreenPoint(_shipPos.position);

            return new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                               Mathf.Clamp(pos.y, 50f, Screen.height - 50f));
        }
    }

    private void Update()
    {
        gameObject.rectTransform().position = _shipScreenPos;
        //gameObject.rectTransform().Rotate((gameObject.rectTransform().position - _shipScreenPos).normalized);
    }


}
