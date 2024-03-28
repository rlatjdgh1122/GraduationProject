using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using static UnityEngine.Rendering.DebugUI;

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
        bool raycastSuccess = Physics.BoxCast
            (transform.position, transform.lossyScale / 2f, transform.forward, out raycastHit, transform.rotation, _detectRange, TargetLayer);

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
                Debug.Log("크리티컬!");
            }
            
            raycastHealth?.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            _hitType = originType;

            return true;
        }

        return false;
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
        Gizmos.DrawWireCube(transform.position, transform.lossyScale);
    }
    #endif

    public void ShowCritical(EntityActionData actionData)
    {
        //actionData.HitPoint
    }

    public void SelectTypeAOECast(int damage, HitType hitType, bool Knb = false, float value = 0)
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
                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, hitType);

                if (Knb == true)
                    health.KnockBack(value, raycastHit.normal);

            }
        }
    }

    //private IEnumerator BleedStart(int damage, int repeat, float duration, Health raycastHealth, RaycastHit ray, HitType hitType)
    //{
    //    for (int i = 0; i < repeat; i++)
    //    {
    //        yield return new WaitForSeconds(duration);
    //        raycastHealth.ApplyDamage(damage, ray.point, ray.normal, hitType);
    //        Debug.Log(damage);
    //    }
    //}
}
