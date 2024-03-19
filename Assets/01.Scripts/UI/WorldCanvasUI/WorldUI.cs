using Define.CamDefine;
using UnityEngine;

public abstract class WorldUI : MonoBehaviour
{
    protected CanvasGroup canvas;
    protected Camera cam;

    public virtual void Awake()
    {
        cam = Cam.MainCam;
        canvas = GetComponent<CanvasGroup>();
    }

    public virtual void Update()
    {
        Vector3 cameraRotation = Cam.MainCam.transform.rotation * Vector3.forward;
        Vector3 posTarget = transform.position + cameraRotation;
        //Vector3 orientationTarget = _cam.transform.rotation * Vector3.forward;
        transform.LookAt(posTarget);
    }
}
