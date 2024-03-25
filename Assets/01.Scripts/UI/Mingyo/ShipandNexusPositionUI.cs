using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipandNexusPositionUI : MonoBehaviour
{
    private Transform _shipPos; // ���� ��ġ�� ��Ÿ���� Transform
    private Vector3 _nexusPos = Vector3.zero; // �ؼ����� ��ġ�� ��Ÿ���� Transform

    CameraSystem _cameraSystem;
    Button _shipbutton;
    Button _nexusbutton;

    private void Awake()
    {
        _cameraSystem = Object.FindObjectOfType<CameraSystem>();

        _shipbutton = transform.GetChild(0).GetComponent<Button>();
        _shipbutton.onClick.AddListener(() => _cameraSystem.SetCameraTartget(new Vector3(_shipPos.position.x,
                                                                                     _shipPos.position.y,
                                                                                     _shipPos.position.z + 10)));

        _nexusbutton = transform.GetChild(1).GetComponent<Button>();
        _nexusbutton.onClick.AddListener(() => _cameraSystem.SetCameraTartget(_nexusPos));
    }

    // ���� ȭ��� ��ġ
    private Vector3 _shipScreenPos
    {
        get
        {
            if (_shipPos == null)
            {
                // GeneralShip ��ũ��Ʈ�� ���� ���� ������Ʈ�� ù ��° �ڽ��� ��ġ�� ������
                _shipPos = GameObject.FindObjectOfType<GeneralShip>().transform.GetChild(0).transform;
            }

            // ������ ��ġ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 pos = Camera.main.WorldToScreenPoint(_shipPos.position);

            // ȭ�� ������ ���� ��ġ�� ������Ű�� ���� ������ ��
            return new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                               Mathf.Clamp(pos.y, 50f, Screen.height - 50f));
        }
    }

    // �ؼ����� ȭ��� ��ġ
    private Vector3 _nexusScreenPos
    {
        get
        {
            // ������ ��ġ�� ȭ�� ��ǥ�� ��ȯ
            Vector3 pos = Define.CamDefine.Cam.MainCam.WorldToScreenPoint(_nexusPos) + new Vector3(-10f, 175f, 0);

            Vector3 screenPos = new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                                            Mathf.Clamp(pos.y, 50f, Screen.height - 50f));

            // ȭ�� ������ ���� ��ġ�� ������Ű�� ���� ������ ��
            return screenPos;
        }
    }

    private void LateUpdate()
    {
        _shipbutton.gameObject.rectTransform().position = _shipScreenPos; //��UI��ġ�� �� ��ġ��

        Debug.Log($"Btn: {_shipbutton.gameObject.rectTransform().position}");
        Debug.Log($"ScreenPos: {_shipScreenPos}");

        _nexusbutton.gameObject.rectTransform().position = _nexusScreenPos; //�ؼ���UI��ġ�� �� ��ġ��
    }
}
