using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnOnOff : MonoBehaviour
{
    [SerializeField] private List<GameObject> _list;
    [SerializeField] private GameObject _onGameObj;

    [SerializeField] private bool _tween;

    public void OnOffObject()
    {
        for(int i =  0; i < _list.Count; ++i)
        {
            _list[i].SetActive(false);
            _onGameObj.SetActive(true);
        }
    }
}
