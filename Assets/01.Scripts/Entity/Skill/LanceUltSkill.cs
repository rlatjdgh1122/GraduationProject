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
        // 마우스 위치를 가져와서 월드 좌표로 변환
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.transform.position.y - transform.root.position.y; // 카메라와 오브젝트의 Y축 차이를 Z축으로 설정

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);

        // 플레이어 위치
        Vector3 playerPosition = transform.root.position;

        // 플레이어 기준으로 마우스 위치의 방향 벡터를 계산
        Vector3 direction = (mouseWorldPosition - playerPosition).normalized;

        return direction;
    }
}
