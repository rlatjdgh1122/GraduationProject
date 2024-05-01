using DG.Tweening;
using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private int _enableStage;
    [SerializeField] private float _moveDuration = 5f;

    private Vector3 _moveDir;
    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color endColor;

    #region ������Ƽ
    //private NavMeshSurface _parentSurface;
    //private NavMeshSurface _surface;
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private GameObject _waveEffect;

    private void Awake()
    {
        //_parentSurface = GameObject.Find("IcePlateParent").GetComponent<NavMeshSurface>();
        //_surface = transform.parent.GetComponent<NavMeshSurface>();
        _outline = GetComponent<Outline>();

        _enemies = GetComponentsInChildren<Enemy>();

        _waveEffect = transform.Find("WaterWave").gameObject;
        _moveDir = transform.parent.localPosition;
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
        if (WaveManager.Instance.CurrentWaveCount == _enableStage) // ���߿� �������� �ٲٸ� �� ���ֱ� 
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.enabled = true;
            }

            //���� �� �� ����Ʈ
            _waveEffect.gameObject.SetActive(true);

            transform.DOMove(new Vector3(_moveDir.x, transform.position.y, _moveDir.z), _moveDuration).
                OnComplete(() =>
                {
                    SoundManager.Play2DSound(SoundName.GroundHit);
                    //_surface.enabled = true;
                    //_surface.transform.SetParent(_parentSurface.transform);
                    //_parentSurface.BuildNavMesh();

                    // �ε��� �� ����Ʈ / ī�޶� ����ũ + ����
                    CoroutineUtil.CallWaitForSeconds(.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                                         () => Define.CamDefine.Cam.ShakeCam.enabled = false);

                    SignalHub.OnBattlePhaseEndEvent += DisableDeadBodys;

                    _waveEffect.gameObject.SetActive(false);

                    DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                    {
                        WaveManager.Instance.OnIceArrivedEventHanlder();

                        foreach (Enemy enemy in _enemies)
                        {
                            enemy.IsMove = true;
                            enemy.NavAgent.enabled = true;
                        }
                        SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
                    });
                });
        }

        
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

    public void SetGroundInfo(Vector3 rotation)
    {
        //����ٰ� ���̳� ���� ���� �� ����
        _enableStage = WaveManager.Instance.CurrentWaveCount; // ���߿� �������� �ٲٸ� �� ���ֱ� 
    }
}
