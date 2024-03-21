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

    #region ÇÁ·ÎÆÛÆ¼
    private NavMeshSurface _parentSurface;
    private NavMeshSurface _surface;
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private GameObject _waveEffect;

    private void Awake()
    {
        _parentSurface = GameObject.Find("IcePlateParent").GetComponent<NavMeshSurface>();
        _surface = transform.parent.GetComponent<NavMeshSurface>();
        _outline = GetComponent<Outline>();

        _enemies = GetComponentsInChildren<Enemy>();

        _waveEffect = transform.Find("WaterWave").gameObject;
    }

    private void Start()
    {
        _surface.enabled = false;
        _moveDir = transform.parent.localPosition;

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
        if (WaveManager.Instance.CurrentWaveCount == _enableStage)
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.enabled = true;
            }

            //ºùÇÏ ¿Ã ¶§ ÀÌÆåÆ®
            _waveEffect.gameObject.SetActive(true);

            transform.DOMove(new Vector3(_moveDir.x, transform.position.y, _moveDir.z), _moveDuration).
                OnComplete(() =>
                {
                    _surface.enabled = true;
                    _surface.transform.SetParent(_parentSurface.transform);
                    _parentSurface.BuildNavMesh();

                    // ºÎµúÈú ¶§ ÀÌÆåÆ® / Ä«¸Þ¶ó ½¦ÀÌÅ© + »ç¿îµå
                    CoroutineUtil.CallWaitForSeconds(0.5f, () => Define.CamDefine.Cam.ShakeCam.enabled = true,
                                                         () => Define.CamDefine.Cam.ShakeCam.enabled = false);


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

}
