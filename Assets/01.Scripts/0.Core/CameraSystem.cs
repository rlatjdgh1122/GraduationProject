using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSystem : MonoBehaviour
{
    [Header("ī�޶� ������")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _edgeScrollSize = 20;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [SerializeField] private float _dragSpeed = 2f;
    private bool _dragPanMoveActive;
    private Vector2 _lastMousePosition;

    [Header("ī�޶� Ȯ��&���")]
    [SerializeField] private float _fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;

    [Header("ī�޶� ȸ��")]
    [Range(0.1f, 9f)]
    [SerializeField] private float _rotateSpeed;

    private float targetFieldOfView = 50f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;


    [Header("ī�޶� �̵� ����")]
    [Range(-100f, 100f)]
    [SerializeField] private float minXValue = -50f;
    [Range(-100f, 100f)]
    [SerializeField] private float maxXValue = 50f;
    [Range(-100f, 100f)]
    [SerializeField] private float minZValue = -50f;
    [Range(-100f, 100f)]
    [SerializeField] private float maxZValue = 50f;


    private bool isMoving = true;
    public bool IsMoving
    {
        get => isMoving;
        set => isMoving = value;
    }
    public CinemachineVirtualCamera CinemachineCam => _cinemachineCam;

    private Vector3 _startPosition;

    private void Awake()
    {
        isMoving = true;
        _startPosition = transform.position;
    }

    private void LateUpdate()
    {
        if (UIManager.Instance.currentPopupUI.Count != 0)
        {
            if (IsMoving == true)
            {
                IsMoving = false;
            }
            if (isRotating == true)
            {
                isRotating = false;
            }
        }
        else
        {
            if (IsMoving == false)
            {
                IsMoving = true;
            }
        }
        CameraControl();
        CameraMove();
        //Move();
        CameraZoomHandle();
        CameraRotate();
    }

    private void CameraControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        if (Input.GetKey(KeyCode.Space))
        {
            isMoving = false;
            transform.position = _startPosition;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = true;
        }
    }

    private void CameraMove()
    {
        if (isMoving &&
            !isRotating)
        {
            float xInput = 0;
            float yInput = 0;
            Vector3 inputDir = new Vector3(xInput, 0, yInput).normalized;

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            // ���콺 ȭ�� �������� �̵� ó��
            Vector3 mousePosition = Input.mousePosition;
            if (mousePosition.x <= _edgeScrollSize)
            {
                if (transform.position.x > minXValue)
                {
                    moveDir -= transform.right;
                }
            }
            else if (mousePosition.x >= Screen.width - _edgeScrollSize)
            {
                if (transform.position.x < maxXValue)
                {
                    moveDir += transform.right;
                }
            }

            if (mousePosition.y <= _edgeScrollSize)
            {
                if (transform.position.z > minZValue)
                {
                    moveDir -= transform.forward;
                }
            }
            else if (mousePosition.y >= Screen.height - _edgeScrollSize)
            {
                if (transform.position.z < maxZValue)
                {
                    moveDir += transform.forward;
                }
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
            // ���콺 ���� ��ư�� ������ ȸ�� ����
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            // ���콺 ���� ��ư�� �� �� ȸ�� ����
            isRotating = false;
        }

        if (isRotating)
        {
            // ���콺�� �巡���� �Ÿ��� ���� ȸ�� ���� ���
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            float rotateDirX = deltaMousePosition.x;

            // ȸ�� ����
            _cinemachineCam.transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime, 0);
            transform.eulerAngles += new Vector3(0, rotateDirX * _rotateSpeed * Time.deltaTime);

            // ���콺 ��ġ ������Ʈ
            lastMousePosition = Input.mousePosition;
        }
    }

    public void SetCameraTartget(Vector3 target)
    {
        Vector3 vec = new Vector3(target.x, transform.position.y, target.z);
        transform.DOMove(vec, 0.5f);
    }
}
