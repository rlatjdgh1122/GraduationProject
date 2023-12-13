using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [Header("ī�޶� ������")]
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private int _edgeScrollSize = 20;
    [SerializeField] private CinemachineVirtualCamera _cinemachineCam;

    [Header("ī�޶� Ȯ��&���")]
    [SerializeField] private float _fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;

    [Header("ī�޶� ȸ��")]
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

        // ���콺 ȭ�� �������� �̵� ó��
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
