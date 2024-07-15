using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera originCamera;
    [SerializeField] private CinemachineVirtualCamera camera1;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    originCamera.gameObject.SetActive(false);
        //    camera1.gameObject.SetActive(true);
        //}
    }
}
