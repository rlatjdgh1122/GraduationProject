using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    // 시야 영역의 반지름과 시야 각도
    [SerializeField]
    private float viewRadius;
    public float ViewRadius => viewRadius;

    [Range(0, 360)]
    [SerializeField]
    private float viewAngle;
    public float ViewAngle => viewAngle;

    [SerializeField]
    private LayerMask _targetLayer, _obstacleLayer;

    // Target layer에 ray hit된 transform을 보관하는 리스트
    private HashSet<Transform> _visibleTargets = new (); //중복되면 안되니까 HashSet

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

            if (_visibleTargets.Count > 0)
            {
                foreach (Transform t in _visibleTargets)
                {
                    Debug.Log(t.gameObject);
                }
            }
        }
    }

    public HashSet<Transform> FindVisibleTargets()
    {
        _visibleTargets.Clear();
        // viewRadius를 반지름으로 한 원 영역 내 _targetLayer 레이어인 콜라이더를 모두 가져옴
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, _targetLayer);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            dirToTarget.y = 0; // y 값을 무시함

            // 오브젝트의 forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle * 0.5f)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);

                // 타겟으로 가는 레이캐스트에 _obstacleLayer 걸리지 않으면 _visibleTargets에 Add
                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, _obstacleLayer))
                {
                    Debug.Log("발견!!!!");
                    _visibleTargets.Add(target);
                }
            }
        }

        return _visibleTargets;
    }

    // y축 오일러 각을 3차원 방향 벡터로 변환한다.
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if(angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
}
