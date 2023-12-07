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

    [SerializeField] private int _maxHitCount = 5; //최대로 때릴 수 있는 적 갯수
    public LayerMask whatIsHitable;
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
        Collider[] colliders = Physics.OverlapSphere(attackChecker.position, attackCheckRadius, whatIsHitable);

        // 거리순으로 정렬
        List<Collider> sortedColliders = colliders.OrderBy(collider => Vector3.Distance(transform.position, collider.transform.position)).ToList();

        foreach (Collider collider in sortedColliders)
        {
            Vector3 direction = (collider.transform.position - transform.position).normalized;

            if (collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _owner.Stat.GetDamage();
                ParticleSystem effect = Instantiate(_owner.HitEffect, collider.transform.position, Quaternion.identity);
                health.ApplyDamage(damage, direction, knockbackPower, _owner);

                return true; 
            }
        }

        return false; 
    }

    private void OnDrawGizmos()
    {
        if (attackChecker != null)
            Gizmos.DrawWireSphere(attackChecker.position, attackCheckRadius);
    }
}
