using System;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 3f)]
    private float _casterRadius = 1f;
    [SerializeField]
    private float _casterInterpolation = 0.5f;  //�̰� ĳ���͸� �������� ���ִ� ����
    [SerializeField]
    private LayerMask _targetLayer;

    //public Transform attackChecker;
    //public float attackCheckRadius;

    //public Vector2 knockbackPower;

    //[SerializeField] private int _numberOfTargets = 5; //�ִ�� ���� �� �ִ� �� ����
    //public LayerMask whatIsHitable;

    private Entity _owner;

    public void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public void CastDamage()
    {
        Vector3 startPos = transform.position - transform.forward * _casterRadius;

        RaycastHit[] hitArr = Physics.SphereCastAll(startPos, _casterRadius, transform.forward,
                                    _casterRadius + _casterInterpolation, _targetLayer);

        foreach (RaycastHit hit in hitArr)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                if (hit.point.sqrMagnitude == 0)
                {
                    continue;
                }

                int damage = _owner.Stat.damage.GetValue();
                health.ApplyDamage(damage, hit.point, hit.normal);
                //float critical = _controller.CharData.BaseCritical;
                //float criticalDamage = _controller.CharData.BaseCriticalDamage;

                ////�������� �Ծ��ٸ� ũ��Ƽ���̶�� �̷� �������� ���⼭ ���ɲ���
                //float dice = Random.value; // 0 ~ 1������ ���� ���´�.
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
