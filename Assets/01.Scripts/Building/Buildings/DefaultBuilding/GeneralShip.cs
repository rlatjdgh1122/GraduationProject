using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShip : MonoBehaviour
{
    //private GeneralUI _generalUI
    //{
    //    get
    //    {
    //        UIManager.Instance.overlayUIDictionary.TryGetValue(UIType.General, out NormalUI generalUI);
    //        return (GeneralUI)generalUI;
    //    }
    //}

    private void OnMouseDown()
    {
        Debug.Log("ÀÀ´­·È¾î");
        //_generalUI.EnableUI(0.5f, null);
    }

    private void Start()
    {
        transform.DOMoveY(-0.1f, 3.2f).SetLoops(-2, LoopType.Yoyo);
        transform.DORotate(new Vector3(-1.2f, -60, 0f), 3.2f).SetLoops(-2, LoopType.Yoyo);
    }
}
