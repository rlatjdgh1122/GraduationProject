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
        Vector3 startPos = transform.position - transform.forward * _casterRadius;

        RaycastHit[] hitArr = Physics.SphereCastAll(startPos, _casterRadius, transform.forward,
                                    _casterRadius + _casterInterpolation, TargetLayer);

        foreach (RaycastHit hit in hitArr)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                if (hit.point.sqrMagnitude == 0)
                {
                    continue;
                }

                int damage = _owner.Stat.damage.GetValue();
                health.ApplyDamage(damage, hit.point, hit.normal, _hitType);
                return true;
                //float critical = _controller.CharData.BaseCritical;
                //float criticalDamage = _controller.CharData.BaseCriticalDamage;

                ////아이템을 먹었다면 크리티컬이라면 이런 설정들이 여기서 계산될꺼야
                //float dice = Random.value; // 0 ~ 1까지의 값이 나온다.
                //int fontSize = 10;
                //Color fontColor = Color.white;

                //if (dice < critical)
                //{
                //    damage = Mathf.CeilToInt(damage * criticalDamage);
                //    fontSize = 15;
                //    fontColor = Color.red;
                //}
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
