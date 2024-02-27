using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShip : MonoBehaviour
{
    private GeneralUI _generalUI
    {
        get
        {
            UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.General, out NormalUI generalUI);
            return (GeneralUI)generalUI;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("ÀÀ´­·È¾î");
        _generalUI.EnableUI(0.5f, null);
    }
}
