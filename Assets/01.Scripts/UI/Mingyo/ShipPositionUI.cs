using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPositionUI : MonoBehaviour
{
    private Transform _shipPos; // 배의 위치를 나타내는 Transform

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

    private void LateUpdate()
    {
        gameObject.rectTransform().position = _shipScreenPos; //UI를 배 위치로
    }
}
