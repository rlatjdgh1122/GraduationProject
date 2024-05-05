using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private float _moveDuration = 5f;

    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color endColor;

    [SerializeField]
    private LayerMask _groundLayer;

    #region 프로퍼티
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private GameObject _waveEffect;

    private MeshCollider _meshCollider;

    private Vector3 _centerPos;
    private Vector3 _closestPointDirToCenter => _meshCollider.ClosestPoint(_centerPos); // 반드시 _meshCollider의 convex를 켜줘야함

    private Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            if (Physics.Raycast(_closestPointDirToCenter, (_centerPos - _closestPointDirToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }

    private Vector3 _targetPos;

    private void Awake()
    {
        _outline = GetComponent<Outline>();

        _waveEffect = transform.Find("TopArea/GlacierModel/WaterWave").gameObject;
        _meshCollider = transform.Find("TopArea").GetComponent<MeshCollider>();
    }

    private void Start()
    {
        //_surface.enabled = false;

        foreach (Enemy enemy in _enemies)
        {
            enemy.IsMove = false;
            enemy.NavAgent.enabled = false;
            enemy.enabled = false;
        }

        SignalHub.OnBattlePhaseStartEvent += GroundMoveHandle;
        SignalHub.OnBattlePhaseEndEvent += SetOutline;

    }

    private void GroundMoveHandle()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.enabled = true;
        }

        //빙하 올 때 이펙트
        _waveEffect.gameObject.SetActive(true);

        transform.DOMove(_targetPos, _moveDuration).
            OnComplete(() =>
            {
                SoundManager.Play2DSound(SoundName.GroundHit);

                // 부딪힐 때 이펙트 / 카메라 쉐이크 + 사운드
                CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                                     () => Define.CamDefine.Cam.ShakeCam.enabled = false);

                SignalHub.OnBattlePhaseEndEvent += DisableDeadBodys;

                _waveEffect.gameObject.SetActive(false);


                Debug.Log(_enemies.Length);

                foreach (Enemy enemy in _enemies)
                {
                    enemy.IsMove = true;
                    enemy.NavAgent.enabled = true;
                }

                WaveManager.Instance.OnIceArrivedEventHanlder();
                SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;

                //DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                //{
                //    WaveManager.Instance.OnIceArrivedEventHanlder();

                //    Debug.Log(_enemies.Length);
                //    foreach (Enemy enemy in _enemies)
                //    {
                //        enemy.IsMove = true;
                //        enemy.NavAgent.enabled = true;
                //    }
                //    SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
                //});
            });


    }

    private void SetOutline()
    {
        DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, endColor, 0.7f).OnComplete(() =>
        {
            _outline.enabled = false;
            SignalHub.OnBattlePhaseEndEvent -= GroundMoveHandle;
        });
    }

    private void DisableDeadBodys()
    {
        foreach (var enemy in _enemies)
        {
            //PoolManager.Instance.Push(enemy); // 아니 이거 풀매니저 SO에 넣으면 오류 150개뜸 내가 보았을 때 이거는 씬에는 이미 있는데 풀매니저로 개지랄 하려고 해서 그러는듯. 나중에 빙판 자동 생성할때 같이 수정
            // 다시 생각해보니까 이거 펭귄 크기가 이상해서 그런듯. 근데 걍 나중에 합세
            
            enemy.gameObject.SetActive(false); // 그래서 임시로 이렇게 함
        }

        SignalHub.OnBattlePhaseEndEvent -= DisableDeadBodys;
    }

    public void SetGroundInfo(Transform parentTransform, Vector3 position)
    {
        // 위치 설정 관련 주석 추가
        // 나중에 생성할 때 위치 설정하도록 변경해야 함
        
        transform.SetParent(parentTransform); // 부모 설정
        transform.localPosition = position; // 빙하의 위치 설정

        // 회전 및 부모 해제
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);


        // 중앙과 히트 포인트 사이의 거리 계산
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        // 가장 가까운 포인트와 히트 포인트 사이의 거리 계산
        float closestPointToHitPointX = Mathf.Abs(_closestPointDirToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(_closestPointDirToCenter.z - RaycastHit_ToCenterPos.z);

        // X와 Z 거리 계산
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // 타겟 벡터 계산
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // 타겟 위치 설정
        _targetPos = targetVec;
    }

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }

    public void SetEnemies(Enemy[] enemies) // 나중에 Ground로 옮겨서 해야됨
    {
        _enemies = enemies;
    }
}
