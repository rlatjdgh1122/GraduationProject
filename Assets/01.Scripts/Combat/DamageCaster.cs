using System;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.1f, 3f)]
    private float _casterRadius = 1f;
    [SerializeField]
    private float _casterInterpolation = 0.5f;  //이건 캐스터를 뒤쪽으로 빼주는 정도
    [SerializeField]
    private HitType _hitType;

    public LayerMask TargetLayer;

    private Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public bool CastDamage()
    {
        Vector3 sphereCastDirection = transform.forward;
        Vector3 sphereCastStartPos = transform.position - sphereCastDirection * _casterRadius;

        RaycastHit[] sphereCastHits = Physics.SphereCastAll(sphereCastStartPos, _casterRadius, sphereCastDirection, _casterRadius + _casterInterpolation, TargetLayer);

        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _casterRadius, TargetLayer);

        if (raycastSuccess && raycastHit.collider.TryGetComponent<IDamageable>(out IDamageable raycastHealth))
        {
            int damage = _owner.Stat.damage.GetValue();
            raycastHealth.ApplyDamage(damage, raycastHit.point, raycastHit.normal, _hitType);
            return true;
            //float critical = _controller.CharData.BaseCritical;
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

        foreach (RaycastHit sphereCastHit in sphereCastHits)
        {
            if (sphereCastHit.collider.TryGetComponent<IDamageable>(out IDamageable sphereCastHealth))
            {
                if (sphereCastHit.point != Vector3.zero)
                {
                    int damage = _owner.Stat.damage.GetValue();
                    sphereCastHealth.ApplyDamage(damage, sphereCastHit.point, sphereCastHit.normal, _hitType);
                    return true;
                }
            }
        }

        return false;
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _casterRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
