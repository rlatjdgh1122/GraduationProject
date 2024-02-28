using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    [Header("카메라 움직임")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _edgeScrollSize = 20;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [SerializeField] private float _dragSpeed = 2f;
    private bool _dragPanMoveActive;
    private Vector2 _lastMousePosition;

    [Header("카메라 확대&축소")]
    [SerializeField] private float _fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;

    [Header("카메라 회전")]
    [Range(0.1f, 9f)]
    [SerializeField] private float _rotateSpeed;

    private float targetFieldOfView = 50f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;

    private bool isMoving = false;
    private void Update()
    {
        CameraControl();
        CameraMove();
        //Move();
        CameraZoomHandle();
        CameraRotate();
    }

    private void CameraControl()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            isMoving = !isMoving;

            transform.position = new Vector3(0, transform.position.y, 0);
            //transform.Rotate(0f, 0f, 0f);
            //_cinemachineCam.transform.Rotate(50f, 0f, 0f);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            _cinemachineCam.transform.rotation = Quaternion.Euler(new Vector3(50, 0, 0));
        }
    }

    private void CameraMove()
    {
        if (isMoving)
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
    }

    private void Move()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(2))
        {
            _dragPanMoveActive = true;
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            _dragPanMoveActive = false;
        }

        if (_dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - _lastMousePosition;

            Debug.Log(mouseMovementDelta);

            inputDir.x = mouseMovementDelta.x * _dragSpeed;
            inputDir.z = mouseMovementDelta.y * _dragSpeed;

            _lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = transform.forward * inputDir.x + transform.right * inputDir.z;

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
