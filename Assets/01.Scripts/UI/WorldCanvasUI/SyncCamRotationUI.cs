using Define.CamDefine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SyncCamRotationUI : MonoBehaviour
{
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Cam.MainCam;
    }

    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Cam.MainCam.transform.position);
    }
}