using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipandNexusPositionUI : MonoBehaviour
{
    private Transform _shipPos; // 배의 위치를 나타내는 Transform
    private Vector3 _nexusPos = Vector3.zero; // 넥서스의 위치를 나타내는 Transform

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

    // 배의 화면상 위치
    private Vector3 _shipScreenPos
    {
        get
        {
            if (_shipPos == null)
            {
                // GeneralShip 스크립트를 가진 게임 오브젝트의 첫 번째 자식의 위치를 가져옴
                _shipPos = GameObject.FindObjectOfType<GeneralShip>().transform.GetChild(0).transform;
            }

            // 선박의 위치를 화면 좌표로 변환
            Vector3 pos = Camera.main.WorldToScreenPoint(_shipPos.position);

            // 화면 내에서 선박 위치를 유지시키기 위해 제한을 둠
            return new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                               Mathf.Clamp(pos.y, 50f, Screen.height - 50f));
        }
    }

    // 넥서스의 화면상 위치
    private Vector3 _nexusScreenPos
    {
        get
        {
            // 선박의 위치를 화면 좌표로 변환
            Vector3 pos = Define.CamDefine.Cam.MainCam.WorldToScreenPoint(_nexusPos) + new Vector3(-10f, 175f, 0);

            Vector3 screenPos = new Vector2(Mathf.Clamp(pos.x, 50f, Screen.width - 50f),
                                            Mathf.Clamp(pos.y, 50f, Screen.height - 50f));

            // 화면 내에서 선박 위치를 유지시키기 위해 제한을 둠
            return screenPos;
        }
    }

    private void LateUpdate()
    {
        _shipbutton.gameObject.rectTransform().position = _shipScreenPos; //배UI위치를 배 위치로

        Debug.Log($"Btn: {_shipbutton.gameObject.rectTransform().position}");
        Debug.Log($"ScreenPos: {_shipScreenPos}");

        _nexusbutton.gameObject.rectTransform().position = _nexusScreenPos; //넥서스UI위치를 배 위치로
    }
}
