using Cinemachine;
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

    //ī�޶� �ý��� �� ������Ʈ�� ���� �־��ֱ�
    //-> ������Ʈ�� �����̴°�

    private void Update()
    {
        CameraMove();
        //Move();
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

    private void Move()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(2))
        {
            _dragPanMoveActive = true;
            _lastMousePosition = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(2))
        {
            _dragPanMoveActive = false;
        }

        if(_dragPanMoveActive)
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
}
