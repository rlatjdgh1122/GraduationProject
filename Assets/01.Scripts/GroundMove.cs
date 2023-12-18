using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using static UnityEngine.EventSystems.EventTrigger;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private int _enableStage;
    [SerializeField] private float _moveDuration = 5f;

    private Vector3 _moveDir;
    [SerializeField] private Color startColor;
    [SerializeField] private Color targetColor;

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
    }

    private void GroundMoveHandle()
    {
        if (WaveManager.Instance.CurrentStage == _enableStage)
        {
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
}
