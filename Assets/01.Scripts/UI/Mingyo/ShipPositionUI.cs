using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPositionUI : MonoBehaviour
{
    private Transform _shipPos; // ���� ��ġ�� ��Ÿ���� Transform

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

    private void LateUpdate()
    {
        gameObject.rectTransform().position = _shipScreenPos; //UI�� �� ��ġ��
    }
}
