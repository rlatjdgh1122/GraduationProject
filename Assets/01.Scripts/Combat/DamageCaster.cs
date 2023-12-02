using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DamageCaster : MonoBehaviour
{
    public Transform attackChecker;
    public float attackCheckRadius;

    public Vector2 knockbackPower;

    [SerializeField] private int _maxHitCount = 5; //최대로 때릴 수 있는 적 갯수
    public LayerMask whatIsEnemy;
    private Collider[] _hitResult;

    private Entity _owner;

    private void Awake()
    {
        _hitResult = new Collider[_maxHitCount];
    }

    public void SetOwner(Entity owner, bool castByCloneSkill)
    {
        _owner = owner;
    }

    public bool CastDamage()
    {
        int cnt = Physics.OverlapSphereNonAlloc(attackChecker.position, attackCheckRadius, _hitResult, whatIsEnemy);

        for (int i = 0; i < cnt; ++i)
        {
            Vector2 direction = (_hitResult[i].transform.position - transform.position).normalized;
            if (_hitResult[i].TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _owner.Stat.GetDamage();
                health.ApplyDamage(damage, direction, knockbackPower, _owner);
            }
        }

        return cnt > 0;
    }
    private void OnDrawGizmos()
    {
        if (attackChecker != null)
            Gizmos.DrawWireSphere(attackChecker.position, attackCheckRadius);
    }
}
