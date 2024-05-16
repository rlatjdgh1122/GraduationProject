using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskingImage : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            UIManager.Instance.SetMaskingImagePos(Input.mousePosition);
        }
    }
}