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
        _cinemachineCam.gameObject.SetActive(true);
        _cinemachineCam.Follow = trm;
    }

    public void DisableCamera()
    {
        _cinemachineCam.Follow = null;
        _cinemachineCam.gameObject.SetActive(false);
    }
}