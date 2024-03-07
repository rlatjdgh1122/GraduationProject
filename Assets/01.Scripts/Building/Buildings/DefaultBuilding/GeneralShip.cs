using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShip : MonoBehaviour
{
    private void OnMouseDown()
    {
        UIManager.Instance.ShowPanel("GeneralStore");
    }

    private void Start()
    {
        transform.DOMoveY(-0.1f, 3.2f).SetLoops(-2, LoopType.Yoyo);
        transform.DORotate(new Vector3(-1.2f, -60, 0f), 3.2f).SetLoops(-2, LoopType.Yoyo);
    }
}
