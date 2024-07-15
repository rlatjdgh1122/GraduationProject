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
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0)); // 객체의 높이에서 평면 설정

        if (groundPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance); // 마우스의 월드 위치 반환
        }
        return Vector3.zero; // 평면을 교차하지 않는 경우
    }
}
