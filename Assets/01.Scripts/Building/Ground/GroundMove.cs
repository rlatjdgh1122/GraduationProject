using DG.Tweening;
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
    private NavMeshSurface _parentSurface;
    private NavMeshSurface _surface;
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private void Awake()
    {
        _parentSurface = GameObject.Find("IcePlateParent").GetComponent<NavMeshSurface>();
        _surface = transform.parent.GetComponent<NavMeshSurface>();
        _outline = GetComponent<Outline>();

        _enemies = GetComponentsInChildren<Enemy>();
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

        WaveManager.Instance.OnPhaseStartEvent += GroundMoveHandle;
        WaveManager.Instance.OnPhaseEndEvent += SetOutline;
    }

    private void GroundMoveHandle()
    {
        if (WaveManager.Instance.CurrentStage == _enableStage)
        {
            Debug.Log("일치하여 스타트");
            foreach (Enemy enemy in _enemies)
            {
                enemy.enabled = true;
            }

            transform.DOMove(new Vector3(_moveDir.x, transform.position.y, _moveDir.z), _moveDuration).
                OnComplete(() =>
                {
                    _surface.enabled = true;
                    _surface.transform.SetParent(_parentSurface.transform);
                    _parentSurface.BuildNavMesh();

                    DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                    {
                        WaveManager.Instance.OnIceArrivedEventHanlder();
                        foreach (Enemy enemy in _enemies)
                        {
                            enemy.IsMove = true;
                            enemy.NavAgent.enabled = true;
                        }
                        WaveManager.Instance.OnPhaseStartEvent -= GroundMoveHandle;
                    });
                });
        }
    }

    private void SetOutline()
    {
        DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, endColor, 0.7f).OnComplete(() =>
        {
            _outline.enabled = false;
            WaveManager.Instance.OnPhaseEndEvent -= GroundMoveHandle;
        });
    }
}
