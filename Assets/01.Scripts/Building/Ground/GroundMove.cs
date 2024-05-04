using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private int _enableStage;
    [SerializeField] private float _moveDuration = 5f;

    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color endColor;

    [SerializeField]
    private LayerMask _groundLayer;

    #region ������Ƽ
    //private NavMeshSurface _parentSurface;
    //private NavMeshSurface _surface;
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private GameObject _waveEffect;

    private Collider _col;

    private Vector3 _centerPos;
    private Vector3 _closestPointDirToCenter
    {
        get
        {
            GameObject obj = new GameObject();
            obj.transform.SetParent(_col.transform.parent);
            obj.transform.localPosition = _col.ClosestPoint(_centerPos);
            return _col.ClosestPointOnBounds(obj.transform.localPosition);
        }
        //get
        //{
        //    if (Physics.Raycast(transform.position, _centerPos.normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
        //    {
        //        Debug.Log(hit.collider.gameObject);
        //        return hit.point;
        //    }
        //    return Vector3.zero;
        //}
    }

    private void Awake()
    {
        //_parentSurface = GameObject.Find("IcePlateParent").GetComponent<NavMeshSurface>();
        //_surface = transform.parent.GetComponent<NavMeshSurface>();
        _outline = GetComponent<Outline>();

        _enemies = GetComponentsInChildren<Enemy>();

        _waveEffect = transform.Find("TopArea/GlacierModel/WaterWave").gameObject;
        _col = transform.Find("TopArea").GetComponent<Collider>();
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
        //if (WaveManager.Instance.CurrentWaveCount == _enableStage) // ���߿� �������� �ٲٸ� �� ���ֱ� 
        //{
        //    foreach (Enemy enemy in _enemies)
        //    {
        //        enemy.enabled = true;
        //    }

        //    //���� �� �� ����Ʈ
        //    _waveEffect.gameObject.SetActive(true);

        //    transform.DOMove(new Vector3(targetPos.x, transform.position.y, targetPos.z), _moveDuration).
        //        OnComplete(() =>
        //        {
        //            SoundManager.Play2DSound(SoundName.GroundHit);
        //            //_surface.enabled = true;
        //            //_surface.transform.SetParent(_parentSurface.transform);
        //            //_parentSurface.BuildNavMesh();

        //            // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
        //            CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
        //                                                 () => Define.CamDefine.Cam.ShakeCam.enabled = false);

        //            SignalHub.OnBattlePhaseEndEvent += DisableDeadBodys;

        //            _waveEffect.gameObject.SetActive(false);

        //            DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
        //            {
        //                WaveManager.Instance.OnIceArrivedEventHanlder();

        //                foreach (Enemy enemy in _enemies)
        //                {
        //                    enemy.IsMove = true;
        //                    enemy.NavAgent.enabled = true;
        //                }
        //                SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
        //            });
        //        });
        //}

        
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

    public void SetGroundInfo(Transform parentTrm, Vector3 position)
    {
        // ����ٰ� ���̳� ���� ���� �� ����
        // ���߿��� �̰� �׳� �����Ҷ� �ϵ��� �ٲ���

        #region ��ġ ����

        transform.transform.SetParent(parentTrm);
        transform.transform.localPosition = position; // �ϴ��� ���� ������ ����ؼ� �ָ��� ��������

        transform.rotation = Quaternion.identity;
        transform.SetParent(null);

        CoroutineUtil.CallWaitForSeconds(1f, null, () => Debug.Log(_closestPointDirToCenter));

        #endregion

        _enableStage = WaveManager.Instance.CurrentWaveCount; // ���߿� �������� �ٲٸ� �� ���ֱ� 
    }

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }
}
