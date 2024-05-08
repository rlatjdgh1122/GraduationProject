using System;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float _detectRange = 1f;
    [SerializeField]
    private HitType _hitType;

    public LayerMask TargetLayer;

    private TargetObject _owner;
    private General General => _owner as General;

    public void SetOwner(TargetObject owner)
    {
        _owner = owner;
        SetPosition();
    }

    public void SetPosition()
    {
        transform.localPosition = new Vector3(0, 0.5f, 0);
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
            raycastHealth.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType, _owner);
            return true;
        }

        return false;
    }


    /// <summary>
    /// 광역 데미지
    /// </summary>
    public void CaseAoEDamage(float knbValue = 0f, float stunValue = 0f)
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

                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType, _owner);
                health.Knockback(knbValue, raycastHit.normal);
                health.Stun(stunValue);

            }
        }
    }
    /// <summary>
    /// 단일 데미지
    /// </summary>
    /// <returns> 공격 맞았나 여부</returns>
    public bool CastDamage(float knbValue = 0f, float stunValue = 0f)
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent<Health>(out Health health))
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

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType, _owner);
            health.Knockback(knbValue, raycastHit.normal);
            health.Stun(stunValue);

            _hitType = originType;
            return true;
        }

        return false;
    }

    public void CastWork()
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent<Health>(out Health health))
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

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType, _owner);

            _hitType = originType;
        }

       /* var Colls = Physics.OverlapSphere(transform.position, _detectRange, TargetLayer);

        foreach (var col in Colls)
        {
            if (col.TryGetComponent<Health>(out Health health))
            {
                int damage = _owner.Stat.damage.GetValue();

                health.ApplyDamage(damage, col.transform.position, col.transform.position, _hitType, _owner);
            }
        }*/
    }

    public void CastDashDamage()
    {
        var Colls = Physics.OverlapSphere(transform.position, _detectRange * 2f, TargetLayer);

        foreach (var col in Colls)
        {
            RaycastHit raycastHit;

            var dir = (col.transform.position - transform.position).normalized;
            dir.y = 0;

            bool raycastSuccess = Physics.Raycast(transform.position, dir, out raycastHit, _detectRange * 2f, TargetLayer);

            if (raycastSuccess
                && raycastHit.collider.TryGetComponent<Health>(out Health health))
            {
                if (health.currentHealth < health.maxHealth * 0.5f)
                {
                    health.ApplyDamage(100, raycastHit.point, raycastHit.normal, _hitType, _owner);
                    if (health.IsDead)
                    {
                        health.ApplyHitType(HitType.DashHit);
                        health.OnDashDeathEvent?.Invoke();
                        General.skill.CanUseSkill = true;
                        _hitType = HitType.KatanaHit;
                        return;
                    }
                }
                else
                {
                    General.skill.CanUseSkill = false;
                    int damage = _owner.Stat.damage.GetValue() * 2;
                    health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType, _owner);
                }
            }
        }
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

            raycastHealth?.ApplyDamage(damage, coll.transform.position, coll.transform.position, _hitType, _owner);
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
                damageable.ApplyDamage(damage, position, collider.transform.position, _hitType, _owner);
            }
        }

        return true;
    }

    public void SelectTypeAOECast(int damage, HitType hitType, SoundName sound, float range = 0, float knbValue = 0f, float stunValue = 0f)
    {
        var Colls = Physics.OverlapSphere(transform.position, range, TargetLayer);

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
                health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, hitType, _owner);
                health.Knockback(knbValue);
                health.Stun(stunValue);
            }
        }
    }

    #region BuildingDamageCast

    public bool CastBuildingAoEDamage(Vector3 position, LayerMask targetLayer, int damage) // 건물은 Entity 상속 안 받아서 매개변수로 데미지 받음
    {
        bool isHit = false;
        Collider[] colliders = Physics.OverlapSphere(position, _detectRange * 3, targetLayer);

        foreach (Collider collider in colliders)
        {
            //IDamageable damageable = collider.GetComponent<IDamageable>();
            //if (damageable != null)
            //{
            //    damageable.ApplyDamage(damage, position, collider.transform.position, _hitType, _owner);
            //    isHit = true;
            //}

            if (collider.TryGetComponent(out Health health))
            {
                health.ApplyDamage(damage, position, collider.transform.position, _hitType, _owner, false); // 이펙트 2개 나가서 여기서 bool로 처리. ApplyDamage에서 이펙트 하면 사라지는 것도 이상함
                isHit = true;
                health.Knockback(0.05f, collider.transform.position); // 내 생각에 넉백 있어야 할 것 같아서 그냥 하드코딩한 값으로 넣었음
            }
        }

        return isHit;
    }

    public void CastBuildingStunDamage(Health enemyHealth, RaycastHit hit, float duration, int damage)
    {
        enemyHealth.ApplyDamage(damage, hit.point, hit.normal, _hitType, _owner);
        enemyHealth.Stun(duration);
    }

    #endregion


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
            Gizmos.color = Color.white;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.forward);
        }
    }
#endif
}
