using DG.Tweening;
using System;
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
    private NavMeshSurface _surface;
    private Outline _outline;
    #endregion

    public Enemy[] _enemies;

    private void Awake()
    {
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
        //WaveManager.Instance.OnIceArrivedEvent += SetChilderns;
    }

    //private void SetChilderns()
    //{
    //    if (WaveManager.Instance.CurrentStage == _enableStage)
    //    {
    //        foreach (Enemy enemy in _enemies)
    //        {
    //            enemy.transform.parent = null;
    //        }
    //        WaveManager.Instance.OnIceArrivedEvent -= SetChilderns;
    //    }
    //}

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
