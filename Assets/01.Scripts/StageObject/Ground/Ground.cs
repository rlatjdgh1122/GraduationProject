using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Rendering.FilterWindow;

public enum OutlineColorType
{
    Green,
    Red,
    None
}

[RequireComponent(typeof(Outline))]
public class Ground : MonoBehaviour, IComingObject
{
    private bool isInstalledBuilding;

    public bool IsInstalledBuilding => isInstalledBuilding;

    private Outline _outline;
    public Outline OutlineCompo => _outline;

    private GroundMovement _groundMove;

    private Enemy[] _enemies;

    private Transform _elementsParent;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        try
        {
            _groundMove = GetComponent<GroundMovement>();
        }
        catch
        {

        }

        _elementsParent = transform.Find("TopArea").transform;
    }

    private void ActivateEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.NavAgent.enabled = true;
            enemy.IsMove = true;
        }
    }

    private void GroundMoveHandler()
    {
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;
        _groundMove.GroundMove();
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;
        UpdateOutlineColor(OutlineColorType.None);
    }

    public void UpdateOutlineColor(OutlineColorType type)
    {
        if(_outline == null)
        {
            _outline = GetComponent<Outline>();
        }
        _outline.enabled = true;
        _outline.OutlineWidth = 2.0f;
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        switch (type)
        {
            case OutlineColorType.Green:
                _outline.OutlineColor = Color.green;
                break;
            case OutlineColorType.Red:
                _outline.OutlineColor = Color.red;
                break;
            case OutlineColorType.None:
                _outline.enabled = false;
                break;
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _groundMove.SetMoveTarget(trm);
    }

    public void ActivateGround()
    {
        SignalHub.OnBattlePhaseStartEvent += GroundMoveHandler;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;
        SignalHub.OnIceArrivedEvent += ActivateEnemies;
    }

    public void SetComingObjectInfo(Transform parentTransform, Vector3 position, ComingElements groundElements)
    {
        _groundMove.SetGroundPos(parentTransform,
                                 position);

        SetEnemies(groundElements.Enemies);
    }

    public void SetEnemies(Enemy[] enemies)
    {
        _enemies = enemies;
    }

    private void OnBattleEnd()
    {
        foreach (var enemy in _enemies)
        {
            PoolManager.Instance.Push(enemy); // 아니 이거 풀매니저 SO에 넣으면 오류 150개뜸 내가 보았을 때 이거는 씬에는 이미 있는데 풀매니저로 개지랄 하려고 해서 그러는듯. 나중에 빙판 자동 생성할때 같이 수정
            // 다시 생각해보니까 이거 펭귄 크기가 이상해서 그런듯. 근데 걍 나중에 합세
        }

        SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandler;
        SignalHub.OnIceArrivedEvent -= ActivateEnemies;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
        SignalHub.OnIceArrivedEvent -= ActivateEnemies;
    }
}
