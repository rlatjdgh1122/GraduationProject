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
    [Range(0.1f, 9f)]
    [SerializeField] private float _rotateSpeed;

    private float targetFieldOfView = 50f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;

    private void Update()
    {
        CameraMove();
        CameraZoomHandle();
        CameraRotate();
    }

    private void CameraMove()
    {
        float xInput = 0;
        float yInput = 0;
        Vector3 inputDir = new Vector3(xInput, 0, yInput).normalized;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        // 마우스 화면 끝에서의 이동 처리
        Vector3 mousePosition = Input.mousePosition;
        if (mousePosition.x <= _edgeScrollSize)
        {
            moveDir -= transform.right;
        }
        else if (mousePosition.x >= Screen.width - _edgeScrollSize)
        {
            moveDir += transform.right;
        }

        if (mousePosition.y <= _edgeScrollSize)
        {
            moveDir -= transform.forward;
        }
        else if (mousePosition.y >= Screen.height - _edgeScrollSize)
        {
            moveDir += transform.forward;
        }

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
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 왼쪽 버튼을 누르면 회전 시작
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // 마우스 왼쪽 버튼을 뗄 때 회전 종료
            isRotating = false;
        }

        if (isRotating)
        {
            // 마우스를 드래그한 거리에 따라 회전 방향 계산
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            float rotateDirX = deltaMousePosition.x;

            // 회전 적용
            _cinemachineCam.transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime, 0);
            transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime);

            // 마우스 위치 업데이트
            lastMousePosition = Input.mousePosition;
        }
    }
}
