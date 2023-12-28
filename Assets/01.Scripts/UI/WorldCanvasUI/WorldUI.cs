using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define.CamDefine;

public abstract class WorldUI : MonoBehaviour
{
    protected Canvas canvas;

    public virtual void Awake()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        canvas.worldCamera = Cam.MainCam;
    }

    public virtual void Update()
    {
        Vector3 cameraRotation = Cam.MainCam.transform.rotation * Vector3.forward;
        Vector3 posTarget = transform.position + cameraRotation;
        //Vector3 orientationTarget = _cam.transform.rotation * Vector3.forward;
        transform.LookAt(posTarget);
    }

    public abstract void EnableUI(float time);
}
