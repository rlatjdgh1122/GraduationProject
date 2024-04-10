﻿using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float _detectRange = 1f;
    [SerializeField]
    private HitType _hitType;

    public LayerMask TargetLayer;

    private Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }


    /// <summary>
    /// 스패셜 데미지
    /// </summary>
    public bool CastSpecialDamage(float AfewTimes)
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent<IDamageable>(out IDamageable raycastHealth))
        {
            int damage = (int)(_owner.Stat.damage.GetValue() * AfewTimes);
            raycastHealth.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            return true;
        }

        return false;
    }


    /// <summary>
    /// 광역 데미지
    /// </summary>
    public void CaseAoEDamage(bool Knb, float value)
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        foreach (var col in Colls)
        {
            RaycastHit raycastHit;

            var dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;

            bool raycastSuccess = Physics.Raycast(transform.position, dir, out raycastHit, _detectRange, TargetLayer);

            if (raycastSuccess
                && raycastHit.collider.TryGetComponent<Health>(out Health health))
            {
                int damage = _owner.Stat.damage.GetValue();

                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);

                if (Knb == true)
                    health.KnockBack(value, raycastHit.normal);

            }
        }
    }

    public void CastOverlap()
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        foreach (var col in Colls)
        {       
            if (col.TryGetComponent<Health>(out Health health))
            {
                int damage = _owner.Stat.damage.GetValue();

                health.ApplyDamage(damage, col.transform.position, col.transform.position, _hitType);
            }
        }
    }

    /// <summary>
    /// 단일 스턴 데미지
    /// </summary>
    /// <returns> 공격 맞았나 여부</returns>
    public void CastStunDamage(bool Stun, float duration)
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent<Health>(out Health health))
        {
            int damage = _owner.Stat.damage.GetValue();

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);

            if (Stun == true)
                health.Stun(raycastHit, duration);
        }

    }

    public void CastAoEStunDamage(bool Stun, float duration)
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        foreach (var col in Colls)
        {
            RaycastHit raycastHit;

            var dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;

            bool raycastSuccess = Physics.Raycast(transform.position, dir, out raycastHit, _detectRange, TargetLayer);

            if (raycastSuccess
                && raycastHit.collider.TryGetComponent<Health>(out Health health))
            {
                int damage = _owner.Stat.damage.GetValue();

                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);

                if (Stun == true)
                    health.Stun(raycastHit, duration);

            }
        }
    }

    /// <summary>
    /// 단일 데미지
    /// </summary>
    /// <returns> 공격 맞았나 여부</returns>
    public bool CastDamage()
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent<IDamageable>(out IDamageable raycastHealth))
        {
            int damage = _owner.Stat.damage.GetValue();

            float critical = _owner.Stat.criticalChance.GetValue() * 0.01f;
            int criticalValue = _owner.Stat.criticalValue.GetValue();
            float adjustedDamage;
            float dice = UnityEngine.Random.value;
            HitType originType = _hitType;

            if (dice < critical)
            {
                _hitType = HitType.CriticalHit;
                adjustedDamage = damage * (1.0f + (criticalValue * 0.01f));
                damage = (int)adjustedDamage;
            }
            
            raycastHealth?.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            _hitType = originType;

            return true;
        }

        return false;
    }

    public bool CastArrowDamage(Collider coll, LayerMask targetLayer)
    {
        if (coll.TryGetComponent<IDamageable>(out IDamageable raycastHealth) && ((1 << coll.gameObject.layer) & targetLayer) != 0)
        {
            int damage = _owner.Stat.damage.GetValue();

            float critical = _owner.Stat.criticalChance.GetValue() * 0.01f;
            int criticalValue = _owner.Stat.criticalValue.GetValue();
            float adjustedDamage;
            float dice = UnityEngine.Random.value;
            HitType originType = _hitType;

            if (dice < critical)
            {
                _hitType = HitType.CriticalHit;
                adjustedDamage = damage * (1.0f + (criticalValue * 0.01f));
                damage = (int)adjustedDamage;

            }

            raycastHealth?.ApplyDamage(damage, coll.transform.position, coll.transform.position, _hitType);
            _hitType = originType;

            return true;
        }

        return false;
    }

    public bool CastMeteorDamage(Vector3 position, LayerMask targetLayer)
    {
        Collider[] colliders = Physics.OverlapSphere(position, _detectRange * 3, targetLayer);

        foreach (Collider collider in colliders)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                int damage = _owner.Stat.damage.GetValue();
                damageable.ApplyDamage(damage, position, collider.transform.position, _hitType);
            }
        }

        return true;
    }

    public void ShowCritical(EntityActionData actionData)
    {
        //actionData.HitPoint
    }

    public void SelectTypeAOECast(int damage, HitType hitType, SoundName sound, bool Knb = false, float value = 0)
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        SoundManager.Play3DSound(sound, transform.position);

        foreach (var col in Colls)
        {
            RaycastHit raycastHit;

            var dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;

            bool raycastSuccess = Physics.Raycast(transform.position, dir, out raycastHit, _detectRange, TargetLayer);

            if (raycastSuccess
                && raycastHit.collider.TryGetComponent<Health>(out Health health))
            {
                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, hitType);

                if (Knb == true && col.gameObject.layer != 13) //13은 넥서스 레이어고 임시
                    health.KnockBack(value, raycastHit.normal);

            }
        }
    }


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            //Gizmos.color = Color.red;
            //Gizmos.DrawWireSphere(transform.position, _detectRange);
            //Gizmos.color = Color.white;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.forward);
        }
    }
#endif
}
