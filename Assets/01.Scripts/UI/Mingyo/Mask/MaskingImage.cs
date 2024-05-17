using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingImage : MonoBehaviour
{
    private void Start()
    {
        CoroutineUtil.CallWaitForOneFrame(() => UIManager.Instance.HidePanel("Masking"));
    }

    private Transform _buttonTrm;
    public Transform ButtonTrm
    {
        get
        {
            if (_buttonTrm == null)
            {
                _buttonTrm = transform.parent.Find("MaskingButtonTrm");
            }
            return _buttonTrm;
        }
    }

}