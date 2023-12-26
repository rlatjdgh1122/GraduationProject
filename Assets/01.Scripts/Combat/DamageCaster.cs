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

    private Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public bool CastDamage()
    {
        RaycastHit raycastHit;
        bool raycastSuccess = Physics.Raycast(transform.position, transform.forward, out raycastHit, _detectRange, TargetLayer);

        if (raycastSuccess && raycastHit.collider.TryGetComponent<IDamageable>(out IDamageable raycastHealth))
        {
            int damage = _owner.Stat.damage.GetValue();
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
