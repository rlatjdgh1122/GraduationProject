using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingImage : MonoBehaviour
{
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

    private void Start()
    {
        gameObject.SetActive(false);
        CoroutineUtil.CallWaitForSeconds(1f, () => { gameObject.SetActive(true); });
    }

}