using Define.CamDefine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeRemain : MonoBehaviour
{
    private Canvas _canvas;
    private TextMeshProUGUI _container;

    private bool installing;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();


        _canvas.worldCamera = Cam.MainCam;

        _container = _canvas.transform.Find("TimeRemainText").GetComponent<TextMeshProUGUI>();
        _container.enabled = false;
    }

    public void OnRemainUI()
    {
        _container.enabled = true;
        installing = true;
    }

    public void OffRemainUI()
    {
        _container.enabled = false;
        installing = false;
    }

    public void SetText(int time)
    {
        _container.text = $"{time}≈œ ≥≤¿Ω";
    }

    private void Update()
    {
        if (installing)
        {
            _container.transform.rotation = Quaternion.LookRotation(_container.transform.position - Cam.MainCam.transform.position);
        }
    }
}
