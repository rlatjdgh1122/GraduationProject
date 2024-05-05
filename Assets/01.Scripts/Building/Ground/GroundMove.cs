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

    #region ������Ƽ
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private GameObject _waveEffect;

    private MeshCollider _meshCollider;

    private Vector3 _centerPos;
    private Vector3 _closestPointDirToCenter => _meshCollider.ClosestPoint(_centerPos); // �ݵ�� _meshCollider�� convex�� �������

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

        //���� �� �� ����Ʈ
        _waveEffect.gameObject.SetActive(true);

        transform.DOMove(_targetPos, _moveDuration).
            OnComplete(() =>
            {
                SoundManager.Play2DSound(SoundName.GroundHit);

                // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
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
            //PoolManager.Instance.Push(enemy); // �ƴ� �̰� Ǯ�Ŵ��� SO�� ������ ���� 150���� ���� ������ �� �̰Ŵ� ������ �̹� �ִµ� Ǯ�Ŵ����� ������ �Ϸ��� �ؼ� �׷��µ�. ���߿� ���� �ڵ� �����Ҷ� ���� ����
            // �ٽ� �����غ��ϱ� �̰� ��� ũ�Ⱑ �̻��ؼ� �׷���. �ٵ� �� ���߿� �ռ�
            
            enemy.gameObject.SetActive(false); // �׷��� �ӽ÷� �̷��� ��
        }

        SignalHub.OnBattlePhaseEndEvent -= DisableDeadBodys;
    }

    public void SetGroundInfo(Transform parentTransform, Vector3 position)
    {
        // ��ġ ���� ���� �ּ� �߰�
        // ���߿� ������ �� ��ġ �����ϵ��� �����ؾ� ��
        
        transform.SetParent(parentTransform); // �θ� ����
        transform.localPosition = position; // ������ ��ġ ����

        // ȸ�� �� �θ� ����
        transform.rotation = Quaternion.identity;
        transform.SetParent(null);


        // �߾Ӱ� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        // ���� ����� ����Ʈ�� ��Ʈ ����Ʈ ������ �Ÿ� ���
        float closestPointToHitPointX = Mathf.Abs(_closestPointDirToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(_closestPointDirToCenter.z - RaycastHit_ToCenterPos.z);

        // X�� Z �Ÿ� ���
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // Ÿ�� ���� ���
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z ��ǥ�� ���� Ÿ�� ���� ���� (�������, ��������, 0����)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // Ÿ�� ��ġ ����
        _targetPos = targetVec;
    }

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }

    public void SetEnemies(Enemy[] enemies) // ���߿� Ground�� �Űܼ� �ؾߵ�
    {
        _enemies = enemies;
    }
}
