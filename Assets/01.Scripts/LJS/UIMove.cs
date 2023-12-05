using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;
using static UnityEngine.Rendering.DebugUI;

public class UIMove : MonoBehaviour
{
    [SerializeField] private ClickBuilding _building;
    private Transform _spawnUI;
    private Transform _legionUI;

    [SerializeField] private float _spawnEndValue;
    [SerializeField] private float _legionEndValue;

    private float _curSpawnTrm;
    private float _curLegionTrm;

    bool isClick;

    private void Awake()
    {
        _spawnUI = transform.Find("SpawnUI").GetComponent<RectTransform>();
        _legionUI = transform.Find("LegionUI").GetComponent<RectTransform>();

        _curSpawnTrm = _spawnUI.transform.position.y;
        _curLegionTrm = _legionUI.transform.position.x;
        _building.ClickEvent += OnClick;
    }

    private void OnClick(bool value)
    {
        isClick = value;
    }

    private void Update()
    {
        if (isClick)
        {
            _spawnUI.DOMoveY(_spawnEndValue, 0.1f);
        }
        else
        {
            _spawnUI.DOMoveY(_curSpawnTrm, 0.1f);
        }
    }



    #region 군단버튼

    public void OnLegion()
    {
        isClick = false;
        _legionUI.DOMoveX(_legionEndValue, 0.1f);
    }

    public void OnLegionEsc()
    {
        isClick = true;
        _legionUI.DOMoveX(_curLegionTrm, 0.1f);
    }
    #endregion
}
