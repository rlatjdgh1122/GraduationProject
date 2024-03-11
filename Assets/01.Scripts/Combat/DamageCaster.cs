using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float _detectRange = 1f;
    [SerializeField]
    private HitType _hitType;


    public LayerMask TargetLayer;

    private BaseStat _owner;

    public void SetOwner(BaseStat owner)
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
            int damage = (int)(_owner.damage.GetValue() * AfewTimes);
            raycastHealth.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            return true;
            //float critical = _controller.CharData.BaseCritical; <- 크리티컬 데미지 관련 로직입니다.
            //float criticalDamage = _controller.CharData.BaseCriticalDamage; 

            //float dice = Random.value; 
            //int fontSize = 10;
            //Color fontColor = Color.white;

            //if (dice < critical)
            //{
            //    damage = Mathf.CeilToInt(damage * criticalDamage);
            //    fontSize = 15;
            //    fontColor = Color.red;
            //}
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
                int damage = _owner.damage.GetValue();

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
            int damage = _owner.damage.GetValue();

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
                int damage = _owner.damage.GetValue();

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
            int damage = _owner.damage.GetValue();
            raycastHealth?.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            return true;
            //float critical = _controller.CharData.BaseCritical; <- 크리티컬 데미지 관련 로직입니다.
            //float criticalDamage = _controller.CharData.BaseCriticalDamage; 

            //float dice = Random.value; 
            //int fontSize = 10;
            //Color fontColor = Color.white;

            //if (dice < critical)
            //{
            //    damage = Mathf.CeilToInt(damage * criticalDamage);
            //    fontSize = 15;
            //    fontColor = Color.red;
            //}
        }

        return false;
    }
    public void BleedCast(int damage, int repeat, float duration, HitType hitType)
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
                StartCoroutine(BleedStart(damage, repeat, duration, health, raycastHit, hitType));
            }
        }
    }

    private IEnumerator BleedStart(int damage, int repeat, float duration, Health raycastHealth, RaycastHit ray, HitType hitType)
    {
        for (int i = 0; i < repeat; i++)
        {
            yield return new WaitForSeconds(duration);
            raycastHealth.ApplyDamage(damage, ray.point, ray.normal, hitType);
            Debug.Log(damage);
        }
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
            Gizmos.color = Color.white;
        }
    }
#endif
}
