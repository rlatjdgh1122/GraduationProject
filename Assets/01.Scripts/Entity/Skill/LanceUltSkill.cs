using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceUltSkill : MonoBehaviour
{
    LancePenguinUltAttackableEntity _lanceUltEntity;

    private void Awake()
    {
        _lanceUltEntity = transform.parent.GetComponent<LancePenguinUltAttackableEntity>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            _lanceUltEntity.TruckAttack(LookMouse());
        }
    }

    private Vector3 LookMouse()
    {
        // ���콺 ��ġ�� �����ͼ� ���� ��ǥ�� ��ȯ
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.transform.position.y - transform.root.position.y; // ī�޶�� ������Ʈ�� Y�� ���̸� Z������ ����

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // �÷��̾� ��ġ
        Vector3 playerPosition = transform.root.position;

        // �÷��̾� �������� ���콺 ��ġ�� ���� ���͸� ���
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        return direction;
    }
}
