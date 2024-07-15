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

    public void LanceUltimate()
    {
        _lanceUltEntity.TruckAttack(LookMouse());
    }

    private Vector3 LookMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0)); // ��ü�� ���̿��� ��� ����

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance); // ���콺�� ���� ��ġ ��ȯ
        }
        return Vector3.zero; // ����� �������� �ʴ� ���
    }
}
