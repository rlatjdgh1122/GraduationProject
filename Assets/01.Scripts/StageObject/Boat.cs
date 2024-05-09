using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : PoolableMono
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            OnSink(); //Debug
        }
    }

    public void OnSink()
    {
        transform.DOMoveY(-15f, 10f).OnComplete(() => gameObject.SetActive(false));
    }
}
