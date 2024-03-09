using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // �þ� ������ �������� �þ� ����
    [SerializeField]
    private float viewRadius;
    public float ViewRadius => viewRadius;

    [Range(0, 360)]
    [SerializeField]
    private float viewAngle;
    public float ViewAngle => viewAngle;

    [SerializeField]
    private LayerMask _targetLayer, _obstacleLayer;

    void Start()
    {
        StartCoroutine(FindTargetsWithDelay(0.2f));
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    public HashSet<Transform> FindVisibleTargets()
    {
        HashSet<Transform> visibleTargets = new();

        // viewRadius�� ���������� �� �� ���� �� _targetLayer ���̾��� �ݶ��̴��� ��� ������
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, _targetLayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            dirToTarget.y = 0; // y ���� ������

            // ������Ʈ�� forward�� target�� �̷�� ���� ������ ���� �����
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle * 0.5f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // Ÿ������ ���� ����ĳ��Ʈ�� _obstacleLayer �ɸ��� ������ _visibleTargets�� Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, _obstacleLayer))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        return visibleTargets;
    }

    // y�� ���Ϸ� ���� 3���� ���� ���ͷ� ��ȯ�Ѵ�.
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if(angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}