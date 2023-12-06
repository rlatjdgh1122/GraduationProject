using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("카메라 움직임")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _edgeScrollSize = 20;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;

    [Header("카메라 확대&축소")]
    [SerializeField] private float _fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;

    [Header("카메라 회전")]
    [SerializeField] private float _rotateSpeed = 300f;

    private float targetFieldOfView = 50f;

    //카메라 시스템 빈 오브젝트를 만들어서 넣어주기
    //-> 오브젝트를 움직이는것

    private void Update()
    {
        CameraMove();
        CameraZoomHandle();
        CameraRotate();
    }

    private void CameraMove()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (xInput < 0)
        {
            inputDir.x = -1f;
        }
        if (yInput < 0)
        {
            inputDir.z = -1f;
        }
        if (xInput > 0)
        {
            inputDir.x = 1f;
        }
        if (yInput > 0)
        {
            inputDir.z = 1f;
        }


        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        transform.position += moveDir * _moveSpeed * Time.deltaTime;
    }

    private void CameraZoomHandle()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, _fieldOfViewMax);

        float zoomSpeed = 10f;
        _cinemachineCam.m_Lens.FieldOfView
            = Mathf.Lerp(_cinemachineCam.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void CameraRotate()
    {
        float rotateDirX = 0f;

        if(Input.GetKey(KeyCode.Q))
        {
            rotateDirX = -1f;
        }
        if(Input.GetKey(KeyCode.E))
        {
            rotateDirX = 1f;
        }

        _cinemachineCam.transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime, 0);
        transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime, 0);
    }
}
