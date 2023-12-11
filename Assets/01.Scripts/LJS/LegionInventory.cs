using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LegionInventory : MonoBehaviour
{
    [SerializeField] private RectTransform _legionInventory;
    [SerializeField] private float _targetPos;
    [SerializeField] private float _duration;

    private Vector3 _firstPos;

    private void Awake()
    {
        _firstPos = _legionInventory.position;
    }



    private void FixedUpdate()
    {
        #region 임시 움직임
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            UIManager.Instance.UIMoveDot(_legionInventory, new Vector3(_targetPos + _firstPos.x, _firstPos.y, 0),
                _duration);
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            UIManager.Instance.UIMoveDot(_legionInventory, _firstPos,
                _duration);
        }
        #endregion
    }
}