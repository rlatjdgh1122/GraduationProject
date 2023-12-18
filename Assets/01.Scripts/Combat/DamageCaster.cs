using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageCaster : MonoBehaviour
{
    public Transform attackChecker;
    public float attackCheckRadius;

    public Vector2 knockbackPower;

    [SerializeField] private int _numberOfTargets = 5; //최대로 때릴 수 있는 적 갯수
    public LayerMask whatIsHitable;

    private Entity _owner;

    public void SetOwner(Entity owner, bool castByCloneSkill)
    {
        _owner = owner;
    }

    public bool CastDamage()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 250f, whatIsHitable))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _owner.Stat.GetDamage();
                ParticleSystem effect = Instantiate(_owner.HitEffect, hit.collider.transform.position, Quaternion.identity);
                health.ApplyDamage(damage, Vector2.zero, knockbackPower, _owner);

                return true;
            }
        }

        return false; // 피해를 입힐 요소가 지정한 개수만큼 없는 경우
    }


    private void OnDrawGizmos()
    {
        if (attackChecker != null)
            Gizmos.DrawWireSphere(attackChecker.position, attackCheckRadius);
    }
}
