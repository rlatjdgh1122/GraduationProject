using Cinemachine;
using UnityEngine;

public class DummyPenguinCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;

    private void Awake()
    {
        PenguinManager.Instance.GetComponent_DummyCameraSystem(this);
    }

    public void SetCamera(Transform trm)
    {
        PenguinManager.Instance.CameraCompo.IsMoving = false;
        _cinemachineCam.gameObject.SetActive(true);
        _cinemachineCam.Follow = trm;
    }

    public void DisableCamera()
    {
        PenguinManager.Instance.CameraCompo.IsMoving = true;
        _cinemachineCam.Follow = null;
        _cinemachineCam.gameObject.SetActive(false);
    }
}