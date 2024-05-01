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

    #region 프로퍼티
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
        if (WaveManager.Instance.CurrentWaveCount == _enableStage) // 나중에 랜덤으로 바꾸면 걍 없애기 
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.enabled = true;
            }

            //빙하 올 때 이펙트
            _waveEffect.gameObject.SetActive(true);

            transform.DOMove(new Vector3(_moveDir.x, transform.position.y, _moveDir.z), _moveDuration).
                OnComplete(() =>
                {
                    SoundManager.Play2DSound(SoundName.GroundHit);
                    //_surface.enabled = true;
                    //_surface.transform.SetParent(_parentSurface.transform);
                    //_parentSurface.BuildNavMesh();

                    // 부딪힐 때 이펙트 / 카메라 쉐이크 + 사운드
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
            //PoolManager.Instance.Push(enemy); // 아니 이거 풀매니저 SO에 넣으면 오류 150개뜸 내가 보았을 때 이거는 씬에는 이미 있는데 풀매니저로 개지랄 하려고 해서 그러는듯. 나중에 빙판 자동 생성할때 같이 수정
            // 다시 생각해보니까 이거 펭귄 크기가 이상해서 그런듯. 근데 걍 나중에 합세
            
            enemy.gameObject.SetActive(false); // 그래서 임시로 이렇게 함
        }

        SignalHub.OnBattlePhaseEndEvent -= DisableDeadBodys;
    }

    public void SetGroundInfo(Vector3 rotation)
    {
        //여기다가 적이나 보상 같은 것 설정
        _enableStage = WaveManager.Instance.CurrentWaveCount; // 나중에 랜덤으로 바꾸면 걍 없애기 
    }
}
