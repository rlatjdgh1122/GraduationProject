using Cinemachine;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Space(10f)]
    [Header("ī�޶� ������")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _edgeScrollSize = 20;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;
    [SerializeField] private float _dragSpeed = 2f;
    private bool _dragPanMoveActive;
    private Vector2 _lastMousePosition;

    [Space(10f)]
    [Header("ī�޶� Ȯ��&���")]
    [SerializeField] private float _fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;
    [SerializeField] private float _scrollAmount = 5f;

    [Space(10f)]
    [Header("ī�޶� ȸ��")]
    [Range(0f, 9f)]
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float zoomSpeed = 10f;

    private float targetFieldOfView = 50f;
    private bool isRotating = false;
    private Vector3 lastMousePosition;

    [Space(10f)]
    [Header("ī�޶� �̵� ����")]
    [Range(-100f, 100f)]
    [SerializeField] private float minXValue = -50f;
    [Range(-100f, 100f)]
    [SerializeField] private float maxXValue = 50f;
    [Range(-100f, 100f)]
    [SerializeField] private float minZValue = -50f;
    [Range(-100f, 100f)]
    [SerializeField] private float maxZValue = 50f;

    [Space(10f)]
    [Header("ī�޶� Space Text ����")]
    [SerializeField] private float _distance;
    [SerializeField] private TextMeshProUGUI _goToNexusText;

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

    private bool canMoving => UIManager.Instance.currentPopupUI.Count <= 0;

    private float _startFOV = 0;

    private void Awake()
    {
        isMoving = true;
        _startPosition = transform.position;
        _vCamstartRotation = _cinemachineCam.transform.rotation;
        _startFOV = _cinemachineCam.m_Lens.FieldOfView;

        transposer = _cinemachineCam.GetCinemachineComponent<CinemachineTransposer>();

        PenguinManager.Instance.GetComponent_CameraSystem(this);
    }

    private void LateUpdate()
    {
        if (!canMoving)
        {
            if (isRotating) { isRotating = false; }
            return;
        }

        if (!isMoving) { isMoving = true; }
        CameraControl();
        CameraRotate();
        CameraZoomHandle();
        CameraMove();
    }

    private void CameraControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _cinemachineCam.transform.rotation = _vCamstartRotation;
            CheckDistanceAndFade();
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
        CheckDistanceAndFade();
    }

    private void CameraMove()
    {
        if (isMoving && !isRotating)
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

            transform.position += moveDir * _moveSpeed * Time.unscaledDeltaTime;
            CheckDistanceAndFade();
        }
    }

    public void ResetFOV()
    {
        _cinemachineCam.m_Lens.FieldOfView = _startFOV;
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

    private void CheckDistanceAndFade()
    {
        if (Vector3.Distance(transform.position, _startPosition) >= _distance)
        {
            _goToNexusText.DOFade(1, 0.5f);
        }
        else
        {
            _goToNexusText.DOFade(0, 0.5f);
        }
    }

    public void SetCameraTartget(Vector3 target)
    {
        Vector3 vec = new Vector3(target.x, transform.position.y, target.z);
        transform.DOMove(vec, 0.5f).OnComplete(CheckDistanceAndFade);
    }

    public void CaemraAllStop()
    {

    }
}
