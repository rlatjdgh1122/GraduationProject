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
    private Camera _cam;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _cam = Cam.MainCam;
        _canvas.worldCamera = _cam;

        _container = _canvas.transform.Find("TimeRemainText").GetComponent<TextMeshProUGUI>();
    }

    public void Text(int time)
    {
        _container.text = $"{time}≈œ ≥≤¿Ω";
    }

    private void Update()
    {
        _container.transform.rotation = Quaternion.LookRotation(_container.transform.position - _cam.transform.position);
    }
}
