using System.Collections.Generic;
using UnityEngine;

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
    public GroundMovement GroundMove => _groundMove;

    private List<Enemy> _enemies = new();

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
    }

    public void ActivateEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.IsMove = true;
            enemy.ColliderCompo.enabled = true;
            enemy.NavAgent.enabled = true;
            //enemy.StateMachine.ChangeState(EnemyStateType.Move);
            enemy.FindNearestTarget();
        }
    }

    private void GroundMoveHandler()
    {
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;
        _groundMove.Move();
    }

    public void InstallBuilding() //땅에 설치되었다고 처리
    {
        isInstalledBuilding = true;
        UpdateOutlineColor(OutlineColorType.None);
    }

    public void UnInstallBuilding()
    {
        isInstalledBuilding = false;
    }

    public void UpdateOutlineColor(OutlineColorType type)
    {
        if (_outline == null) // 왜인지 Outline을 Awake에서 못 찾을 때가 있음
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
        GroundMoveHandler();
        //SignalHub.OnGroundArrivedEvent += ActivateEnemies;
    }

    public void SetComingObjectInfo(ComingElements groundElements, Transform parentTransform, Vector3 position)
    {
        _groundMove.SetComingObejctPos(parentTransform, position);

        SetEnemies(groundElements.Enemies);
    }

    public void SetEnemies(List<Enemy> enemies)
    {
        if (_enemies.Count > 0) { _enemies.Clear(); }
        _enemies = enemies;
    }

    private void OnBattleEnd()
    {
        SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
        //SignalHub.OnGroundArrivedEvent -= ActivateEnemies;
        if (_enemies.Count > 0) _enemies.Clear();
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandler;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
        //SignalHub.OnGroundArrivedEvent -= ActivateEnemies;
    }
}
