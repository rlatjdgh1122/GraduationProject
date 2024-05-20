using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskingUI : PopupUI
{
    private void Start()
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }
}
