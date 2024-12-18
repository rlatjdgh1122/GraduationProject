using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(FieldOfView))]
public abstract class DefenseBuilding : BaseBuilding
{
    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float groundCheckRange;

    FieldOfView _fov;
    protected FieldOfView FOV => _fov;

    private Dictionary<int, Ground> _groundOutlines = new();

    private HashSet<Ground> _previousGrounds = new();

    private HashSet<Ground> _removedGrounds = new();
    private HashSet<Ground> currentGrounds = new();

    private Health _health;

    protected Transform _currentTarget
    {
        get
        {
            if (_fov.FindVisibleTargets().FirstOrDefault() != null)
            {
                return _fov.FindVisibleTargets().FirstOrDefault().transform;
            }
            return null;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        _fov = GetComponent<FieldOfView>();
        _health = GetComponent<Health>();

        _health.SetHealth(_characterStat);
        _health.enabled = false; // 설치 완료 되기 전까지는 공격 대상 X
    }

    public override void Init()
    {
        _groundOutlines.Clear();
        _previousGrounds.Clear();
        _removedGrounds.Clear();
        currentGrounds.Clear();
    }

    protected override void Update()
    {
        base.Update();

        if (IsSelected)
        {
            DisplayRange();
        }
    }

    public override void StopInstall()
    {
        base.StopInstall();

        foreach (var g in _groundOutlines.Values)
        {
            g.UpdateOutlineColor(OutlineColorType.None);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, groundCheckRange);
    }

    private void OnMouseEnter()
    {
        if (IsInstalled)
        {
            DisplayRange();
            _health.OnUIUpdate?.Invoke(_health.currentHealth, _health.maxHealth);
        }
    }

    private void OnMouseExit()
    {
        if (IsInstalled)
        {
            ResetRemovedGroundsOutline();
            _health.OffUIUpdate?.Invoke();
        }
    }

    protected override void SetInstalled()
    {
        base.SetInstalled();

        ColliderCompo.enabled = true;

        _health.enabled = true; // 설치 완료 되면 공격 대상 O
    }

    private void DisplayRange()
    {
        Collider[] newColls = Physics.OverlapSphere(transform.position, groundCheckRange, _groundLayer);
        currentGrounds.Clear();

        foreach (Collider coll in newColls)
        {
            int id = coll.GetInstanceID();

            if (!_groundOutlines.TryGetValue(id, out Ground ground))
            {
                ground = coll.transform.parent.GetComponent<Ground>();
                _groundOutlines.Add(id, ground);
            }

            currentGrounds.Add(ground);

            SetGroundToRedOutline(ground);
        }

        _removedGrounds.Clear();
        _removedGrounds.UnionWith(_previousGrounds);
        _removedGrounds.ExceptWith(currentGrounds);

        foreach (var ground in _removedGrounds)
        {
            ground.UpdateOutlineColor(OutlineColorType.None);
        }

        _previousGrounds.UnionWith(currentGrounds);
    }

    private void SetGroundToRedOutline(Ground ground)
    {
        if (ground.OutlineCompo.OutlineWidth < 10)
        {
            ground.enabled = true;
            ground.OutlineCompo.OutlineMode = Outline.Mode.OutlineVisible;
            ground.UpdateOutlineColor(OutlineColorType.Red);
            ground.OutlineCompo.OutlineWidth = 10.0f;
        }
    }

    private void ResetRemovedGroundsOutline()
    {
        Collider[] newColls = Physics.OverlapSphere(transform.position, groundCheckRange, _groundLayer);

        foreach (Collider coll in newColls)
        {
            int id = coll.GetInstanceID();

            if (!_groundOutlines.TryGetValue(id, out Ground ground))
            {
                ground = coll.transform.parent.GetComponent<Ground>();
                _groundOutlines.Add(id, ground);
            }

            ground.UpdateOutlineColor(OutlineColorType.None);
        }
    }

    public override void OnDisable()
    {
        base.OnDisable();

        ResetRemovedGroundsOutline();
    }
}