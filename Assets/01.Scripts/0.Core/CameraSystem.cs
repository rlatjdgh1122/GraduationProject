using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;

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
    [SerializeField] private float _scrollAmount = 5f;

    [Header("카메라 회전")]
    [Range(0f, 9f)]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float zoomSpeed = 10f;

    private float targetFieldOfView = 50f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;


    [Header("카메라 이동 범위")]
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
    private CinemachineTransposer transposer;

    private Vector3 _startPosition;
    private Quaternion _vCamstartRotation;

    private Vector3 prevPos = Vector3.zero;

    private void Awake()
    {
        isMoving = true;
        _startPosition = transform.position;
        _vCamstartRotation = _cinemachineCam.transform.rotation;

        transposer = _cinemachineCam.GetCinemachineComponent<CinemachineTransposer>();

        PenguinManager.Instance.GetComponent_CameraSystem(this);
    }

    private void LateUpdate()
    {
        if (UIManager.Instance.currentPopupUI.Count <= 0)
        {
            if(!isMoving) { isMoving = true; }
            CameraControl();
            CameraRotate();
            CameraZoomHandle();
            CameraMove();
        }
        else
        {
            isRotating = false;
        }
    }

    private void CameraControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _cinemachineCam.transform.rotation = _vCamstartRotation;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Look(_startPosition);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = true;
        }
    }

    public void Look(Vector3 pos)
    {
        targetFieldOfView = 50f;

        isMoving = false;
        isRotating = false;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        _cinemachineCam.transform.rotation = _vCamstartRotation;
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

            // 마우스 화면 끝에서의 이동 처리
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

            transform.position += moveDir * _moveSpeed * Time.unscaledDeltaTime;
        }
    }

    private void CameraZoomHandle()
    {
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += _scrollAmount;
        }
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= _scrollAmount;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, _fieldOfViewMax);


        _cinemachineCam.m_Lens.FieldOfView
            = Mathf.Lerp(_cinemachineCam.m_Lens.FieldOfView, targetFieldOfView, Time.unscaledDeltaTime * zoomSpeed);
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

    public void SetCameraTartget(Vector3 target)
    {
        prevPos = transform.position;

        Vector3 vec = new Vector3(target.x, transform.position.y, target.z);
        transform.DOMove(vec, 0.5f);
    }

    public void CaemraAllStop()
    {

    }
}
